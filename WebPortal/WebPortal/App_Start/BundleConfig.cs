using System;
using System.Web.Optimization;

namespace WebPortal.App_Start
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            Scripts.DefaultTagFormat = @"<script src=""{0}"" type=""text/javascript""></script>";
            Styles.DefaultTagFormat  = @"<link href=""{0}"" rel=""stylesheet"" type=""text/css"">";

            // All style sheets
            bundles.Add(new StyleBundle("~/bootstrap/css").Include(
                "~/css/jquery-ui-1.11.2.css",
                "~/css/DataTables-1.10.4/css/jquery.dataTables.css",
                "~/css/DataTables-1.10.4/css/dataTables.bootstrap.css",
                "~/css/bootstrap.css",
                "~/css/bootstrap-clockpicker.css",
                "~/css/bootstrap-datepicker.css",
                "~/css/bootstrap-select.css",
                "~/css/select2/select2.css",
                "~/css/select2/bootstrap-select2.css",
                "~/css/FullCalendar-2.6.1/fullcalendar.css",
                "~/css/FullCalendar-2.6.1/fullscheduler.css",
                "~/css/select2/site.css",
                "~/css/site.css"));

            // Modernizr and jQuery with input validation
            bundles.Add(new ScriptBundle("~/jquery/js").Include(
                "~/js/platform/modernizr-2.5.3.js",
                "~/js/platform/jquery-1.11.2.js",
                "~/js/platform/jquery-ui-1.11.2.js",
                //"~/js/platform/jquery.jeditable.js",
                "~/js/platform/jquery.uiblock.js",
                "~/js/platform/jquery.validate.js",
                "~/js/platform/jquery.validate.unobtrusive.js",
                "~/js/platform/jquery.validate.bootstrap.js",
                "~/js/platform/jquery.unobtrusive-ajax.js"));

            // Bootstrap etc
            bundles.Add(new ScriptBundle("~/bootstrap/js").Include(
                "~/js/platform/bootstrap.js",
                "~/js/platform/bootstrap-select.js",
                "~/js/platform/bootstrap-clockpicker.js",
                "~/js/platform/bootstrap-datepicker.js",
                //"~/js/platform/bootstrap-duallistbox.js",
                "~/js/DataTables-1.10.4/jquery.dataTables.js",
                "~/js/DataTables-1.10.4/dataTables.bootstrap.js",
                "~/js/select2/select2.full.js",
                "~/js/select2/i18n/sv.js",
                "~/js/site-base.js"));

            // Custom components
            bundles.Add(new ScriptBundle("~/site-components/js").Include(
                "~/js/site-components/site-xcomp-autoajax.js",
                "~/js/site-components/site-xcomp-validation.js",
                "~/js/site-components/site-xcomp-inputforms.js",
                "~/js/site-components/site-xcomp-inputpanels.js",
                "~/js/site-components/site-xcomp-select.js",
                "~/js/site-components/site-xcomp-select2.js",
                "~/js/site-components/site-xcomp-localstorage.js",
                "~/js/site-components/site-xcomp-popover.js",
                "~/js/site-components/site-xcomp-datatables.js",
                "~/js/site-components/site-xcomp-datepicker.js",
                "~/js/site-components/site-xcomp-dialogs.js",
                "~/js/site-components/site-xcomp-tabmenu.js"));

            // Page-specific JS code
            bundles.Add(new ScriptBundle("~/site-bugreport/js").Include(
                "~/js/site-bugreport.js"));

            bundles.Add(new ScriptBundle("~/site-customer/js").Include(
                "~/js/site-customer.js"));

            bundles.Add(new ScriptBundle("~/site-employee/js").Include(
                "~/js/site-employee.js"));
            
            bundles.Add(new ScriptBundle("~/site-home/js").Include(
                "~/js/site-home.js"));
            
            bundles.Add(new ScriptBundle("~/site-issue/js").Include(
                "~/js/FullCalendar-2.6.1/moment.min.js",
                "~/js/FullCalendar-2.6.1/fullcalendar.js",
                "~/js/FullCalendar-2.6.1/fullscheduler.js",
                "~/js/FullCalendar-2.6.1/lang/sv.js",
                "~/js/site-components/site-xcomp-calendar.js",
                "~/js/site-components/site-xcomp-scheduler.js",
                "~/js/site-issue/site-issuepageall/site-issuecalendar/site-issuecalendar.js",
                "~/js/site-issue/site-issuepageall/site-issueschedule/site-issueschedule.js",
                "~/js/site-issue/site-issuepageall/site-issuelist/site-issuelist.js",
                "~/js/site-issue/site-issuepageall/site-issuecreate.js",
                "~/js/site-issue/site-issuepageall/site-issuefilter.js",
                "~/js/site-issue/site-issuepageall/site-issuepageall.js",
                "~/js/site-issue/site-issuepageone/site-issuetabs/site-issuetabproblem.js",
                "~/js/site-issue/site-issuepageone/site-issuetabs/site-issuefeedbackcreate.js",
                "~/js/site-issue/site-issuepageone/site-issuetabs/site-issuetabfeedback.js",
                "~/js/site-issue/site-issuepageone/site-issuetabs/site-issuetabhistory.js",
                "~/js/site-issue/site-issuepageone/site-issuetabs.js",
                "~/js/site-issue/site-issuepageone/site-issuecommand.js",
                "~/js/site-issue/site-issuepageone/site-issuepageone.js",
                "~/js/site-issue/site-issue.js"));

            bundles.Add(new ScriptBundle("~/site-issueclass/js").Include(
                "~/js/site-issueclass.js"));

            bundles.Add(new ScriptBundle("~/site-login/js").Include(
                "~/js/site-components/site-xcomp-localstorage.js",
                "~/js/site-login.js"));

            bundles.Add(new ScriptBundle("~/site-news/js").Include(
                "~/js/site-news.js"));

            bundles.Add(new ScriptBundle("~/site-offer/js").Include(
                "~/js/site-offer.js"));

            bundles.Add(new ScriptBundle("~/site-profile/js").Include(
                "~/js/site-profile.js"));

            bundles.Add(new ScriptBundle("~/site-register/js").Include(
                "~/js/site-components/site-xcomp-localstorage.js",
                "~/js/site-register.js"));

            bundles.Add(new ScriptBundle("~/site-resident/js").Include(
                "~/js/site-resident.js"));

            bundles.Add(new ScriptBundle("~/site-timetype/js").Include(
                "~/js/site-timetype.js"));

#if !DEBUG
            BundleTable.EnableOptimizations = true;
#endif
        }
    }
}
