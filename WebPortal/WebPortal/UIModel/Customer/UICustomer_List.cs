using ServerLibrary.Model;
using ServerLibrary.Utils;
using WebPortal.Utils;

namespace WebPortal.UIModel
{
    public class UICustomer_List
    {
        public long   id        { get; set; }
        public string name      { get; set; }
        public string orgnumber { get; set; }
        public string address   { get; set; }
        public string city      { get; set; }

        /* Commands */
        public string editcmdlink   { get; set; }
        public string deletecmdlink { get; set; }

        public UICustomer_List(Customer model)
        {
            this.id            = model.id;
            this.name          = StringUtils.Shorten(model.name, 30);
            this.orgnumber     = model.orgnumber;
            this.address       = StringUtils.Shorten(model.address, 20);
            this.city          = StringUtils.Shorten(model.city, 15);
            this.editcmdlink   = Images.EDIT();
            this.deletecmdlink = Images.DELETE();
        }
    }
}
