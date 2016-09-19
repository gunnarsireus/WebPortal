using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using ServerLibrary.Utils;

namespace ServerLibrary.Model
{
    [Table("customers")]
    public class Customer : Activatable
    {
        // Constants
        public const int CUSTOMER_ANY   = 0;
        public const int MANAGEMENT_ANY = 0;
        public const int TECHNICIAN_ANY = 0;

        public const int TYPE_ANY     = 0;
        public const int TYPE_COMPANY = 11;
        public const int TYPE_PERSON  = 12;

        // Strings
        public const string STRING_TYPE_COMPANY = "BRF/Företag";
        public const string STRING_TYPE_PERSON  = "Privatperson";

        // Database constraints
        public const int MINLEN_NAME      = 2;
        public const int MAXLEN_NAME      = 45;
        public const int MINLEN_PHONE     = 0;
        public const int MAXLEN_PHONE     = 20;
        public const int MINLEN_EMAIL     = 0;
        public const int MAXLEN_EMAIL     = 60;
        public const int MINLEN_ADDRESS   = 5;
        public const int MAXLEN_ADDRESS   = 45;
        public const int MINLEN_CITY      = 2;
        public const int MAXLEN_CITY      = 45;
        public const int MINLEN_ZIP       = 5;
        public const int MAXLEN_ZIP       = 10;
        public const int MINLEN_ORGNUMBER = 0;
        public const int MAXLEN_ORGNUMBER = 14;
        public const int MINLEN_VISMAREF  = 0;
        public const int MAXLEN_VISMAREF  = 12;
        public const int MINLEN_CONTACT   = 0;
        public const int MAXLEN_CONTACT   = 45;
        public const int MINLEN_NOTE      = 0;
        public const int MAXLEN_NOTE      = 200;
        
        [Key]
        public int    id           { get; set; }
        public int    type         { get; set; }
        // Activatable.active
        public int    managementid { get; set; }
        public int    technicianid { get; set; }
        public string name         { get; set; }
        public string phone        { get; set; }
        public string email        { get; set; }
        public string address      { get; set; }
        public string zip          { get; set; }
        public string city         { get; set; }
        public string orgnumber    { get; set; }
        public string vismaref     { get; set; }
        public string contact      { get; set; }
        public string note         { get; set; }

        public Customer()
        {
            this.id           = 0;
            this.type         = TYPE_ANY;
            // Activatable.active
            this.managementid = MANAGEMENT_ANY;
            this.technicianid = TECHNICIAN_ANY;
            this.name         = "";
            this.phone        = "";
            this.email        = "";
            this.address      = "";
            this.zip          = "";
            this.city         = "";
            this.orgnumber    = "";
            this.vismaref     = "";
            this.contact      = "";
            this.note         = "";
        }
    
        public string ZipCity
        {
            get { return StringUtils.Trim(zip + " " + city); }
        }

        public string AsText
        {
            get { return name + " (" + address + " " + city + ")"; }
        }

        public static string TypeAsString(int type)
        {
            if (type == TYPE_COMPANY) return STRING_TYPE_COMPANY;
            if (type == TYPE_PERSON)  return STRING_TYPE_PERSON;
            return "";
        }

        public override void Validate()
        {
            ValidateCondition(type         != TYPE_ANY,                               "Kundtyp måste anges");
            ValidateCondition(managementid != MANAGEMENT_ANY,                         "Kundansvarig måste anges");
            ValidateCondition(technicianid != TECHNICIAN_ANY || type == TYPE_PERSON,  "Ansvarig tekniker måste anges för denna kundtyp");
            name      = ValidateRange (MINLEN_NAME,      name,      MAXLEN_NAME,      "Felaktigt namn");
            phone     = ValidatePhone (MINLEN_PHONE,     phone,     MAXLEN_PHONE,     "Felaktigt telefonnummer");
            email     = ValidateEmail (MINLEN_EMAIL,     email,     MAXLEN_EMAIL,     "Felaktig e-postadress");
            address   = ValidateRange (MINLEN_ADDRESS,   address,   MAXLEN_ADDRESS,   "Felaktig adress");
            zip       = ValidateNumber(MINLEN_ZIP,       zip,       MAXLEN_ZIP,       "Felaktigt postnummer");
            city      = ValidateRange (MINLEN_CITY,      city,      MAXLEN_CITY,      "Felaktig ort");
            orgnumber = ValidateOrgnum(MINLEN_ORGNUMBER, orgnumber, MAXLEN_ORGNUMBER, "Felaktigt organisationsnummer");
            vismaref  = ValidateRange (MINLEN_VISMAREF,  vismaref,  MAXLEN_VISMAREF,  "Felaktig Vismakod");
            contact   = ValidateRange (MINLEN_CONTACT,   contact,   MAXLEN_CONTACT,   "Felaktig kontakt");
            note      = ValidateRange (MINLEN_NOTE,      note,      MAXLEN_NOTE,      "Felaktig notering");
        }
    }
}