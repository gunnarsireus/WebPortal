using System;
using ServerLibrary.Model;
using ServerLibrary.Utils;
using WebPortal.Utils;

namespace WebPortal.UIModel
{
    public class UIIssueCalendar_List
    {
        public int    id          { get; set; }
        public int    status      { get; set; }
        public string priority    { get; set; }
        public string header      { get; set; }
        public string description { get; set; }
        public string address     { get; set; }
        public string assigned    { get; set; }
        public string responsible { get; set; }
        public string areatype    { get; set; }
        public string startdate   { get; set; }
        public string enddate     { get; set; }
        public string backcolor   { get; set; }
        public string textcolor   { get; set; }
        public bool   changeable  { get; set; }
        public string statusname  { get; set; }
        public string statuslink  { get; set; }

        public UIIssueCalendar_List(Issue model, string assigned)
        {
            int nextstatus = Issue.NextProbableStatus(model.status);

            this.id          = model.id;
            this.status      = model.status;
            this.priority    = Issue.PrioAsString(model.prio);
            this.header      = StringUtils.Shorten(model.name, 30);
            this.description = StringUtils.Shorten(model.description, 100);
            this.address     = StringUtils.Shorten(model.address, 30);
            this.assigned    = assigned;
            this.responsible = Issue.ResponsibleAsString(model.responsible);
            this.areatype    = Issue.AreaTypeAsString(model.areatype);
            this.startdate   = DateUtils.ConvertToISO8601DateTimeString(model.startdate);
            this.enddate     = DateUtils.ConvertToISO8601DateTimeString(model.enddate);
            this.backcolor   = Issue.StatusAsBackgroundColor(model.status);
            this.textcolor   = Issue.StatusAsTextColor(model.status);
            this.changeable  = true;
            this.statusname  = Issue.StatusAsString(model.status);
            this.statuslink  = Images.StatusAsNextImage(nextstatus, "issue-" + model.id, nextstatus + "/" + Issue.StatusAsString(nextstatus));
        }
    }
}