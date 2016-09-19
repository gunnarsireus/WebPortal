using System;
using System.IO;
using System.Web.Mvc;

namespace WebPortal.Helpers
{
    public static class SitePanels
    {
        public static Panel SitePanel(this HtmlHelper html, string title, Enums.PanelStyle style=Enums.PanelStyle.Default)
        {
            return new Panel(html, title, null, null, null, Enums.PanelPadding.Default, style);
        }

        public static Panel SitePanel(this HtmlHelper html, string title, Enums.PanelPadding padding, Enums.PanelStyle style = Enums.PanelStyle.Default)
        {
            return new Panel(html, title, null, null, null, padding, style);
        }

        public static Panel SitePanel(this HtmlHelper html, string title, string classname, string id, string icon, Enums.PanelStyle style = Enums.PanelStyle.Default)
        {
            return new Panel(html, title, classname, id, icon, Enums.PanelPadding.Default, style);
        }

        public static Panel SitePanel(this HtmlHelper html, string title, string classname, string id, string icon, Enums.PanelPadding padding, Enums.PanelStyle style = Enums.PanelStyle.Default)
        {
            return new Panel(html, title, classname, id, icon, padding, style);
        }

        public class Panel : IDisposable
        {
            private readonly TextWriter _writer;

            public Panel(HtmlHelper helper, string title, string classname, string id, string icon, Enums.PanelPadding padding, Enums.PanelStyle style = Enums.PanelStyle.Default)
            {
                _writer = helper.ViewContext.Writer;

                var paneldiv = new TagBuilder("div");
                paneldiv.AddCssClass("panel-" + style.ToString().ToLower());
                paneldiv.AddCssClass("panel");

                var panelheadingdiv = new TagBuilder("div");
                panelheadingdiv.AddCssClass("panel-heading");

                TagBuilder paneltitle = null;
                if (classname != null && id != null && icon != null)
                {
                    paneltitle = new TagBuilder("div");
                    paneltitle.AddCssClass("row");
                    var leftcolumn = new TagBuilder("div");
                    leftcolumn.AddCssClass("col-xs-10");
                    var heading = new TagBuilder("h3");
                    heading.AddCssClass("panel-title");
                    heading.SetInnerText(title);
                    leftcolumn.InnerHtml = heading.ToString();
                    var rightcolumn = new TagBuilder("div");
                    rightcolumn.AddCssClass("col-xs-2");
                    var anchor = new TagBuilder("a");
                    anchor.AddCssClass(classname);
                    anchor.AddCssClass("pull-right");
                    anchor.Attributes.Add("id", id);
                    anchor.Attributes.Add("href", "#");
                    var image = new TagBuilder("img");
                    image.Attributes.Add("src", icon);
                    anchor.InnerHtml = image.ToString();
                    rightcolumn.InnerHtml = anchor.ToString();
                    paneltitle.InnerHtml = leftcolumn.ToString() + rightcolumn.ToString();
                }
                else
                {
                    paneltitle = new TagBuilder("h3");
                    paneltitle.AddCssClass("panel-title");
                    paneltitle.SetInnerText(title);
                }

                var panelbodydiv = new TagBuilder("div");
                panelbodydiv.AddCssClass("panel-body");
                if (padding == Enums.PanelPadding.NoHorizontal)
                {
                    panelbodydiv.AddCssClass("site-no-horizontal-padding");
                }
                else if (padding == Enums.PanelPadding.NoVertical)
                {
                    panelbodydiv.AddCssClass("site-no-vertical-padding");
                }
                else if (padding == Enums.PanelPadding.None)
                {
                    panelbodydiv.AddCssClass("site-no-padding");
                }

                panelheadingdiv.InnerHtml = paneltitle.ToString();

                string html = string.Format("{0}{1}{2}", paneldiv.ToString(TagRenderMode.StartTag), panelheadingdiv, panelbodydiv.ToString(TagRenderMode.StartTag));
                _writer.Write(html);
            }

            public void Dispose()
            {
                _writer.Write("</div></div>");
            }
        }
    }
}
