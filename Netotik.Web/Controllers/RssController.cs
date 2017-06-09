using Netotik.Resources;
using Netotik.Services.Abstract;
using Netotik.ViewModels.Common.Rss;
using Netotik.Web.Infrastructure.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace Netotik.Web.Controllers
{
    public partial class RssController : Controller
    {
        private readonly IContentService _contentService;

        public RssController(IContentService contentService)
        {
            _contentService = contentService;
        }


        [OutputCache(Duration = 86400)]
        public virtual ContentResult Index()
        {

            var items = GetRssFeed();
            var rss = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
              new XElement("rss",
                new XAttribute("version", "2.0"),
                  new XElement("channel",
                    new XElement("title", Captions.LastContent),
                    new XElement("link", Url.Action(MVC.Rss.Index())),
                    new XElement("description", string.Format("{0} | {1}", Captions.Netotik, Captions.LastContent)),
                    new XElement("copyright", Captions.CopyRight),
                  from item in items
                  select
                  new XElement("item",
                    new XElement("title", item.Title),
                    new XElement("description", item.Description),
                    new XElement("link", item.Url),
                    new XElement("pubDate", item.PublishDate)

                  )
                )
              )
            );
            return Content(rss.ToString(), "text/xml");
        }

        /// <summary>
        /// Using For Rss Reader..
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RssItemViewModel> GetRssFeed()
        {
            return _contentService
                .GetRss(100, LanguageCache.GetLanguage(HttpContext).Id)
                .Select(x => new RssItemViewModel
                {
                    ContentId = x.Id,
                    Description = x.BodyOverview,
                    PublishDate = x.StartDate.Value,
                    Title = x.Title,
                    Url = Url.Action(MVC.Blog.Detail(x.Id))
                })
                .ToList();
        }
    }
}