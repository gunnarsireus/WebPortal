using ServerLibrary.Model;
using ServerLibrary.Utils;

namespace WebPortal.UIModel
{
    public class UIIssueSchedule_U
    {
        public int    id         { get; set; }
        public int    assignedid { get; set; }
        public string startdate  { get; set; }
        public string starttime  { get; set; }
        public string enddate    { get; set; }
        public string endtime    { get; set; }

        public UIIssueSchedule_U()
        {
        }

        public Issue UpdateModel(Issue dbm)
        {
            dbm.assignedid = assignedid;
            dbm.startdate  = DateUtils.ConvertToTimeStamp(startdate + " " + starttime);
            dbm.enddate    = DateUtils.ConvertToTimeStamp(enddate + " " + endtime);
            return dbm;
        }
    }
}
