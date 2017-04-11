using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Netotik.Web.Infrastructure.Helpers
{
    public enum ControlSize
    {
        small,
        medium,
        big
    }
    public static class BootstrapHelper
    {

        public static MvcHtmlString BootstrapDropDownFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, SelectList selectList, ControlSize size)
        {
            MvcHtmlString label = LabelExtensions.LabelFor(helper, expression, new { @class = "col-md-2 control-label" });
            MvcHtmlString select = SelectExtensions.DropDownListFor(helper, expression, selectList, new { @class = "form-control" });
            MvcHtmlString validation = ValidationExtensions.ValidationMessageFor(helper, expression, null, new { @class = "help-block" });
            StringBuilder innerHtml = new StringBuilder();
            innerHtml.Append(select);
            innerHtml.Append(validation);
            TagBuilder innerDiv = new TagBuilder("div");
            innerDiv.AddCssClass((size == ControlSize.big) ? "col-md-10" : (size == ControlSize.medium) ? "col-md-6" : "col-md-3");
            innerDiv.InnerHtml = innerHtml.ToString();
            TagBuilder midleDiv = new TagBuilder("div");
            midleDiv.AddCssClass("col-md-10");
            midleDiv.InnerHtml = innerDiv.ToString();
            StringBuilder outerHtml = new StringBuilder();
            outerHtml.Append(label);
            outerHtml.Append(midleDiv.ToString());
            TagBuilder outerDiv = new TagBuilder("div");
            outerDiv.AddCssClass("form-group");
            outerDiv.InnerHtml = outerHtml.ToString();
            return MvcHtmlString.Create(outerDiv.ToString());
        }
    }
}