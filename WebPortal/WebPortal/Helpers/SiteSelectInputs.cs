using System;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;

using ServerLibrary.Model;

namespace WebPortal.Helpers
{
    public static class SiteSelectInputs
    {
        private const string BOOTSTRAP_MARGIN = "style=\"margin-left:-15px;margin-right:15px;\"";

        public enum GlyphIcon
        {
            COMPANY, YESNO, PERMISSION, CALENDAR, STATUS, PRIO, AREATYPE, CATEGORY, USER
        }

        public enum SelectType
        {
            MANDATORY, NONEABLE
        }

        // Customer > Type (label)
        public static IHtmlString SiteSelectCustomerType(
            this HtmlHelper       helper,
            string                labeltext,
            int                   labelcols,
            string                id,
            SelectType            type,
            bool                  isautoajax = true)
        {
            IList<SelectListItem> options = GetCustomerTypeOptions();
            return SiteSelect(helper, labeltext, labelcols, id, type, options, GlyphIcon.CATEGORY, isautoajax);
        }

        // Customer > Type (plain)
        public static IHtmlString SiteSelectCustomerType(
            this HtmlHelper       helper,
            string                id,
            SelectType            type,
            bool                  isautoajax = true)
        {
            IList<SelectListItem> options = GetCustomerTypeOptions();
            return SiteSelect(helper, id, type, options, isautoajax);
        }

        private static IList<SelectListItem> GetCustomerTypeOptions()
        {
            IList<SelectListItem> options = new List<SelectListItem>();
            options.Add(new SelectListItem { Text = Customer.STRING_TYPE_COMPANY, Value = Customer.TYPE_COMPANY.ToString() });
            options.Add(new SelectListItem { Text = Customer.STRING_TYPE_PERSON,  Value = Customer.TYPE_PERSON.ToString()  });
            return options;
        }

        // Account > Authz (label)
        public static IHtmlString SiteSelectAccountAuthz(
            this HtmlHelper       helper,
            string                labeltext,
            int                   labelcols,
            string                id,
            SelectType            type,
            bool                  isautoajax = true)
        {
            IList<SelectListItem> options = GetAccountAuthzOptions();
            return SiteSelect(helper, labeltext, labelcols, id, type, options, GlyphIcon.PERMISSION, isautoajax);
        }

        // Account > Authz (plain)
        public static IHtmlString SiteSelectAccountAuthz(
            this HtmlHelper       helper,
            string                id,
            SelectType            type,
            bool                  isautoajax = true)
        {
            IList<SelectListItem> options = GetAccountAuthzOptions();
            return SiteSelect(helper, id, type, options, isautoajax);
        }

        private static IList<SelectListItem> GetAccountAuthzOptions()
        {
            IList<SelectListItem> options = new List<SelectListItem>();
            options.Add(new SelectListItem { Text = Account.STRING_AUTHZ_ADMIN,      Value = Account.AUTHZ_ADMIN.ToString()      });
            options.Add(new SelectListItem { Text = Account.STRING_AUTHZ_MANAGEMENT, Value = Account.AUTHZ_MANAGEMENT.ToString() });
            options.Add(new SelectListItem { Text = Account.STRING_AUTHZ_TECHNICIAN, Value = Account.AUTHZ_TECHNICIAN.ToString() });
            options.Add(new SelectListItem { Text = Account.STRING_AUTHZ_CUSTOMER,   Value = Account.AUTHZ_CUSTOMER.ToString()   });
            options.Add(new SelectListItem { Text = Account.STRING_AUTHZ_RESIDENT,   Value = Account.AUTHZ_RESIDENT.ToString()   });
            return options;
        }

        // Account > Authz > Employee (label)
        public static IHtmlString SiteSelectAccountAuthzEmployee(
            this HtmlHelper       helper,
            string                labeltext,
            int                   labelcols,
            string                id,
            SelectType            type,
            bool                  isautoajax = true)
        {
            IList<SelectListItem> options = GetAccountAuthzEmployeeOptions();
            return SiteSelect(helper, labeltext, labelcols, id, type, options, GlyphIcon.PERMISSION, isautoajax);
        }

        // Account > Authz (plain)
        public static IHtmlString SiteSelectAccountAuthzEmployee(
            this HtmlHelper       helper,
            string                id,
            SelectType            type,
            bool                  isautoajax = true)
        {
            IList<SelectListItem> options = GetAccountAuthzEmployeeOptions();
            return SiteSelect(helper, id, type, options, isautoajax);
        }

        private static IList<SelectListItem> GetAccountAuthzEmployeeOptions()
        {
            IList<SelectListItem> options = new List<SelectListItem>();
            options.Add(new SelectListItem { Text = Account.STRING_AUTHZ_ADMIN,      Value = Account.AUTHZ_ADMIN.ToString()      });
            options.Add(new SelectListItem { Text = Account.STRING_AUTHZ_MANAGEMENT, Value = Account.AUTHZ_MANAGEMENT.ToString() });
            options.Add(new SelectListItem { Text = Account.STRING_AUTHZ_TECHNICIAN, Value = Account.AUTHZ_TECHNICIAN.ToString() });
            return options;
        }

        // Account > Authz > Resident (label)
        public static IHtmlString SiteSelectAccountAuthzResident(
            this HtmlHelper       helper,
            string                labeltext,
            int                   labelcols,
            string                id,
            SelectType            type,
            bool                  isautoajax = true)
        {
            IList<SelectListItem> options = GetAccountAuthzResidentOptions();
            return SiteSelect(helper, labeltext, labelcols, id, type, options, GlyphIcon.PERMISSION, isautoajax);
        }

        // Account > Authz > Resident (plain)
        public static IHtmlString SiteSelectAccountAuthzResident(
            this HtmlHelper       helper,
            string                id,
            SelectType            type,
            bool                  isautoajax = true)
        {
            IList<SelectListItem> options = GetAccountAuthzResidentOptions();
            return SiteSelect(helper, id, type, options, isautoajax);
        }

        private static IList<SelectListItem> GetAccountAuthzResidentOptions()
        {
            IList<SelectListItem> options = new List<SelectListItem>();
            options.Add(new SelectListItem { Text = Account.STRING_AUTHZ_CUSTOMER, Value = Account.AUTHZ_CUSTOMER.ToString() });
            options.Add(new SelectListItem { Text = Account.STRING_AUTHZ_RESIDENT, Value = Account.AUTHZ_RESIDENT.ToString() });
            return options;
        }

        // Issue > Status selections
        public enum IssueStatusSet
        {
            ALL, ONGOING, ARCHIVED
        }

        // Issue > Status (label)
        public static IHtmlString SiteSelectIssueStatus(
            this HtmlHelper       helper,
            string                labeltext,
            int                   labelcols,
            string                id,
            SelectType            type,
            IssueStatusSet        set,
            bool                  isautoajax = true)
        {
            IList<SelectListItem> options = GetIssueStatusOptions(set);
            return SiteSelect(helper, labeltext, labelcols, id, type, options, GlyphIcon.STATUS, isautoajax);
        }

        // Issue > Status (plain)
        public static IHtmlString SiteSelectIssueStatus(
            this HtmlHelper       helper,
            string                id,
            SelectType            type,
            IssueStatusSet        set,
            bool                  isautoajax = true)
        {
            IList<SelectListItem> options = GetIssueStatusOptions(set);
            return SiteSelect(helper, id, type, options, isautoajax);
        }

        private static IList<SelectListItem> GetIssueStatusOptions(IssueStatusSet set)
        {
            IList<SelectListItem> options = new List<SelectListItem>();
            if (set == IssueStatusSet.ALL || set == IssueStatusSet.ONGOING)
            {
                options.Add(new SelectListItem { Text = Issue.STRING_STATUS_PRELIMINARY, Value = Issue.STATUS_PRELIMINARY.ToString() });
                options.Add(new SelectListItem { Text = Issue.STRING_STATUS_OPEN,        Value = Issue.STATUS_OPEN.ToString()        });
                options.Add(new SelectListItem { Text = Issue.STRING_STATUS_STARTED,     Value = Issue.STATUS_STARTED.ToString()     });
                options.Add(new SelectListItem { Text = Issue.STRING_STATUS_CLOSED,      Value = Issue.STATUS_CLOSED.ToString()      });
                options.Add(new SelectListItem { Text = Issue.STRING_STATUS_APPROVED,    Value = Issue.STATUS_APPROVED.ToString()    });
            }
            if (set == IssueStatusSet.ALL || set == IssueStatusSet.ARCHIVED)
            {
                options.Add(new SelectListItem { Text = Issue.STRING_STATUS_FINISHED,    Value = Issue.STATUS_FINISHED.ToString()    });
                options.Add(new SelectListItem { Text = Issue.STRING_STATUS_INVOICED,    Value = Issue.STATUS_INVOICED.ToString()    });
                options.Add(new SelectListItem { Text = Issue.STRING_STATUS_REJECTED,    Value = Issue.STATUS_REJECTED.ToString()    });
            }
            return options;
        }

        // Issue > Prio (label)
        public static IHtmlString SiteSelectIssuePrio(
            this HtmlHelper       helper,
            string                labeltext,
            int                   labelcols,
            string                id,
            SelectType            type,
            bool                  isautoajax = true)
        {
            IList<SelectListItem> options = GetIssuePrioOptions();
            return SiteSelect(helper, labeltext, labelcols, id, type, options, GlyphIcon.PRIO, isautoajax);
        }

        // Issue > Prio (plain)
        public static IHtmlString SiteSelectIssuePrio(
            this HtmlHelper       helper,
            string                id,
            SelectType            type,
            bool                  isautoajax = true)
        {
            IList<SelectListItem> options = GetIssuePrioOptions();
            return SiteSelect(helper, id, type, options, isautoajax);
        }

        private static IList<SelectListItem> GetIssuePrioOptions()
        {
            IList<SelectListItem> options = new List<SelectListItem>();
            options.Add(new SelectListItem { Text = Issue.STRING_PRIO_LOW,    Value = Issue.PRIO_LOW.ToString()    });
            options.Add(new SelectListItem { Text = Issue.STRING_PRIO_NORMAL, Value = Issue.PRIO_NORMAL.ToString() });
            options.Add(new SelectListItem { Text = Issue.STRING_PRIO_HIGH,   Value = Issue.PRIO_HIGH.ToString()   });
            options.Add(new SelectListItem { Text = Issue.STRING_PRIO_URGENT, Value = Issue.PRIO_URGENT.ToString() });
            return options;
        }

        // Issue > Responsible (label)
        public static IHtmlString SiteSelectIssueResponsible(
            this HtmlHelper       helper,
            string                labeltext,
            int                   labelcols,
            string                id,
            SelectType            type,
            bool                  isautoajax = true)
        {
            IList<SelectListItem> options = GetIssueResponsibleOptions();
            return SiteSelect(helper, labeltext, labelcols, id, type, options, GlyphIcon.USER, isautoajax);
        }

        // Issue > Responsible (plain)
        public static IHtmlString SiteSelectIssueResponsible(
            this HtmlHelper       helper,
            string                id,
            SelectType            type,
            bool                  isautoajax = true)
        {
            IList<SelectListItem> options = GetIssueResponsibleOptions();
            return SiteSelect(helper, id, type, options, isautoajax);
        }

        private static IList<SelectListItem> GetIssueResponsibleOptions()
        {
            IList<SelectListItem> options = new List<SelectListItem>();
            options.Add(new SelectListItem { Text = Issue.STRING_RESPONSIBLE_INTERNAL, Value = Issue.RESPONSIBLE_INTERNAL.ToString() });
            options.Add(new SelectListItem { Text = Issue.STRING_RESPONSIBLE_EXTERNAL, Value = Issue.RESPONSIBLE_EXTERNAL.ToString() });
            return options;
        }

        // Issue > AreaType (label)
        public static IHtmlString SiteSelectIssueAreaType(
            this HtmlHelper       helper,
            string                labeltext,
            int                   labelcols,
            string                id,
            SelectType            type,
            bool                  isautoajax = true)
        {
            IList<SelectListItem> options = GetIssueAreaTypeOptions();
            return SiteSelect(helper, labeltext, labelcols, id, type, options, GlyphIcon.AREATYPE, isautoajax);
        }

        // Issue > AreaType (plain)
        public static IHtmlString SiteSelectIssueAreaType(
            this HtmlHelper       helper,
            string                id,
            SelectType            type,
            bool                  isautoajax = true)
        {
            IList<SelectListItem> options = GetIssueAreaTypeOptions();
            return SiteSelect(helper, id, type, options, isautoajax);
        }

        private static IList<SelectListItem> GetIssueAreaTypeOptions()
        {
            IList<SelectListItem> options = new List<SelectListItem>();
            options.Add(new SelectListItem { Text = Issue.STRING_AREATYPE_COMMON,    Value = Issue.AREATYPE_COMMON.ToString()    });
            options.Add(new SelectListItem { Text = Issue.STRING_AREATYPE_APARTMENT, Value = Issue.AREATYPE_APARTMENT.ToString() });
            return options;
        }

        // IssueFeedback > AccessType (label)
        public static IHtmlString SiteSelectIssueFeedbackAccessType(
            this HtmlHelper       helper,
            string                labeltext,
            int                   labelcols,
            string                id,
            SelectType            type,
            bool                  isautoajax = true)
        {
            IList<SelectListItem> options = GetIssueFeedbackAccessTypeOptions();
            return SiteSelect(helper, labeltext, labelcols, id, type, options, GlyphIcon.PERMISSION, isautoajax);
        }

        // IssueFeedback > AccessType (plain)
        public static IHtmlString SiteSelectIssueFeedbackAccessType(
            this HtmlHelper       helper,
            string                id,
            SelectType            type,
            bool                  isautoajax = true)
        {
            IList<SelectListItem> options = GetIssueFeedbackAccessTypeOptions();
            return SiteSelect(helper, id, type, options, isautoajax);
        }

        private static IList<SelectListItem> GetIssueFeedbackAccessTypeOptions()
        {
            IList<SelectListItem> options = new List<SelectListItem>();
            options.Add(new SelectListItem { Text = IssueFeedback.STRING_ACCESSTYPE_PUBLIC,  Value = IssueFeedback.ACCESSTYPE_PUBLIC.ToString()  });
            options.Add(new SelectListItem { Text = IssueFeedback.STRING_ACCESSTYPE_PRIVATE, Value = IssueFeedback.ACCESSTYPE_PRIVATE.ToString() });
            return options;
        }

        // News > Category (label)
        public static IHtmlString SiteSelectNewsCategory(
            this HtmlHelper       helper,
            string                labeltext,
            int                   labelcols,
            string                id,
            SelectType            type,
            bool                  isautoajax = true)
        {
            IList<SelectListItem> options = GetNewsCategoryOptions();
            return SiteSelect(helper, labeltext, labelcols, id, type, options, GlyphIcon.CATEGORY, isautoajax);
        }

        // News > Category (plain)
        public static IHtmlString SiteSelectNewsCategory(
            this HtmlHelper       helper,
            string                id,
            SelectType            type,
            bool                  isautoajax = true)
        {
            IList<SelectListItem> options = GetNewsCategoryOptions();
            return SiteSelect(helper, id, type, options, isautoajax);
        }

        private static IList<SelectListItem> GetNewsCategoryOptions()
        {
            IList<SelectListItem> options = new List<SelectListItem>();
            options.Add(new SelectListItem { Text = News.STRING_CATEGORY_MESSAGE, Value = News.CATEGORY_MESSAGE.ToString() });
            options.Add(new SelectListItem { Text = News.STRING_CATEGORY_ECONOMY, Value = News.CATEGORY_ECONOMY.ToString() });
            options.Add(new SelectListItem { Text = News.STRING_CATEGORY_REQUEST, Value = News.CATEGORY_REQUEST.ToString() });
            options.Add(new SelectListItem { Text = News.STRING_CATEGORY_CALLING, Value = News.CATEGORY_CALLING.ToString() });
            options.Add(new SelectListItem { Text = News.STRING_CATEGORY_WARNING, Value = News.CATEGORY_WARNING.ToString() });
            options.Add(new SelectListItem { Text = News.STRING_CATEGORY_VARIOUS, Value = News.CATEGORY_VARIOUS.ToString() });
            return options;
        }

        // Offer > Category (label)
        public static IHtmlString SiteSelectOfferCategory(
            this HtmlHelper       helper,
            string                labeltext,
            int                   labelcols,
            string                id,
            SelectType            type,
            bool                  isautoajax = true)
        {
            IList<SelectListItem> options = GetOfferCategoryOptions();
            return SiteSelect(helper, labeltext, labelcols, id, type, options, GlyphIcon.CATEGORY, isautoajax);
        }

        // Offer > Category (plain)
        public static IHtmlString SiteSelectOfferCategory(
            this HtmlHelper       helper,
            string                id,
            SelectType            type,
            bool                  isautoajax = true)
        {
            IList<SelectListItem> options = GetOfferCategoryOptions();
            return SiteSelect(helper, id, type, options, isautoajax);
        }

        private static IList<SelectListItem> GetOfferCategoryOptions()
        {
            IList<SelectListItem> options = new List<SelectListItem>();
            options.Add(new SelectListItem { Text = Offer.STRING_CATEGORY_UNDEF01, Value = Offer.CATEGORY_UNDEF01.ToString() });
            options.Add(new SelectListItem { Text = Offer.STRING_CATEGORY_UNDEF02, Value = Offer.CATEGORY_UNDEF02.ToString() });
            options.Add(new SelectListItem { Text = Offer.STRING_CATEGORY_UNDEF03, Value = Offer.CATEGORY_UNDEF03.ToString() });
            options.Add(new SelectListItem { Text = Offer.STRING_CATEGORY_VARIOUS, Value = Offer.CATEGORY_VARIOUS.ToString() });
            return options;
        }

        // Yes/No (label)
        public static IHtmlString SiteSelectYesNo(
            this HtmlHelper       helper,
            string                labeltext,
            int                   labelcols,
            string                id,
            bool                  isautoajax = true)
        {
            IList<SelectListItem> options = GetYesNoOptions();
            return SiteSelect(helper, labeltext, labelcols, id, SelectType.MANDATORY, options, GlyphIcon.YESNO, isautoajax);
        }

        // Yes/No (plain)
        public static IHtmlString SiteSelectYesNo(
            this HtmlHelper       helper,
            string                id,
            bool                  isautoajax = true)
        {
            IList<SelectListItem> options = GetYesNoOptions();
            return SiteSelect(helper, id, SelectType.MANDATORY, options, isautoajax);
        }

        private static IList<SelectListItem> GetYesNoOptions()
        {
            IList<SelectListItem> options = new List<SelectListItem>();
            options.Add(new SelectListItem { Text = "Ja",  Value = "1" });
            options.Add(new SelectListItem { Text = "Nej", Value = "0" });
            return options;
        }

        // Month (label)
        public static IHtmlString SiteSelectMonth(
            this HtmlHelper       helper,
            string                labeltext,
            int                   labelcols,
            string                id,
            bool                  shortname,
            bool                  isautoajax = true)
        {
            IList<SelectListItem> options = GetMonthOptions(shortname);
            return SiteSelect(helper, labeltext, labelcols, id, SelectType.MANDATORY, options, GlyphIcon.CALENDAR, isautoajax);
        }

        // Month (plain)
        public static IHtmlString SiteSelectMonth(
            this HtmlHelper       helper,
            string                id,
            bool                  shortname,
            bool                  isautoajax = true)
        {
            IList<SelectListItem> options = GetMonthOptions(shortname);
            return SiteSelect(helper, id, SelectType.MANDATORY, options, isautoajax);
        }

        private static IList<SelectListItem> GetMonthOptions(bool shortname)
        {
            IList<SelectListItem> options = new List<SelectListItem>();
            options.Add(new SelectListItem { Text = shortname ? "Jan" : "Januari",   Value = "1"  });
            options.Add(new SelectListItem { Text = shortname ? "Feb" : "Februari",  Value = "2"  });
            options.Add(new SelectListItem { Text = shortname ? "Mar" : "Mars",      Value = "3"  });
            options.Add(new SelectListItem { Text = shortname ? "Apr" : "April",     Value = "4"  });
            options.Add(new SelectListItem { Text = shortname ? "Maj" : "Maj",       Value = "5"  });
            options.Add(new SelectListItem { Text = shortname ? "Jun" : "Juni",      Value = "6"  });
            options.Add(new SelectListItem { Text = shortname ? "Jul" : "Juli",      Value = "7"  });
            options.Add(new SelectListItem { Text = shortname ? "Aug" : "Augusti",   Value = "8"  });
            options.Add(new SelectListItem { Text = shortname ? "Sep" : "September", Value = "9"  });
            options.Add(new SelectListItem { Text = shortname ? "Okt" : "Oktober",   Value = "10" });
            options.Add(new SelectListItem { Text = shortname ? "Nov" : "November",  Value = "11" });
            options.Add(new SelectListItem { Text = shortname ? "Dec" : "December",  Value = "12" });
            return options;
        }

        // Generic (label)
        public static IHtmlString SiteSelect(
            this HtmlHelper       helper,
            string                labeltext,
            int                   labelcols,
            string                id,
            SelectType            type,
            IList<SelectListItem> options,
            GlyphIcon             icon,
            bool                  isautoajax = true)
        {
            string autoajax  = isautoajax ? "true" : "false";
            string glyph     = Icon2Glyph(icon);
            int    inputcols = 12 - labelcols;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<div class=\"form-group\">");
            sb.AppendLine("<label class=\"control-label col-md-" + labelcols + "\" " + BOOTSTRAP_MARGIN + " for=\"" + id + "\">" + labeltext + "</label>");
            sb.AppendLine("<div class=\"col-md-" + inputcols + " input-group\">");
            sb.AppendLine("<span class=\"input-group-addon\">");
            sb.AppendLine("<span class=\"" + glyph + "\"></span>");
            sb.AppendLine("</span>");
            sb.AppendLine("<select class=\"selectpicker\""); 
            sb.AppendLine("data-autoajax=\"" + autoajax + "\"");
            sb.AppendLine("data-width=\"100%\"");
            sb.AppendLine("id=\"" + id + "\"");
            sb.AppendLine("name=\"" + id + "\">");
            // Possibly add "--"
            if (type == SelectType.NONEABLE)
            {
                var o = new TagBuilder("option");
                o.Attributes.Add("value", "0");
                o.InnerHtml = "--";
                sb.AppendLine(o.ToString(TagRenderMode.Normal));
            }
            // Build list
            if (options != null)
            {
                foreach (SelectListItem option in options)
                {
                    var o = new TagBuilder("option");
                    o.Attributes.Add("value", option.Value);
                    o.InnerHtml = option.Text;
                    sb.AppendLine(o.ToString(TagRenderMode.Normal));
                }
            }
            sb.AppendLine("</select>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            return new MvcHtmlString(sb.ToString());
        }

        // Generic (plain)
        public static IHtmlString SiteSelect(
            this HtmlHelper       helper,
            string                id,
            SelectType            type,
            IList<SelectListItem> options,
            bool                  isautoajax = true)
        {
            string autoajax  = isautoajax ? "true" : "false";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<select class=\"selectpicker\""); 
            sb.AppendLine("data-autoajax=\"" + autoajax + "\"");
            sb.AppendLine("data-width=\"100%\"");
            sb.AppendLine("id=\"" + id + "\"");
            sb.AppendLine("name=\"" + id + "\">");
            // Possibly add "--"
            if (type == SelectType.NONEABLE)
            {
                var o = new TagBuilder("option");
                o.Attributes.Add("value", "0");
                o.InnerHtml = "--";
                sb.AppendLine(o.ToString(TagRenderMode.Normal));
            }
            // Build list
            if (options != null)
            {
                foreach (SelectListItem option in options)
                {
                    var o = new TagBuilder("option");
                    o.Attributes.Add("value", option.Value);
                    o.InnerHtml = option.Text;
                    sb.AppendLine(o.ToString(TagRenderMode.Normal));
                }
            }
            sb.AppendLine("</select>");
            return new MvcHtmlString(sb.ToString());
        }

        private static string Icon2Glyph(GlyphIcon icon)
        {
            switch (icon)
            {
                case GlyphIcon.COMPANY:    return "glyphicon glyphicon-th-list";
                case GlyphIcon.PERMISSION: return "glyphicon glyphicon-ban-circle";
                case GlyphIcon.YESNO:      return "glyphicon glyphicon-off";
                case GlyphIcon.CALENDAR:   return "glyphicon glyphicon-calendar";
                case GlyphIcon.STATUS:     return "glyphicon glyphicon-cog";
                case GlyphIcon.PRIO:       return "glyphicon glyphicon-exclamation-sign";
                case GlyphIcon.AREATYPE:   return "glyphicon glyphicon-map-marker";
                case GlyphIcon.CATEGORY:   return "glyphicon glyphicon-sort-by-attributes";
                case GlyphIcon.USER:       return "glyphicon glyphicon-user";
                default:                   return "";
            }
        }
    }
}