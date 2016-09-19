using ServerLibrary.Model;
using ServerLibrary.Utils;

namespace WebPortal.UIModel
{
    public class UIIssueStatus_U
    {
        public int id     { get; set; }
        public int status { get; set; }

        public UIIssueStatus_U()
        {
        }

        public Issue UpdateModel(Issue dbm)
        {
            dbm.status = this.status;
            return dbm;
        }

        public IssueTransition CreateModel(Issue dbm, Account requester)
        {
            IssueTransition model = new IssueTransition();
            model.fromstatus  = dbm.status;
            model.tostatus    = this.status;
            model.issueid     = dbm.id;
            model.createdby   = requester.id;
            model.createdname = requester.Name;
            model.createddate = DateUtils.TimeStamp;
            return model;
        }
    }
}