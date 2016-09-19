using ServerLibrary.Model;
using ServerLibrary.Utils;

namespace WebPortal.UIModel
{
    public class UIResident_S : Validatable
    {
        public int    id       { get; set; }
        public string email    { get; set; }
        public string password { get; set; }

        public UIResident_S()
        {
        }

        public Account UpdateModel(Account model)
        {
            // Need to check this before converted to hash
            password = ValidateRange(Account.MINLEN_PASSWORD, password, Account.MAXLEN_PASSWORD, "Felaktigt lösenord");
            ValidatePassword(Account.MINLEN_PASSWORD, password);
            // Create hash password
            model.password = CryptoUtils.GetMD5Hash(this.password);
            return model;
        }

        public override void Validate()
        {
        }
    }
}