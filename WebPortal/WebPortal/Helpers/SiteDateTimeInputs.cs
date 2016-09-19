using System;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace WebPortal.Helpers
{
    public static class SiteDateTimeInputs
    {
        public enum CalendarType
        {
            PAST, FREE, FUTURE
        }

        private const  string BOOTSTRAP_MARGIN = "margin-left:-15px;margin-right:15px;";
        private static int    containernumber  = 0;

        public static IHtmlString SiteDateTimeInput(
            this HtmlHelper helper,
            string          labeltext,
            int             labelcols,
            string          dateid,
            string          timeid,
            CalendarType    type,
            bool            isautoajax = true
            )
        {
            DateTime now = DateTime.Now;

            string containerid = "date-container-for-" + dateid + "-" + containernumber++;
            string autoajax    = isautoajax ? "true" : "false";
            string todaydate   = now.ToString(@"yyyy-MM-dd");
            string todaytime   = now.ToString(@"HH:mm");
            int    inputcols   = 12 - labelcols;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<div class=\"form-group\">");
            sb.AppendLine("<label class=\"control-label col-md-" + labelcols + "\" style=\"" + BOOTSTRAP_MARGIN + "\" for=\"" + dateid + "\">" + labeltext + "</label>");
            sb.AppendLine("<div class=\"col-md-" + inputcols + " input-group date clockpicker\" id=\"" + containerid + "\">");
            sb.AppendLine("<span class=\"input-group-addon\">");
            sb.AppendLine("<span class=\"glyphicon glyphicon-calendar\"></span>");
            sb.AppendLine("</span>");
            sb.AppendLine("<input class=\"form-control\"");
            sb.AppendLine("data-autoajax=\"" + autoajax + "\"");
            sb.AppendLine("data-date=\"" + todaydate + "\"");
            sb.AppendLine("data-date-autoclose=\"true\"");
            sb.AppendLine("data-date-calendar-weeks=\"true\"");
            sb.AppendLine("data-date-container=\"#" + containerid + "\"");
            sb.AppendLine("data-date-format=\"yyyy-mm-dd\"");
            sb.AppendLine("data-date-language=\"sv\"");
            sb.AppendLine("data-date-orientation=\"bottom left\"");
            if (type == CalendarType.PAST)
            {
                sb.AppendLine("data-date-end-date=\"" + todaydate + "\"");
            }
            else if (type == CalendarType.FUTURE)
            {
                sb.AppendLine("data-date-start-date=\"" + todaydate + "\"");
            }
            sb.AppendLine("data-date-today-btn=\"linked\"");
            sb.AppendLine("data-date-today-highlight=\"true\"");
            sb.AppendLine("data-date-week-start=\"1\"");
            sb.AppendLine("data-provide=\"datepicker\"");
            sb.AppendLine("data-val=\"true\"");
            sb.AppendLine("data-val-required=\"Datum får inte vara tomt\"");
            sb.AppendLine("id=\"" + dateid + "\"");
            sb.AppendLine("name=\"" + dateid + "\"");
            sb.AppendLine("type=\"text\"");
            sb.AppendLine("value=\"\" />");
            sb.AppendLine("<span class=\"input-group-btn\" style=\"width:0px;\"></span>");
            sb.AppendLine("<input class=\"form-control\" ");
            sb.AppendLine("data-autoajax=\"true\" ");
            sb.AppendLine("data-val=\"true\" ");
            sb.AppendLine("data-val-required=\"Klockslag får inte vara tomt\"");
            sb.AppendLine("id=\"" + timeid + "\"");
            sb.AppendLine("name=\"" + timeid + "\"");
            sb.AppendLine("type=\"text\"");
            sb.AppendLine("placeholder=\"" + todaytime + "\"");
            sb.AppendLine("value=\"\" />");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            return new MvcHtmlString(sb.ToString());
        }

        public static IHtmlString SiteDateInput(
            this HtmlHelper helper,
            string          labeltext,
            int             labelcols,
            string          dateid,
            CalendarType    type,
            bool            isautoajax = true
            )
        {
            DateTime now = DateTime.Now;

            string containerid = "date-container-for-" + dateid + "-" + containernumber++;
            string autoajax = isautoajax ? "true" : "false";
            string todaydate = now.ToString(@"yyyy-MM-dd");
            int inputcols = 12 - labelcols;

            TagBuilder formgroup = new TagBuilder("div");
            formgroup.AddCssClass("form-group");
                TagBuilder label = new TagBuilder("label");
                label.AddCssClass("control-label col-md-" + labelcols);
                label.Attributes.Add("style", BOOTSTRAP_MARGIN);
                label.Attributes.Add("for", dateid);
                label.SetInnerText(labeltext);

                TagBuilder inputgroup = new TagBuilder("div");
                inputgroup.AddCssClass("col-md-" + inputcols + " input-group date clockpicker");
                inputgroup.Attributes.Add("id", containerid);
                    TagBuilder iconaddon = new TagBuilder("span");
                    iconaddon.AddCssClass("input-group-addon");
                        TagBuilder glyph = new TagBuilder("span");
                        glyph.AddCssClass("glyphicon glyphicon-calendar");
                    iconaddon.InnerHtml = glyph.ToString();

                    TagBuilder formcontrol = new TagBuilder("input");
                    formcontrol.AddCssClass("form-control");
                    formcontrol.Attributes.Add("data-autoajax",autoajax.ToString());
                    formcontrol.Attributes.Add("data-date", todaydate);
                    formcontrol.Attributes.Add("data-date-autoclose", "true");
                    formcontrol.Attributes.Add("data-date-calendar-weeks", "true");
                    formcontrol.Attributes.Add("data-date-container", "#" + containerid);
                    formcontrol.Attributes.Add("data-date-format","yyyy-mm-dd");
                    formcontrol.Attributes.Add("data-date-language","sv");
                    formcontrol.Attributes.Add("data-date-orientation","bottom-left");
                    if(type == CalendarType.PAST)
                    {
                        formcontrol.Attributes.Add("data-date-end-date",todaydate);
                    }
                    else if(type == CalendarType.FUTURE)
                    {
                        formcontrol.Attributes.Add("data-date-start-date", todaydate);
                    }
                    formcontrol.Attributes.Add("data-date-today-btn", "linked");
                    formcontrol.Attributes.Add("data-date-today-highlight", "true");
                    formcontrol.Attributes.Add("data-date-week-start", "1");
                    formcontrol.Attributes.Add("data-provide", "datepicker");
                    formcontrol.Attributes.Add("data-val", "true");
                    formcontrol.Attributes.Add("data-val-required", "Datum får inte vara tomt");
                    formcontrol.Attributes.Add("id", dateid);
                    formcontrol.Attributes.Add("name", dateid);
                    formcontrol.Attributes.Add("type", "text");
                    formcontrol.Attributes.Add("value", "");
                inputgroup.InnerHtml = iconaddon.ToString() + formcontrol.ToString();
            formgroup.InnerHtml = label.ToString() + inputgroup.ToString();

            return new MvcHtmlString(formgroup.ToString());
        }

        public static IHtmlString SiteTimeInput(
            this HtmlHelper helper,
            string          labeltext,
            int             labelcols,
            string          timeid,
            bool            isautoajax = true
            )
        {
            DateTime now = DateTime.Now;

            string containerid = "date-container-for-" + timeid + "-" + containernumber++;
            string autoajax = isautoajax ? "true" : "false";
            string todaytime = now.ToString(@"HH:mm");
            int inputcols = 12 - labelcols;

            TagBuilder formgroup = new TagBuilder("div");
            formgroup.AddCssClass("form-group");
                TagBuilder label = new TagBuilder("label");
                label.AddCssClass("control-label col-md-" + labelcols);
                label.Attributes.Add("style", BOOTSTRAP_MARGIN);
                label.Attributes.Add("for", timeid);
                label.SetInnerText(labeltext);

                TagBuilder inputgroup = new TagBuilder("div");
                inputgroup.AddCssClass("col-md-" + inputcols + " input-group date clockpicker");
                inputgroup.Attributes.Add("id", containerid);
                    TagBuilder iconaddon = new TagBuilder("span");
                    iconaddon.AddCssClass("input-group-addon");
                    TagBuilder glyph = new TagBuilder("span");
                    glyph.AddCssClass("glyphicon glyphicon-calendar");
                    iconaddon.InnerHtml = glyph.ToString();

                    TagBuilder clockbutton = new TagBuilder("span");
                    clockbutton.AddCssClass("input-group-btn");
                    clockbutton.Attributes.Add("style", "width:0px;");

                    TagBuilder clockinput = new TagBuilder("input");
                    clockinput.AddCssClass("form-control");
                    clockinput.Attributes.Add("data-autoajax", "true");
                    clockinput.Attributes.Add("data-val","true");
                    clockinput.Attributes.Add("data-val-required","Klockslag får inte vara tomt");
                    clockinput.Attributes.Add("id",timeid);
                    clockinput.Attributes.Add("name",timeid);
                    clockinput.Attributes.Add("type","text");
                    clockinput.Attributes.Add("placeholder",todaytime);
                    clockinput.Attributes.Add("value","");
                inputgroup.InnerHtml = iconaddon.ToString() + clockbutton.ToString() + clockinput.ToString();
            formgroup.InnerHtml = label.ToString() + inputgroup.ToString();

            return new MvcHtmlString(formgroup.ToString());
        }

        public static IHtmlString SiteYearMonthInput(
            this HtmlHelper helper,
            string          labeltext,
            int             labelcols,
            string          dateid,
            string          timeid,
            CalendarType    type,
            bool            isautoajax = true
            )
        {
            DateTime now = DateTime.Now;

            string containerid = "date-container-for-" + dateid + "-" + containernumber++;
            string autoajax    = isautoajax ? "true" : "false";
            string todaydate   = now.ToString(@"yyyy-MM");
            int    inputcols   = 12 - labelcols;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<div class=\"form-group\">");
            sb.AppendLine("<label class=\"control-label col-md-" + labelcols + "\" for=\"" + dateid + "\">" + labeltext + "</label>");
            sb.AppendLine("<div class=\"col-md-" + inputcols + " input-group date clockpicker\" id=\"" + containerid + "\">");
            sb.AppendLine("<span class=\"input-group-addon\">");
            sb.AppendLine("<span class=\"glyphicon glyphicon-calendar\"></span>");
            sb.AppendLine("</span>");
            sb.AppendLine("<input class=\"form-control\"");
            sb.AppendLine("data-autoajax=\"" + autoajax + "\"");
            sb.AppendLine("data-date=\"" + todaydate + "\"");
            sb.AppendLine("data-date-autoclose=\"true\"");
            sb.AppendLine("data-date-calendar-weeks=\"true\"");
            sb.AppendLine("data_date_start_view = \"months\"");
            sb.AppendLine("data_date_min_view_mode = \"months\"");
            sb.AppendLine("data-date-container=\"#" + containerid + "\"");
            sb.AppendLine("data-date-format=\"yyyy-mm\"");
            sb.AppendLine("data-date-language=\"sv\"");
            sb.AppendLine("data-date-orientation=\"bottom left\"");
            if (type == CalendarType.PAST)
            {
                sb.AppendLine("data-date-end-date=\"" + todaydate + "\"");
            }
            else if (type == CalendarType.FUTURE)
            {
                sb.AppendLine("data-date-start-date=\"" + todaydate + "\"");
            }
            sb.AppendLine("data-date-today-btn=\"linked\"");
            sb.AppendLine("data-date-today-highlight=\"true\"");
            sb.AppendLine("data-date-week-start=\"1\"");
            sb.AppendLine("data-provide=\"datepicker\"");
            sb.AppendLine("data-val=\"true\"");
            sb.AppendLine("data-val-required=\"Datum får inte vara tomt\"");
            sb.AppendLine("id=\"" + dateid + "\"");
            sb.AppendLine("name=\"" + dateid + "\"");
            sb.AppendLine("type=\"text\"");
            sb.AppendLine("value=\"\" />");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            return new MvcHtmlString(sb.ToString());
        }
    }
}
