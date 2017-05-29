using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Netotik.Web.Infrastructure
{
    public class SitemapNode
    {
        public string Url { get; set; }
        public DateTime? LastModified { get; set; }
        public SitemapFrequency? Frequency { get; set; }
        public double? Priority { get; set; }


        public SitemapNode(string url)
        {
            Url = url;
            Priority = null;
            Frequency = null;
            LastModified = null;
        }

        public SitemapNode(RequestContext request, object routeValues)
        {
            Url = GetUrl(request, new RouteValueDictionary(routeValues));
            Priority = null;
            Frequency = null;
            LastModified = null;
        }

        private string GetUrl(RequestContext request, RouteValueDictionary values)
        {
            var routes = RouteTable.Routes;
            var data = routes.GetVirtualPathForArea(request, values);

            if (data == null)
            {
                return null;
            }

            var baseUrl = request.HttpContext.Request.Url;
            var relativeUrl = data.VirtualPath;

            return request.HttpContext != null &&
                   (request.HttpContext.Request != null && baseUrl != null)
                       ? new Uri(baseUrl, relativeUrl).AbsoluteUri
                       : null;
        }



    }

    public enum SitemapFrequency
    {
        Never,
        Yearly,
        Monthly,
        Weekly,
        Daily,
        Hourly,
        Always
    }
}
