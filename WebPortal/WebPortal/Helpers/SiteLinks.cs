using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Web.Mvc;

namespace WebPortal.Helpers
{
    public static class SiteLinks
    {
        public static MvcHtmlString SiteLink(this HtmlHelper helper, string id, string caption, string classes, Enums.LinkPosition position)
        {
            string format = "<div id=\"{0}\" class=\"text-info {1} {2}\" style=\"cursor:pointer;\">{3}</div>";
            return new MvcHtmlString(string.Format(format, id, ToBootstrapPosition(position), classes, caption));
        }

        private static string ToBootstrapPosition(Enums.LinkPosition position)
        {
            switch (position)
            {
                case Enums.LinkPosition.Left:
                    return "text-left";
                case Enums.LinkPosition.Center:
                    return "text-center";
                case Enums.LinkPosition.Right:
                    return "text-right";
                default:
                    return "";
            }
        }
    }
}
