using System;
using System.Text;
using System.Collections.Generic;
using System.Web.Mvc;

namespace WebPortal.Helpers
{
    public static class SiteSelectDual
    {
        public static MvcHtmlString SiteDualListbox(this HtmlHelper helper, string id, int size)
        {
            StringBuilder builder = new StringBuilder();
            var select = new TagBuilder("select");
            select.Attributes.Add("id", id);
            select.Attributes.Add("multiple", "multiple");
            select.Attributes.Add("size", size.ToString());
            builder.AppendLine(select.ToString(TagRenderMode.StartTag));
            builder.AppendLine(select.ToString(TagRenderMode.EndTag));
            return new MvcHtmlString(builder.ToString());
        }
    }
}