using ServerLibrary.Model;

namespace WebPortal.UIModel
{
    public class UIIssueClass_CRU
    {
        public int    id   { get; set; }
        public string name { get; set; }

        public UIIssueClass_CRU()
        {
        }

        public UIIssueClass_CRU(IssueClass model)
        {
            this.id   = model.id;
            this.name = model.name;
        }

        public IssueClass CreateModel()
        {
            return UpdateModel(new IssueClass());
        }

        public IssueClass UpdateModel(IssueClass model)
        {
            model.name = this.name;
            return model;
        }
    }
}