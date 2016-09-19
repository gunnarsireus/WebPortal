using ServerLibrary.Model;
using ServerLibrary.Utils;

namespace WebPortal.UIModel
{
    public class UIIssue_C
    {
        public int    customerid      { get; set; }
        public int    assignedid      { get; set; }
        public int    residentid      { get; set; }
        public int    prio            { get; set; }
        public int    responsible     { get; set; }
        public int    areatype        { get; set; }
        public int    issueclassid    { get; set; }
        public string name            { get; set; }
        public string description     { get; set; }
        public string startdatestring { get; set; }
        public string starttimestring { get; set; }
        public string enddatestring   { get; set; }
        public string endtimestring   { get; set; }
        public string firstname       { get; set; }
        public string lastname        { get; set; }
        public string phone           { get; set; }
        public string email           { get; set; }
        public string address         { get; set; }
        public string floor           { get; set; }
        public string apartment       { get; set; }

        public UIIssue_C()
        {
        }

        public Issue CreateModel()
        {
            Issue model = new Issue();
            model.status       = Issue.STATUS_PRELIMINARY;
            model.customerid   = this.customerid;
            model.assignedid   = this.assignedid;
            model.residentid   = this.residentid;
            model.prio         = this.prio;
            model.responsible  = this.responsible;
            model.areatype     = this.areatype;
            model.issueclassid = this.issueclassid;
            model.name         = this.name;
            model.description  = this.description;
            model.startdate    = DateUtils.ConvertToTimeStamp(this.startdatestring + " " + this.starttimestring);
            model.enddate      = DateUtils.ConvertToTimeStamp(this.enddatestring + " " + this.endtimestring);
            model.firstname    = this.firstname;
            model.lastname     = this.lastname;
            model.phone        = this.phone;
            model.email        = this.email;
            model.address      = this.address;
            model.floor        = this.floor;
            model.apartment    = this.apartment;
            return model;
        }

        public IssueTransition CreateModel(Issue dbm, Account requester)
        {
            IssueTransition model = new IssueTransition();
            model.fromstatus  = Issue.__STATUS_NONEXISTING;
            model.tostatus    = dbm.status;
            model.issueid     = dbm.id;
            model.createdby   = requester.id;
            model.createdname = requester.Name;
            model.createddate = DateUtils.TimeStamp;
            return model;
        }
    }
}