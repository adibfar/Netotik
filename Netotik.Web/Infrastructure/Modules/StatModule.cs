﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading;
using System.Web;
using System.Web.SessionState;
using System.Xml.Linq;
using UAParser;
using Netotik.Domain.Entity;
using Netotik.Services.Abstract;
using Netotik.IocConfig;
using Netotik.Data;

namespace Netotik.Web.Infrastructure.Modules
{
    public class StatModule : IHttpModule
    {
        public StatModule()
        {

        }
        public void Dispose()
        {

        }

        public void Init(HttpApplication context)
        {
            IHttpModule module = context.Modules["Session"];
            if (module.GetType() == typeof(SessionStateModule))
            {
                SessionStateModule stateModule = (SessionStateModule)module;
                stateModule.Start += (Session_Start);
            }
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            //بررسی برای اینکه درخواست کننده موتور جستجوگر است ؟
            //و یا اینکه در لیست ای پی هایی است که نباید در آمار آورده شوند
            if (!IsBotOrCrawler(context.Request.UserAgent))
            {

                var uow = ProjectObjectFactory.Container.GetInstance<IUnitOfWork>();

                var statistic = new Statistic();
                statistic.IpAddress = GetIPAddress();
                statistic.UserOs = GetUserOS(context.Request.UserAgent);
                statistic.PageViewed = context.Request.Url.AbsolutePath;
                statistic.Referer = context.Request.UrlReferrer?.ToString() ?? "Direct";
                statistic.UserAgent = context.Request.Browser.Browser;
                statistic.DateStamp = DateTime.Now;

                var statService = ProjectObjectFactory.Container.GetInstance<IStatisticsService>();
                statService.Add(statistic);
                uow.SaveChanges();


                //بدست آوردن کشور بازدید کننده
                try
                {
                    XDocument xdoc = XDocument.Load("http://www.freegeoip.net/xml/" + GetIPAddress());
                    var country = xdoc.Descendants("Response").Select(c => new
                    {
                        IpAddress = c.Element("IP")?.Value,
                        CountryCode = c.Element("CountryCode")?.Value,
                        CountryName = c.Element("CountryName")?.Value,
                        RegionCode = c.Element("RegionCode")?.Value,
                        RegionName = c.Element("RegionName")?.Value,
                        City = c.Element("City")?.Value,
                        ZipCode = c.Element("ZipCode")?.Value,
                        TimeZone = c.Element("TimeZone")?.Value,
                        Latitude = c.Element("Latitude")?.Value,
                        Longitude = c.Element("Longitude")?.Value,
                        MetroCode = c.Element("MetroCode")?.Value,
                    });
                    var countryData = country.First();


                    //Check If The Country Is already in database or not
                    var statCountryService = ProjectObjectFactory.Container.GetInstance<IStatisticCountryService>();
                    if (statCountryService.All().Any(c => c.CountryCode.Equals(countryData.CountryCode)))
                    {
                        //then Update the ViewCount
                        StatisticCountry currentCountry =
                            statCountryService.All().First(cc => cc.CountryCode.Equals(countryData.CountryCode));
                        currentCountry.ViewCount++;
                        uow.SaveChanges();
                    }
                    else
                    {
                        //then add this Country To Database
                        var newCountry = new StatisticCountry()
                        {
                            CountryCode = countryData.CountryCode,
                            CountryName = countryData.CountryName,
                            Latitude = countryData.Latitude,
                            Longitude = countryData.Longitude,
                            ViewCount = 1
                        };
                        statCountryService.Add(newCountry);
                        uow.SaveChanges();

                    }
                }
                catch
                {

                }

            }
        }

        #region

        //list of bots and crawlers
        private static readonly List<string> KnownCrawlers = new List<string>
        {
            "bot","crawler","baiduspider","80legs","ia_archiver","ahrefsBot","twitterbot",
            "yoozbot","yandexBot","bitlybot","other", "sogou web spider", "python requests",
            "voyager","curl","wget","yahoo! slurp","mediapartners-google", "mj12bot",
            "seznamBot", "Sogou web spider", "360Spider", "sogouwebspider"
        };

        //detect the crawlers and bots
        public static bool IsBotOrCrawler(string agent)
        {
            agent = agent.ToLower();
            return KnownCrawlers.Any(crawler => agent.Contains(crawler) || agent.Equals(crawler));
        }

        public static string GetUserOS(string userAgent)
        {
            // get a parser with the embedded regex patterns
            var uaParser = Parser.GetDefault();
            ClientInfo c = uaParser.Parse(userAgent);
            return c.OS.Family;
        }
        public string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }
        #endregion
    }
}
