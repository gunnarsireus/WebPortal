using System;
using System.Linq;
using System.Collections.Generic;
using ServerLibrary.Model;
using ServerLibrary.Utils;
using WebPortal.Utils;

namespace WebPortal.UIModel
{
    public class UIIssueSchedule_List
    {
        public class EventData
        {
            public int    id          { get; set; }
            public int    assignedid  { get; set; }
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

            public EventData(Issue model, Account technician)
            {
                int nextstatus = Issue.NextProbableStatus(model.status);
                
                this.id          = model.id;
                this.assignedid  = model.assignedid;
                this.status      = model.status;
                this.priority    = Issue.PrioAsString(model.prio);
                this.header      = StringUtils.Shorten(model.name, 30);
                this.description = StringUtils.Shorten(model.description, 100);
                this.address     = StringUtils.Shorten(model.address, 30);
                this.assigned    = technician.Name;
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

        public class ResourceData
        {
            public int    id   { get; set; }
            public string name { get; set; }

            public ResourceData(Account technician)
            {
                this.id   = technician.id;
                this.name = technician.Name;
            }
        }

        public IList<EventData>    events;
        public IList<ResourceData> resources;

        public UIIssueSchedule_List(IList<Issue> issues, IList<Account> technicians)
        {
            events    = new List<EventData>(technicians.Count * 20);
            resources = new List<ResourceData>(technicians.Count);

            foreach (Issue issue in issues)
            {
                Account assigned = technicians.FirstOrDefault(a => a.id == issue.assignedid);
                if (assigned != null)
                {
                    events.Add(new EventData(issue, assigned));
                }
            }

            foreach (Account technician in technicians)
            {
                resources.Add(new ResourceData(technician));
            }
        }
    }
}