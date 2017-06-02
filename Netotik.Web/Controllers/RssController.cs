using Netotik.Services.Abstract;
using Netotik.ViewModels.Common.Rss;
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
                    new XElement("title", "آخرین مطالب سایت"),
                    new XElement("link", "http://" + Request.Url.Host + "/rss"),
                    new XElement("description", "آخرین مطالب سایت من"),
                    new XElement("copyright", "(c)" + DateTime.Now.Year + ", نام سایت من.تمامی حقوق محفوظ است"),
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
            return new List<RssItemViewModel>();

        }
    }
}