using ServerLibrary.Model;
using ServerLibrary.Utils;

namespace WebPortal.UIModel
{
    public class UIProfile_S : Validatable
    {
        public string password     { get; set; }
        public string newpassword1 { get; set; }
        public string newpassword2 { get; set; }

        public UIProfile_S()
        {
        }

        public Account UpdateModel(Account model)
        {
            // Knows current password?
            ValidateEquals(CryptoUtils.GetMD5Hash(password), model.password, "Felaktigt lösenord");
            // Need to check this before converted to hash
            newpassword1 = ValidateRange(Account.MINLEN_PASSWORD, newpassword1, Account.MAXLEN_PASSWORD, "Felaktigt nytt lösenord");
            ValidateEquals(newpassword1, newpassword2, "Nytt lösenord matchar inte");
            if (!model.IsAdmin())
            {
                ValidatePassword(Account.MINLEN_PASSWORD, newpassword1);
            }
            // Create hash password
            model.password = CryptoUtils.GetMD5Hash(newpassword1);
            return model;
        }

        public override void Validate()
        {
        }
    }
}
