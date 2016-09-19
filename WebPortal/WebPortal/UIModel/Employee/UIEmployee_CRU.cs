using ServerLibrary.Model;
using ServerLibrary.Utils;

namespace WebPortal.UIModel
{
    public class UIEmployee_CRU : Validatable
    {
        public int    id        { get; set; }
        public int    authz     { get; set; }
        public int    active    { get; set; }
        public string email     { get; set; }
        public string password  { get; set; }
        public string phone     { get; set; }
        public string firstname { get; set; }
        public string lastname  { get; set; }

        public UIEmployee_CRU()
        {
        }

        public UIEmployee_CRU(Account model)
        {
            this.id        = model.id;
            this.authz     = model.authz;
            this.active    = model.active;
            this.email     = model.email;
            // Skip password
            this.phone     = model.phone;
            this.firstname = model.firstname;
            this.lastname  = model.lastname;
        }

        public Account CreateModel()
        {
            // Need to check this before converted to hash
            password = ValidateRange(Account.MINLEN_PASSWORD, password, Account.MAXLEN_PASSWORD, "Felaktigt lösenord");
            ValidatePassword(Account.MINLEN_PASSWORD, password);
            // Create account and hash password
            Account model = UpdateModel(new Account());
            model.password  = CryptoUtils.GetMD5Hash(this.password);
            model.lastlogin = DateUtils.TimeStamp;
            return model;
        }

        public Account UpdateModel(Account model)
        {
            if (model != null)
            {
                model.authz      = this.authz;
                model.active     = this.active;
                model.email      = this.email;
                // Skip password
                model.phone      = this.phone;
                model.firstname  = this.firstname;
                model.lastname   = this.lastname;
                model.address    = "";
                model.floor      = "";
                model.apartment  = "";
                model.customerid = Customer.CUSTOMER_ANY; 
            }
            return model;
        }

        public override void Validate()
        {
        }
    }
}