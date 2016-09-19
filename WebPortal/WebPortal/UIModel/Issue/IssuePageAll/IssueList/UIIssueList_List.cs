using System;
using ServerLibrary.Model;
using ServerLibrary.Utils;
using WebPortal.Utils;

namespace WebPortal.UIModel
{
    public class UIIssueList_List
    {
        public int    id              { get; set; }
        public string statusicon      { get; set; }
        public string priority        { get; set; }
        public string header          { get; set; }
        public string descriptionicon { get; set; }
        public string address         { get; set; }
        public string responsibleicon { get; set; }
        public string assigned        { get; set; }

        /* Hidden */
        public string areatype  { get; set; }
        public string startdate { get; set; }
        public string enddate   { get; set; }
        public int    status    { get; set; }

        /* Commands */
        public string editcmdlink   { get; set; }
        public string statuscmdlink { get; set; }

        public UIIssueList_List(Issue model, string assigned)
        {
            int nextstatus = Issue.NextProbableStatus(model.status);

            this.id              = model.id;
            this.statusicon      = Images.StatusAsImage(model.status, null, Issue.StatusAsString(model.status));
            this.priority        = Issue.PrioAsString(model.prio);
            this.header          = StringUtils.Shorten(model.name, 30);
            this.descriptionicon = Images.COMMENT(null, model.description);
            this.address         = StringUtils.Shorten(model.address, 30);
            this.assigned        = assigned;
            this.responsibleicon = model.responsible == Issue.RESPONSIBLE_INTERNAL ? "" : Images.CUSTOMER();
            this.areatype        = Issue.AreaTypeAsString(model.areatype);
            this.startdate       = DateUtils.ConvertToDateString(model.startdate);
            this.enddate         = DateUtils.ConvertToDateString(model.enddate);
            this.status          = model.status;
            this.editcmdlink     = Images.EDIT();
            this.statuscmdlink   = Images.StatusAsNextImage(nextstatus, null, nextstatus + "/" + Issue.StatusAsString(nextstatus));
        }
    }
}