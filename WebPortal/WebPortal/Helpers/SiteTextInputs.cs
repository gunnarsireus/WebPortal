using System;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace WebPortal.Helpers
{
    public static class SiteTextInputs
    {
        public const int    NOMAX   = -1;
        public const string NOVALUE = "";

        private const string BOOTSTRAP_MARGIN = "style=\"margin-left:-15px;margin-right:15px;\"";

        public enum GlyphIcon
        {
            APARTMENT, CALENDAR, COMPANY, DESCRIPTION, EMAIL, FLOOR, LABEL, LOCATION, PASSWORD, PHONE, REFERENCE, USER, TITLE
        }

        public enum StringType
        {
            TEXT, NUMBER, DECIMAL, PASSWORD, PW_VALIDATE, PHONE, ORGNUMBER
        }

        public static IHtmlString SiteStringEditor(
            this HtmlHelper helper,
            string          labeltext,
            int             labelcols,
            string          id,
            StringType      type,
            int             minlength,
            int             maxlength,
            GlyphIcon       icon,
            bool            isautoajax = true,
            string          defaultvalue = ""
            )
        {
            return SiteStringInput(helper, labeltext.ToString(), labelcols, id, type, minlength, maxlength, icon, false, isautoajax, defaultvalue);
        }

        public static IHtmlString SiteStringDisplay(
            this HtmlHelper helper,
            string          labeltext,
            int             labelcols,
            string          id,
            StringType      type,
            GlyphIcon       icon,
            bool            isautoajax = true,
            string          defaultvalue = ""
            )
        {
            return SiteStringInput(helper, labeltext.ToString(), labelcols, id, type, 0, NOMAX, icon, true, isautoajax, defaultvalue);
        }

        public static IHtmlString SiteNumberHidden(
            this HtmlHelper helper,
            string          id,
            long            value,
            bool            isautoajax = true
            )
        {
            string number = value.ToString();
            return SiteStringHidden(helper, id, number, isautoajax);
        }

        public static IHtmlString SiteStringHidden(
            this HtmlHelper helper,
            string          id,
            string          value,
            bool            isautoajax = true
            )
        {
            string autoajax  = isautoajax ? "true" : "false";
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<div class=\"form-group\">");
            sb.AppendLine("<div class=\"input-group\">");
            sb.AppendLine("<input data-autoajax=\"" + autoajax + "\"");
            sb.AppendLine("id=\"" + id + "\"");
            sb.AppendLine("name=\"" + id + "\"");
            sb.AppendLine("type=\"hidden\"");
            sb.AppendLine("value=\"" + value + "\"/>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            return new MvcHtmlString(sb.ToString());
        }

        private static IHtmlString SiteStringInput(
            this HtmlHelper helper,
            string          labeltext,
            int             labelcols,
            string          id,
            StringType      type,
            int             minlength,
            int             maxlength,
            GlyphIcon       icon,
            bool            isreadonly,
            bool            isautoajax,
            string          defaultvalue = ""
            )
        {
            string autoajax  = isautoajax ? "true" : "false";
            string glyph     = Icon2Glyph(icon);
            string texttype  = Type2String(type);
            string javacode  = Type2JavaScript(type);
            int    inputcols = 12 - labelcols;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<div class=\"form-group\">");
            sb.AppendLine("<label class=\"control-label col-md-" + labelcols + "\" " + BOOTSTRAP_MARGIN + " for=\"" + id + "\">" + labeltext + "</label>");
            sb.AppendLine("<div class=\"col-md-" + inputcols + " input-group\">");
            sb.AppendLine("<span class=\"input-group-addon\">");
            sb.AppendLine("<span class=\"" + glyph + "\"></span>");
            sb.AppendLine("</span>");
            sb.AppendLine("<input class=\"form-control\"");
            sb.AppendLine("data-autoajax=\"" + autoajax + "\"");
            if (!isreadonly && (minlength > 0 || maxlength != NOMAX))
            {
                sb.AppendLine("data-val=\"true\"");
                if (minlength > 0 && maxlength == NOMAX)
                {
                    sb.AppendLine("data-val-length-min=\"" + minlength + "\"");
                    sb.AppendLine("data-val-required=\"" + labeltext + " får inte vara tom\"");
                }
                else if (minlength == 0 && maxlength != NOMAX)
                {
                    sb.AppendLine("data-val-length-max=\"" + maxlength + "\"");
                    sb.AppendLine("data-val-length=\"" + labeltext + " får inte överstiga " + maxlength + " tecken\"");
                }
                else
                {
                    sb.AppendLine("data-val-length-min=\"" + minlength + "\"");
                    sb.AppendLine("data-val-length-max=\"" + maxlength + "\"");
                    sb.AppendLine("data-val-length=\"" + labeltext + " måste vara mellan " + minlength + " och " + maxlength + " tecken\"");
                    sb.AppendLine("data-val-required=\"" + labeltext + " får inte vara tom\"");
                }
            }
            sb.AppendLine("id=\"" + id + "\"");
            sb.AppendLine("name=\"" + id + "\"");
            if (!isreadonly && javacode != null)
            {
                sb.AppendLine(javacode);
            }
            sb.AppendLine("type=\"" + texttype + "\"");
            sb.AppendLine("value=\"" + defaultvalue + "\" " + (isreadonly ? "readonly":"") + "/>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            return new MvcHtmlString(sb.ToString());
        }

        private static string Icon2Glyph(GlyphIcon icon)
        {
            switch (icon)
            {
                case GlyphIcon.APARTMENT:   return "glyphicon glyphicon-home";
                case GlyphIcon.CALENDAR:    return "glyphicon glyphicon-calendar";
                case GlyphIcon.COMPANY:     return "glyphicon glyphicon-th-list";
                case GlyphIcon.DESCRIPTION: return "glyphicon glyphicon-list-alt";
                case GlyphIcon.EMAIL:       return "glyphicon glyphicon-envelope";
                case GlyphIcon.FLOOR:       return "glyphicon glyphicon-align-justify";
                case GlyphIcon.LABEL:       return "glyphicon glyphicon-bookmark";
                case GlyphIcon.LOCATION:    return "glyphicon glyphicon-map-marker";
                case GlyphIcon.PASSWORD:    return "glyphicon glyphicon-lock";
                case GlyphIcon.PHONE:       return "glyphicon glyphicon-phone-alt";
                case GlyphIcon.REFERENCE:   return "glyphicon glyphicon-retweet";
                case GlyphIcon.USER:        return "glyphicon glyphicon-user";
                case GlyphIcon.TITLE:       return "glyphicon glyphicon-header";
                default:                    return "";
            }
        }

        private static string Type2String(StringType type)
        {
            switch (type)
            {
                case StringType.TEXT:             return "text";
                case StringType.NUMBER:           return "text";
                case StringType.DECIMAL:          return "text";
                case StringType.PASSWORD:         return "password";
                case StringType.PW_VALIDATE:      return "password";
                case StringType.PHONE:            return "text";
                case StringType.ORGNUMBER:        return "text";
                default:                          return "";
            }
        }

        private static string Type2JavaScript(StringType type)
        {
            switch (type)
            {
                case StringType.TEXT:             return null;
                case StringType.NUMBER:           return "onkeypress = \"return Site.Validation.isNumber(event)\"";
                case StringType.DECIMAL:          return "onkeypress = \"return Site.Validation.isDecimal(event)\"";
                case StringType.PW_VALIDATE:      return "onkeyup    = \"return Site.Validation.isSecurePassword(event)\"";
                case StringType.PHONE:            return "onkeypress = \"return Site.Validation.isPhoneNumber(event)\"";
                case StringType.ORGNUMBER:        return "onkeypress = \"return Site.Validation.isOrgNumber(event)\"";
                default:                          return null;
            }
        }
    }
}

