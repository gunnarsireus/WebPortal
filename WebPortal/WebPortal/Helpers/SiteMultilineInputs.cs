using System;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace WebPortal.Helpers
{
    public static class SiteMultilineInputs
    {
        public const int    NOMAX   = -1;
        public const string NOVALUE = "";

        private const string BOOTSTRAP_MARGIN = "style=\"margin-left:-15px;margin-right:15px;\"";

        public static IHtmlString SiteMultilineEditor(
            this HtmlHelper helper,
            string          labeltext,
            int             labelcols,
            string          id,
            int             minlength,
            int             maxlength,
            int             rows,
            bool            isautoajax = true
            )
        {
            return SiteMultilineInput(helper, labeltext.ToString(), labelcols, id, minlength, maxlength, rows, false, isautoajax);
        }

        public static IHtmlString SiteMultilineDisplay(
            this HtmlHelper helper,
            string          labeltext,
            int             labelcols,
            string          id,
            int             rows,
            bool            isautoajax = true
            )
        {
            return SiteMultilineInput(helper, labeltext.ToString(), labelcols, id, 0, NOMAX, rows, true, isautoajax);
        }

        private static IHtmlString SiteMultilineInput(
            this HtmlHelper helper,
            string          labeltext,
            int             labelcols,
            string          id,
            int             minlength,
            int             maxlength,
            int             rows,
            bool            isreadonly,
            bool            isautoajax
            )
        {
            string autoajax  = isautoajax ? "true" : "false";
            int    inputcols = 12 - labelcols;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<div class=\"form-group\">");
            sb.AppendLine("<label class=\"control-label col-md-" + labelcols + "\" " + BOOTSTRAP_MARGIN + " for=\"" + id + "\">" + labeltext + "</label>");
            sb.AppendLine("<div class=\"col-md-" + inputcols + " input-group\">");
            sb.AppendLine("<span class=\"input-group-addon\">");
            sb.AppendLine("<span class=\"glyphicon glyphicon-pencil\"></span>");
            sb.AppendLine("</span>");
            sb.AppendLine("<textarea class=\"form-control\" rows=\"" + rows + "\"");
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
            sb.AppendLine((isreadonly ? "readonly":"") + "/></textarea>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            return new MvcHtmlString(sb.ToString());
        }
    }
}

