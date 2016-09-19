using ServerLibrary.Model;

namespace WebPortal.UIModel
{
    public class UIBugReport_C
    {
        public string headline    { get; set; }
        public string description { get; set; }

        public UIBugReport_C()
        {
        }

        public BugReport CreateModel()
        {
            BugReport model = new BugReport();
            model.headline    = this.headline;
            model.description = this.description;
            return model;
        }
    }
}