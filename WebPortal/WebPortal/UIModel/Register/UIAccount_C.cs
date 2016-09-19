using ServerLibrary.Model;
using ServerLibrary.Utils;

namespace WebPortal.UIModel
{
    public class UIAccount_C
    {
        public string email      { get; set; }
        public string firstname  { get; set; }
        public string lastname   { get; set; }
        public string phone      { get; set; }
        public string address    { get; set; }
        public string floor      { get; set; }
        public string apartment  { get; set; }
        public int    customerid { get; set; }

        public UIAccount_C()
        {
        }

        public Account CreateModel()
        {
            Account model       = new Account();
            model.authz         = Account.AUTHZ_RESIDENT;
            model.active        = Account.INACTIVE;
            model.email         = this.email;
            model.firstname     = this.firstname;
            model.lastname      = this.lastname;
            model.phone         = this.phone;
            model.address       = this.address;
            model.floor         = this.floor;
            model.apartment     = this.apartment;
            model.customerid    = this.customerid;
            model.token         = "";
            model.password      = "";
            model.PIN           = CryptoUtils.CreatePINCode();
            model.PINvaliduntil = DateUtils.TimeStamp + Account.PIN_VALID_SECS;
            return model;
        }
    }
}