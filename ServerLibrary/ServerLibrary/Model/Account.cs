using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

using ServerLibrary.Utils;

namespace ServerLibrary.Model
{
    [Table("accounts")]
    public class Account : Activatable
    {
        // Constants
        public const int     ACCOUNT_ANY   = 0;
        public const Account EMPTY_ACCOUNT = null;

        public const int AUTHZ_ANY        = 0x00;  // No web access
        public const int AUTHZ_RESIDENT   = 0x01;  // 1
        public const int AUTHZ_CUSTOMER   = 0x02;  // 2
        public const int AUTHZ_TECHNICIAN = 0x04;  // 4
        public const int AUTHZ_MANAGEMENT = 0x10;  // 16
        public const int AUTHZ_ADMIN      = 0x80;  // 128
        public const int MAX_ATTEMPTS     = 3;
        public const int LOCKED_SECS      = 600;   // 10 mins
        public const int PIN_VALID_SECS   = 1800;  // 30 mins

        // Strings
        public const string STRING_AUTHZ_RESIDENT   = "Boende";
        public const string STRING_AUTHZ_CUSTOMER   = "Kund";
        public const string STRING_AUTHZ_TECHNICIAN = "Tekniker";
        public const string STRING_AUTHZ_MANAGEMENT = "Ledning";
        public const string STRING_AUTHZ_ADMIN      = "Admin";

        // Database constraints
        public const int MINLEN_EMAIL     = 5;
        public const int MAXLEN_EMAIL     = 100;
        public const int MINLEN_PASSWORD  = 6;
        public const int MAXLEN_PASSWORD  = 45;
        public const int MINLEN_FIRSTNAME = 2;
        public const int MAXLEN_FIRSTNAME = 45;
        public const int MINLEN_LASTNAME  = 2;
        public const int MAXLEN_LASTNAME  = 45;
        public const int MINLEN_PHONE     = 0;
        public const int MAXLEN_PHONE     = 45;
        public const int MINLEN_ADDRESS   = 0;
        public const int MAXLEN_ADDRESS   = 45;
        public const int MINLEN_FLOOR     = 0;
        public const int MAXLEN_FLOOR     = 45;
        public const int MINLEN_APARTMENT = 0;
        public const int MAXLEN_APARTMENT = 45;
        public const int MINLEN_PIN       = 6;
        public const int MAXLEN_PIN       = 6;

        [Key]
        public int    id              { get; set; }
        public int    authz           { get; set; }
        // Activatable.active         
        public long   lastlogin       { get; set; }
        public string email           { get; set; }
        public string password        { get; set; }
        public string firstname       { get; set; }
        public string lastname        { get; set; }
        public string phone           { get; set; }
        public string address         { get; set; }
        public string floor           { get; set; }
        public string apartment       { get; set; }
        public string token           { get; set; }
        public long   tokenvaliduntil { get; set; }
        public string PIN             { get; set; }
        public long   PINvaliduntil   { get; set; }
        public int    failedattempts  { get; set; }
        public long   lockeduntil     { get; set; }
        public int    customerid      { get; set; }

        public Account()
        {
            this.id              = 0;
            this.authz           = AUTHZ_ANY;
            // Activatable.active         
            this.lastlogin       = 0;
            this.email           = "";
            this.password        = "";
            this.firstname       = "";
            this.lastname        = "";
            this.phone           = "";
            this.address         = "";
            this.floor           = "";
            this.apartment       = "";
            this.token           = "";
            this.tokenvaliduntil = 0;
            this.PIN             = "";
            this.PINvaliduntil   = 0;
            this.failedattempts  = 0;
            this.lockeduntil     = 0;
            this.customerid      = 0;
        }

        // Convenience methods

        public string Name
        {
            get { return firstname + " " + lastname; }
        }

        public string AsText
        {
            get
            {
                string location = this.IsAtMostCustomer() ? this.address : "Renew";
                return Name + " (" + location + ")";
            }
        }

        // Authorization mappings

        public static string AuthzAsString(int authz)
        {
            if (Account.IsResident(authz))   return STRING_AUTHZ_RESIDENT;
            if (Account.IsCustomer(authz))   return STRING_AUTHZ_CUSTOMER;
            if (Account.IsTechnician(authz)) return STRING_AUTHZ_TECHNICIAN;
            if (Account.IsManagement(authz)) return STRING_AUTHZ_MANAGEMENT;
            if (Account.IsAdmin(authz))      return STRING_AUTHZ_ADMIN;
            return "";
        }

        public static bool IsValidAuthz(int authz)
        {
            return
                authz == AUTHZ_RESIDENT   ||
                authz == AUTHZ_CUSTOMER   ||
                authz == AUTHZ_TECHNICIAN ||
                authz == AUTHZ_MANAGEMENT ||
                authz == AUTHZ_ADMIN;
        }

        public static bool IsAtLeast(Account account, int authz) { return account != null && account.authz >= authz; }

        public bool IsResident()   { return Account.IsResident(authz); }
        public bool IsCustomer()   { return Account.IsCustomer(authz); }
        public bool IsTechnician() { return Account.IsTechnician(authz); }
        public bool IsManagement() { return Account.IsManagement(authz); }
        public bool IsAdmin()      { return Account.IsAdmin(authz); }

        public bool IsAtLeastResident()   { return Account.IsAtLeastResident(authz); }
        public bool IsAtLeastCustomer()   { return Account.IsAtLeastCustomer(authz); }
        public bool IsAtLeastTechnician() { return Account.IsAtLeastTechnician(authz); }
        public bool IsAtLeastManagement() { return Account.IsAtLeastManagement(authz); }
        public bool IsAtLeastAdmin()      { return Account.IsAtLeastAdmin(authz); }

        public bool IsAtMostResident()   { return Account.IsAtMostResident(authz); }
        public bool IsAtMostCustomer()   { return Account.IsAtMostCustomer(authz); }
        public bool IsAtMostTechnician() { return Account.IsAtMostTechnician(authz); }
        public bool IsAtMostManagement() { return Account.IsAtMostManagement(authz); }
        public bool IsAtMostAdmin()      { return Account.IsAtMostAdmin(authz); }

        public static bool IsResident(int authz)   { return authz == AUTHZ_RESIDENT; }
        public static bool IsCustomer(int authz)   { return authz == AUTHZ_CUSTOMER; }
        public static bool IsTechnician(int authz) { return authz == AUTHZ_TECHNICIAN; }
        public static bool IsManagement(int authz) { return authz == AUTHZ_MANAGEMENT; }
        public static bool IsAdmin(int authz)      { return authz == AUTHZ_ADMIN; }

        public static bool IsAtLeastResident(int authz)   { return authz >= AUTHZ_RESIDENT; }
        public static bool IsAtLeastCustomer(int authz)   { return authz >= AUTHZ_CUSTOMER; }
        public static bool IsAtLeastTechnician(int authz) { return authz >= AUTHZ_TECHNICIAN; }
        public static bool IsAtLeastManagement(int authz) { return authz >= AUTHZ_MANAGEMENT; }
        public static bool IsAtLeastAdmin(int authz)      { return authz >= AUTHZ_ADMIN; }

        public static bool IsAtMostResident(int authz)   { return authz <= AUTHZ_RESIDENT; }
        public static bool IsAtMostCustomer(int authz)   { return authz <= AUTHZ_CUSTOMER; }
        public static bool IsAtMostTechnician(int authz) { return authz <= AUTHZ_TECHNICIAN; }
        public static bool IsAtMostManagement(int authz) { return authz <= AUTHZ_MANAGEMENT; }
        public static bool IsAtMostAdmin(int authz)      { return authz <= AUTHZ_ADMIN; }

        // Verification process

        public bool IsVerified()
        {
            return password.Length > 0 && active == Activatable.ACTIVE;
        }

        public bool IsVerifiedInactive()
        {
            return password.Length > 0 && active == Activatable.INACTIVE;
        }

        public bool IsUnverified()
        {
            return password.Length == 0 && active == Activatable.INACTIVE;
        }

        public bool IsLocked(long now)
        {
            if (Math.Abs(this.lockeduntil - now) >= Account.LOCKED_SECS)
            {
                return false;
            }
            return this.lockeduntil >= now;
        }

        public void SetSuccessfulLogin(long now)
        {
            this.lastlogin      = now;
            this.PIN            = "";
            this.PINvaliduntil  = 0;
            this.failedattempts = 0;
            this.lockeduntil    = 0;
        }

        public int RegisterFailedAttempt(long now)
        {
            if (this.failedattempts < Account.MAX_ATTEMPTS)
            {
                this.failedattempts++;
            }

            if (this.failedattempts >= Account.MAX_ATTEMPTS)
            {
                this.failedattempts = 0;
                this.lockeduntil    = now + Account.LOCKED_SECS;
            }
            else if (this.lockeduntil != 0)
            {
                this.lockeduntil = 0;
            }
            return this.failedattempts;
        }

        // Validation

        public override void Validate()
        {
            ValidateCondition(Account.IsValidAuthz(authz),                           "Ogiltig behörighet");
            ValidateCondition(IsAtMostCustomer()    ? customerid != 0 : true,        "Förening måste vara vald");
            ValidateCondition(IsAtLeastTechnician() ? customerid == 0 : true,        "Förening får inte vara vald");
            if (!IsAdmin())
            {
                email = ValidateEmail(MINLEN_EMAIL,     email,     MAXLEN_EMAIL,     "Felaktig e-postadress");
            }
            firstname = ValidateRange(MINLEN_FIRSTNAME, firstname, MAXLEN_FIRSTNAME, "Felaktigt förnamn");
            lastname  = ValidateRange(MINLEN_LASTNAME,  lastname,  MAXLEN_LASTNAME,  "Felaktigt efternamn");
            phone     = ValidatePhone(MINLEN_PHONE,     phone,     MAXLEN_PHONE,     "Felaktigt telefonnummer");
            address   = ValidateRange(MINLEN_ADDRESS,   address,   MAXLEN_ADDRESS,   "Felaktig adress");
            floor     = ValidateRange(MINLEN_FLOOR,     floor,     MAXLEN_FLOOR,     "Felaktigt våningsplan");
            apartment = ValidateRange(MINLEN_APARTMENT, apartment, MAXLEN_APARTMENT, "Felaktig lägenhetsnummer");
        }
    }
}