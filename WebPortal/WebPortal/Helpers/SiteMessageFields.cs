using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Web.Mvc;

namespace WebPortal.Helpers
{
    public static class SiteMessageFields
    {
       public static MvcHtmlString SiteErrorField(this HtmlHelper helper, string id, Enums.MessageFieldStyle style)
       {
            string format = "<div id=\"{0}\" class=\"alert alert-{1}\" hidden></div>";
            return new MvcHtmlString(string.Format(format, id, style.ToString().ToLower()));
        }
    }
}