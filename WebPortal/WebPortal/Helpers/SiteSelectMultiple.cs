using System;
using System.Text;
using System.Collections.Generic;
using System.Web.Mvc;

/*
 * Use together with JS component site-xcomp-selectmultiple.js
 * 
 * Example:
 * 
 *   <div class="col-md-9 input-group">
 *     @Html.BootstrapSpan(Enums.SpanType.InputGroupAddon, Enums.SpanStyle.User)
 *     @Html.BootstrapSelectMultipleList("foremen", (IEnumerable<SelectListItem>)ViewBag.ForemanOptions)
 *   </div>
 */

namespace WebPortal.Helpers
{
    public static class SiteSelectMultiple
    {
        public static MvcHtmlString SiteSelectMultipleList(this HtmlHelper helper, string id, IEnumerable<SelectListItem> items)
        {
            StringBuilder builder = new StringBuilder();

            var select = new TagBuilder("select");
            select.AddCssClass("selectmultiplepicker");
            select.Attributes.Add("id", id);
            select.Attributes.Add("name", id);
            select.Attributes.Add("multiple", "multiple");
            select.Attributes.Add("data-autoajax", "false");
            builder.AppendLine(select.ToString(TagRenderMode.StartTag));

            if (items != null)
            {
                foreach (SelectListItem item in items)
                {
                    var option = new TagBuilder("option");
                    option.Attributes.Add("value", item.Value);
                    option.InnerHtml = item.Text;
                    if (item.Selected)
                    {
                        option.Attributes.Add("selected", "selected");
                    }
                    builder.AppendLine(option.ToString(TagRenderMode.Normal));
                }
            }

            builder.AppendLine(select.ToString(TagRenderMode.EndTag));
            return new MvcHtmlString(builder.ToString());
        }
    }
}