using ServerLibrary.Model;
using ServerLibrary.Utils;

namespace WebPortal.UIModel
{
    public class UIIssueFeedback_C
    {
        public int    issueid     { get; set; }
        public int    accesstype  { get; set; }
        public string description { get; set; }

        public UIIssueFeedback_C()
        {
        }

        public IssueFeedback CreateModel(Account account)
        {
            IssueFeedback model = new IssueFeedback();
            model.issueid      = this.issueid;
            model.accesstype   = this.accesstype;
            model.description  = this.description;
            model.createdby    = account.id;
            model.createdname  = account.Name;
            model.createdauthz = account.authz;
            model.createddate  = DateUtils.TimeStamp;
            return model;
        }
    }
}