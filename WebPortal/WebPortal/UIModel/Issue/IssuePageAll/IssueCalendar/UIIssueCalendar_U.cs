using ServerLibrary.Model;
using ServerLibrary.Utils;

namespace WebPortal.UIModel
{
    public class UIIssueCalendar_U
    {
        public int    id        { get; set; }
        public string startdate { get; set; }
        public string starttime { get; set; }
        public string enddate   { get; set; }
        public string endtime   { get; set; }

        public UIIssueCalendar_U()
        {
        }

        public Issue UpdateModel(Issue dbm)
        {
            dbm.startdate = DateUtils.ConvertToTimeStamp(startdate + " " + starttime);
            dbm.enddate   = DateUtils.ConvertToTimeStamp(enddate + " " + endtime);
            return dbm;
        }
    }
}
