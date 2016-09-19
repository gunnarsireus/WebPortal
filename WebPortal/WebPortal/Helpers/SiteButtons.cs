using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Web.Mvc;

namespace WebPortal.Helpers
{
    public static class SiteButtons
    {
        public static MvcHtmlString SiteButton(this HtmlHelper helper, string caption, Enums.ButtonStyle style, Enums.ButtonSize size)
        {
            string format = "<button type=\"button\" class=\"btn btn-{0} {1}\">{2}</button>";
            return new MvcHtmlString(string.Format(format, style.ToString().ToLower(), ToBootstrapSize(size), caption));
        }

        public static MvcHtmlString SiteButton(this HtmlHelper helper, string id, string caption, Enums.ButtonStyle style, Enums.ButtonSize size)
        {
            string format = "<button id=\"{0}\" type=\"button\" class=\"btn btn-{1} {2}\">{3}</button>";
            return new MvcHtmlString(string.Format(format, id, style.ToString().ToLower(), ToBootstrapSize(size), caption));
        }

        public static MvcHtmlString SiteButton(this HtmlHelper helper, string id, string caption, Enums.ButtonPosition position, Enums.ButtonStyle style, Enums.ButtonSize size)
        {
            string format = "<button id=\"{0}\" type=\"button\" class=\"btn btn-{1} {2} {3}\">{4}</button>";
            return new MvcHtmlString(string.Format(format, id, style.ToString().ToLower(), ToBootstrapPosition(position), ToBootstrapSize(size), caption));
        }

        private static string ToBootstrapPosition(Enums.ButtonPosition position)
        {
            switch (position)
            {
                case Enums.ButtonPosition.Left:
                    return "pull-left";
                case Enums.ButtonPosition.Center:
                    return "";
                case Enums.ButtonPosition.Right:
                    return "pull-right";
                default:
                    return "";
            }
        }

        private static string ToBootstrapSize(Enums.ButtonSize size)
        {
            switch (size)
            {
                case Enums.ButtonSize.Large:
                    return "btn-lg";
                case Enums.ButtonSize.Small:
                    return "btn-sm";
                case Enums.ButtonSize.ExtraSmall:
                    return "btn-xs";
                default:
                    return "";
            }
        }
    }
}
