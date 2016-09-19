using ServerLibrary.Model;

namespace WebPortal.UIModel
{
    public class UICustomer_Search
    {
        public int    id   { get; set; }
        public string text { get; set; }

        public UICustomer_Search(Customer dbm)
        {
            id   = dbm.id;
            text = dbm.AsText;
        }
    }
}