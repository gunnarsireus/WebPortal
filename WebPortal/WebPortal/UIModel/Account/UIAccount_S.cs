using ServerLibrary.Model;
using ServerLibrary.Utils;

namespace WebPortal.UIModel
{
    public class UIAccount_S : Validatable
    {
        public string email     { get; set; }
        public string PIN       { get; set; }
        public string password1 { get; set; }
        public string password2 { get; set; }

        public UIAccount_S()
        {
        }

        public Account UpdateModel(Account model)
        {
            // Need to check this before converted to hash
            password1 = ValidateRange(Account.MINLEN_PASSWORD, password1, Account.MAXLEN_PASSWORD, "Felaktigt lösenord");
            ValidateEquals(password1, password2, "Lösenorden matchar inte");
            ValidatePassword(Account.MINLEN_PASSWORD, password1);
            // Create hash password
            model.password = CryptoUtils.GetMD5Hash(password1);
            return model;
        }

        public Account ResetModel(Account model)
        {
            model.password = "";
            return model;
        }

        public override void Validate()
        {
        }
    }
}
