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
    public partial class SitemapController : Controller
    {
        private readonly IContentService _contentService;

        public SitemapController(IContentService contentService)
        {
            _contentService = contentService;
        }

        [OutputCache(Duration = 86400)]
        public virtual ContentResult Index()
        {
            XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
            var items = GetLinks();
            var sitemap = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
                new XElement(ns + "urlset",
                    from item in items
                    select
                    new XElement("url",
                      new XElement("loc", item.Url),
                      new XElement("changefreq", "monthly"),
                      new XElement("priority", "0.5")
                      )
                    )
                  );
            return Content(sitemap.ToString(), "text/xml");
        }

        /// <summary>
        /// Using For SiteMap
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RssItemViewModel> GetLinks()
        {
            return new List<RssItemViewModel>();
        }
    }
}