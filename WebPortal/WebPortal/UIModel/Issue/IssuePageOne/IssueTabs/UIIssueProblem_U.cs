using ServerLibrary.Model;
using ServerLibrary.Utils;

namespace WebPortal.UIModel
{
    public class UIIssueProblem_U
    {
        public int    issueid         { get; set; }
        public string name            { get; set; }
        public int    assignedid      { get; set; }
        public string startdatestring { get; set; }
        public string starttimestring { get; set; }
        public string enddatestring   { get; set; }
        public string endtimestring   { get; set; }
        public int    issueclassid    { get; set; }
        public int    prio            { get; set; }
        public int    responsible     { get; set; }
        public int    areatype        { get; set; }
        public string description     { get; set; }

        public UIIssueProblem_U()
        {
        }

        public Issue UpdateModel(Issue model)
        {
            model.name         = this.name;
            model.assignedid   = this.assignedid;
            model.startdate    = DateUtils.ConvertToTimeStamp(this.startdatestring + " " + this.starttimestring);
            model.enddate      = DateUtils.ConvertToTimeStamp(this.enddatestring + " " + this.endtimestring);
            model.issueclassid = this.issueclassid;
            model.prio         = this.prio;
            model.responsible  = this.responsible;
            model.areatype     = this.areatype;
            model.description  = this.description;
            return model;
        }
    }
}