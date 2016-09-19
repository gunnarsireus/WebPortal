using System;
using System.Text;
using System.Collections.Generic;
using System.Web.Mvc;

namespace WebPortal.Helpers
{
    public static class SiteSelect2Inputs
    {
        public const string BOOTSTRAP_MARGIN = "margin-left:-15px;margin-right:15px;";

        // With label
        public static MvcHtmlString SiteSelect2(
            this HtmlHelper       helper,
            string                labeltext,
            int                   labelcols,
            string                id,
            IList<SelectListItem> options,
            bool                  isautoajax = true
            )
        {
            string autoajax = isautoajax ? "true" : "false";
            int inputcols = 12 - labelcols;

            TagBuilder form = new TagBuilder("div");
            form.AddCssClass("form-group");
                TagBuilder label = new TagBuilder("label");
                label.AddCssClass("control-label col-md-" + labelcols);
                label.Attributes.Add("for", id);
                label.Attributes.Add("style",BOOTSTRAP_MARGIN);
                label.SetInnerText(labeltext);

                TagBuilder input = new TagBuilder("div");
                input.AddCssClass("col-md-" + inputcols + " input-group");
                    TagBuilder inputaddon = new TagBuilder("span");
                    inputaddon.AddCssClass("input-group-addon");
                        TagBuilder icon = new TagBuilder("span");
                        icon.AddCssClass("glyphicon glyphicon-unchecked");
                    inputaddon.InnerHtml += icon;

                    TagBuilder select = new TagBuilder("select");
                    select.Attributes.Add("data-autoajax", autoajax);
                    select.Attributes.Add("data-width", "100%");
                    select.Attributes.Add("id", id);
                    select.Attributes.Add("name", id);
                    if (options != null)
                    {
                        StringBuilder optionssb = new StringBuilder();
                        foreach (SelectListItem option in options)
                        {
                            TagBuilder o = new TagBuilder("option");
                            o.Attributes.Add("value", option.Value);
                            o.InnerHtml = option.Text;
                            optionssb.AppendLine(o.ToString());
                        }
                        select.InnerHtml = optionssb.ToString();
                    }
                input.InnerHtml = inputaddon.ToString() + select.ToString();
            form.InnerHtml = label.ToString() + input.ToString();
            return new MvcHtmlString(form.ToString());
        }

        // Plain without label
        public static MvcHtmlString SiteSelect2(
            this HtmlHelper       helper,
            string                id,
            IList<SelectListItem> options,
            bool                  isautoajax = true
            )
        {
            string autoajax = isautoajax ? "true" : "false";

            TagBuilder select = new TagBuilder("select");
            select.Attributes.Add("data-autoajax", autoajax);
            select.Attributes.Add("data-width", "100%");
            select.Attributes.Add("id", id);
            select.Attributes.Add("name", id);
            select.AddCssClass("site-select2-roundedcorners"); // site-xcomp-select2 searches for this to set border radius
            if (options != null)
            {
                StringBuilder optionssb = new StringBuilder();
                foreach (SelectListItem option in options)
                {
                    TagBuilder o = new TagBuilder("option");
                    o.Attributes.Add("value", option.Value);
                    o.InnerHtml = option.Text;
                    optionssb.AppendLine(o.ToString());
                }
                select.InnerHtml = optionssb.ToString();
            }
            return new MvcHtmlString(select.ToString());
        }
    }
}