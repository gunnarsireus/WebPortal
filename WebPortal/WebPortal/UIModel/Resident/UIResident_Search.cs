using ServerLibrary.Model;

namespace WebPortal.UIModel
{
    public class UIResident_Search
    {
        public int    id   { get; set; }
        public string text { get; set; }

        public UIResident_Search(Account dbm)
        {
            id   = dbm.id;
            text = dbm.AsText;
        }
    }
}