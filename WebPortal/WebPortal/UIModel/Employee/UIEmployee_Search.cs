using ServerLibrary.Model;

namespace WebPortal.UIModel
{
    public class UIEmployee_Search
    {
        public int    id   { get; set; }
        public string text { get; set; }

        public UIEmployee_Search(Account dbm)
        {
            id   = dbm.id;
            text = dbm.AsText;
        }
    }
}