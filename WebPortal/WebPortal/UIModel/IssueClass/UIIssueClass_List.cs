using ServerLibrary.Model;
using WebPortal.Utils;

namespace WebPortal.UIModel
{
    public class UIIssueClass_List
    {
        public int    id   { get; set; }
        public string name { get; set; }

        /* Commands */
        public string editcmdlink   { get; set; }
        public string deletecmdlink { get; set; }

        public UIIssueClass_List(IssueClass model)
        {
            this.id            = model.id;
            this.name          = model.name;
            this.editcmdlink   = Images.EDIT();
            this.deletecmdlink = Images.DELETE();
        }
    }
}
