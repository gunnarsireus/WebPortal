using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using ServerLibrary.Utils;

namespace ServerLibrary.Model
{
    [Table("issues")]
    public class Issue : Validatable
    {
        public const int __STATUS_NONEXISTING = -1;
        public const int STATUS_ANY           = 0;
        public const int STATUS_PRELIMINARY   = 10;
        public const int STATUS_OPEN          = 20;
        public const int STATUS_STARTED       = 30;
        public const int STATUS_CLOSED        = 40;
        public const int STATUS_APPROVED      = 50;
        public const int __STATUS_ARCHIVED    = 59;
        public const int STATUS_FINISHED      = 60;
        public const int STATUS_INVOICED      = 61;
        public const int STATUS_REJECTED      = 70;

        public const string STRING_STATUS_NONEXISTING = "--";
        public const string STRING_STATUS_PRELIMINARY = "Preliminär";
        public const string STRING_STATUS_OPEN        = "Öppen";
        public const string STRING_STATUS_STARTED     = "Påbörjad";
        public const string STRING_STATUS_CLOSED      = "Stängd";
        public const string STRING_STATUS_APPROVED    = "Godkänd";
        public const string STRING_STATUS_FINISHED    = "Slutförd";
        public const string STRING_STATUS_INVOICED    = "Fakturerad";
        public const string STRING_STATUS_REJECTED    = "Avslagen";

        public const int PRIO_ANY    = 0;
        public const int PRIO_LOW    = 1;
        public const int PRIO_NORMAL = 2;
        public const int PRIO_HIGH   = 3;
        public const int PRIO_URGENT = 4;

        public const string STRING_PRIO_LOW    = "Låg";
        public const string STRING_PRIO_NORMAL = "Normal";
        public const string STRING_PRIO_HIGH   = "Hög";
        public const string STRING_PRIO_URGENT = "Akut";

        public const int RESPONSIBLE_ANY      = 0;
        public const int RESPONSIBLE_INTERNAL = 41;
        public const int RESPONSIBLE_EXTERNAL = 42;

        public const string STRING_RESPONSIBLE_INTERNAL = "Renew";
        public const string STRING_RESPONSIBLE_EXTERNAL = "Kund";

        public const int AREATYPE_ANY       = 0;
        public const int AREATYPE_COMMON    = 10;
        public const int AREATYPE_APARTMENT = 20;

        public const string STRING_AREATYPE_COMMON    = "Gemensamhet";
        public const string STRING_AREATYPE_APARTMENT = "Lägenhet";

        // Database constraints
        public const int MINLEN_NAME        = 2;
        public const int MAXLEN_NAME        = 45;
        public const int MINLEN_DESCRIPTION = 0;
        public const int MAXLEN_DESCRIPTION = 400;
        public const int MINLEN_VISMAREF    = 0;
        public const int MAXLEN_VISMAREF    = 12;
        public const int MINLEN_FIRSTNAME   = 0;
        public const int MAXLEN_FIRSTNAME   = 45;
        public const int MINLEN_LASTNAME    = 0;
        public const int MAXLEN_LASTNAME    = 45;
        public const int MINLEN_PHONE       = 0;
        public const int MAXLEN_PHONE       = 20;
        public const int MINLEN_ADDRESS     = 0;
        public const int MAXLEN_ADDRESS     = 45;
        public const int MINLEN_FLOOR       = 0;
        public const int MAXLEN_FLOOR       = 10;
        public const int MINLEN_APARTMENT   = 0;
        public const int MAXLEN_APARTMENT   = 10;
        public const int MINLEN_EMAIL       = 0;
        public const int MAXLEN_EMAIL       = 60;

        [Key]
        public int    id           { get; set; }
        public int    customerid   { get; set; }
        public int    residentid   { get; set; }
        public int    assignedid   { get; set; }
        public int    issueclassid { get; set; }
        public int    areatype     { get; set; }
        public int    status       { get; set; }
        public int    prio         { get; set; }
        public int    responsible  { get; set; }
        public long   startdate    { get; set; }
        public long   enddate      { get; set; }
        public string name         { get; set; }
        public string description  { get; set; }
        public string vismaref     { get; set; }
        public double latitude     { get; set; }
        public double longitude    { get; set; }
        public string firstname    { get; set; }
        public string lastname     { get; set; }
        public string phone        { get; set; }
        public string address      { get; set; }
        public string floor        { get; set; }
        public string apartment    { get; set; }
        public string email        { get; set; }

        public Issue()
        {
            this.id           = 0;
            this.customerid   = Customer.CUSTOMER_ANY;
            this.residentid   = Account.ACCOUNT_ANY;
            this.assignedid   = Account.ACCOUNT_ANY;
            this.issueclassid = IssueClass.ISSUECLASS_ANY;
            this.areatype     = AREATYPE_ANY;
            this.status       = STATUS_ANY;
            this.prio         = PRIO_ANY;
            this.responsible  = RESPONSIBLE_ANY;
            this.startdate    = 0;
            this.enddate      = 0;
            this.name         = "";
            this.description  = "";
            this.vismaref     = "";
            this.latitude     = 0;
            this.longitude    = 0;
            this.firstname    = "";
            this.lastname     = "";
            this.phone        = "";
            this.address      = "";
            this.floor        = "";
            this.apartment    = "";
            this.email        = "";
        }
    
        public static int[] GetStatusCollection()
        {
            return new int[]
            {
                STATUS_PRELIMINARY,
                STATUS_OPEN       ,
                STATUS_STARTED    ,
                STATUS_CLOSED     ,
                STATUS_APPROVED   ,
                STATUS_FINISHED   ,
                STATUS_INVOICED   ,
                STATUS_REJECTED
            };
        }

        public static string StatusAsString(int status)
        {
            if (status == __STATUS_NONEXISTING) { return STRING_STATUS_NONEXISTING; }
            if (status == STATUS_PRELIMINARY  ) { return STRING_STATUS_PRELIMINARY; }
            if (status == STATUS_OPEN         ) { return STRING_STATUS_OPEN;        }
            if (status == STATUS_STARTED      ) { return STRING_STATUS_STARTED;     }
            if (status == STATUS_CLOSED       ) { return STRING_STATUS_CLOSED;      }
            if (status == STATUS_APPROVED     ) { return STRING_STATUS_APPROVED;    }
            if (status == STATUS_FINISHED     ) { return STRING_STATUS_FINISHED;    }
            if (status == STATUS_INVOICED     ) { return STRING_STATUS_INVOICED;    }
            if (status == STATUS_REJECTED     ) { return STRING_STATUS_REJECTED;    }
            return "";
        }

        public static string StatusAsBackgroundColor(int status)
        {
            if (status == STATUS_PRELIMINARY) return "#BCBDC0";
            if (status == STATUS_OPEN       ) return "#F5E30B";
            if (status == STATUS_STARTED    ) return "#F35F00";
            if (status == STATUS_CLOSED     ) return "#25AAE1";
            if (status == STATUS_APPROVED   ) return "#83BF65";
            if (status == STATUS_FINISHED   ) return "#F3F3F3";
            if (status == STATUS_INVOICED   ) return "#4D5359";
            if (status == STATUS_REJECTED   ) return "#060606";
            return "#FFFFFF";
        }

        public static string StatusAsTextColor(int status)
        {
            if (status == STATUS_PRELIMINARY) return "#000000";
            if (status == STATUS_OPEN       ) return "#000000";
            if (status == STATUS_STARTED    ) return "#FFFFFF";
            if (status == STATUS_CLOSED     ) return "#FFFFFF";
            if (status == STATUS_APPROVED   ) return "#FFFFFF";
            if (status == STATUS_FINISHED   ) return "#000000";
            if (status == STATUS_INVOICED   ) return "#FFFFFF";
            if (status == STATUS_REJECTED   ) return "#FFFFFF";
            return "#000000";
        }

        public static int NextProbableStatus(int status)
        {
            if (status == STATUS_PRELIMINARY) { return STATUS_OPEN;     }
            if (status == STATUS_OPEN       ) { return STATUS_STARTED;  }
            if (status == STATUS_STARTED    ) { return STATUS_CLOSED;   }
            if (status == STATUS_CLOSED     ) { return STATUS_APPROVED; }
            if (status == STATUS_APPROVED   ) { return STATUS_INVOICED; }
            if (status == STATUS_FINISHED   ) { return 0;               }
            if (status == STATUS_INVOICED   ) { return 0;               }
            if (status == STATUS_REJECTED   ) { return 0;               }
            return 0;
        }

        public static int[] NextPossibleStatuses(int status)
        {
            if (status == __STATUS_NONEXISTING) { return new int[] { STATUS_PRELIMINARY               }; }
            if (status == STATUS_PRELIMINARY  ) { return new int[] { STATUS_OPEN,     STATUS_REJECTED }; }
            if (status == STATUS_OPEN         ) { return new int[] { STATUS_STARTED,  STATUS_REJECTED }; }
            if (status == STATUS_STARTED      ) { return new int[] { STATUS_CLOSED,   STATUS_REJECTED }; }
            if (status == STATUS_CLOSED       ) { return new int[] { STATUS_STARTED,  STATUS_APPROVED }; }
            if (status == STATUS_APPROVED     ) { return new int[] { STATUS_FINISHED, STATUS_INVOICED }; }
            if (status == STATUS_FINISHED     ) { return new int[] {}; }
            if (status == STATUS_INVOICED     ) { return new int[] {}; }
            if (status == STATUS_REJECTED     ) { return new int[] {}; }
            return new int[] {};
        }

        public static bool IsValidStatus(int status)
        {
            if (status == __STATUS_NONEXISTING) { return true; }
            if (status == STATUS_PRELIMINARY)   { return true; }
            if (status == STATUS_OPEN)          { return true; }
            if (status == STATUS_STARTED)       { return true; }
            if (status == STATUS_CLOSED)        { return true; }
            if (status == STATUS_APPROVED)      { return true; }
            if (status == STATUS_FINISHED)      { return true; }
            if (status == STATUS_INVOICED)      { return true; }
            if (status == STATUS_REJECTED)      { return true; }
            return false;
        }

        public static string PrioAsString(int prio)
        {
            if (prio == PRIO_LOW   ) { return STRING_PRIO_LOW;    }
            if (prio == PRIO_NORMAL) { return STRING_PRIO_NORMAL; }
            if (prio == PRIO_HIGH  ) { return STRING_PRIO_HIGH;   }
            if (prio == PRIO_URGENT) { return STRING_PRIO_URGENT; }
            return "";
        }

        public static bool IsValidPrio(int prio)
        {
            if (prio == PRIO_LOW)    { return true; }
            if (prio == PRIO_NORMAL) { return true; }
            if (prio == PRIO_HIGH)   { return true; }
            if (prio == PRIO_URGENT) { return true; }
            return false;
        }

        public static string ResponsibleAsString(int responsible)
        {
            if (responsible == RESPONSIBLE_INTERNAL) { return STRING_RESPONSIBLE_INTERNAL; }
            if (responsible == RESPONSIBLE_EXTERNAL) { return STRING_RESPONSIBLE_EXTERNAL; }
            return "";
        }

        public static bool IsValidResponsible(int responsible)
        {
            if (responsible == RESPONSIBLE_INTERNAL) { return true; }
            if (responsible == RESPONSIBLE_EXTERNAL) { return true; }
            return false;
        }

        public static string AreaTypeAsString(int areatype)
        {
            if (areatype == AREATYPE_COMMON   ) { return STRING_AREATYPE_COMMON;    }
            if (areatype == AREATYPE_APARTMENT) { return STRING_AREATYPE_APARTMENT; }
            return "";
        }

        public static bool IsValidAreaType(int areatype)
        {
            if (areatype == AREATYPE_COMMON)    { return true; }
            if (areatype == AREATYPE_APARTMENT) { return true; }
            return false;
        }

        public bool CollidesWith(Issue that)
        {
            if (that != null && that.assignedid != 0 && assignedid != 0)    //No point in checking if either one of the issues are unassigned
            {
                return (startdate < that.enddate) && (that.startdate < enddate) && (that.assignedid == assignedid);
            }
            return false;
        }

        public override void Validate()
        {
            ValidateCondition(customerid   != Customer.CUSTOMER_ANY,     "Ogiltig kund");
            ValidateCondition(residentid   != Account.ACCOUNT_ANY,       "Ogiltig boende");
            ValidateCondition(assignedid   != Account.ACCOUNT_ANY,       "Ogiltig tilldelad");
            ValidateCondition(issueclassid != IssueClass.ISSUECLASS_ANY, "Ogiltig ärendeklass");

            ValidateCondition(IsValidAreaType(areatype),       "Felaktigt utrymme");
            ValidateCondition(IsValidStatus(status),           "Felaktig status");
            ValidateCondition(IsValidPrio(prio),               "Felaktig prioritet");
            ValidateCondition(IsValidResponsible(responsible), "Felaktig utförare");

            name        = ValidateRange(MINLEN_NAME,        name,        MAXLEN_NAME,        "Felaktig titel");
            description = ValidateRange(MINLEN_DESCRIPTION, description, MAXLEN_DESCRIPTION, "Felaktig beskrivning");
            vismaref    = ValidateRange(MINLEN_VISMAREF,    vismaref,    MAXLEN_VISMAREF,    "Felaktig Vismakod");
            firstname   = ValidateRange(MINLEN_FIRSTNAME,   firstname,   MAXLEN_FIRSTNAME,   "Felaktigt förnamn");
            lastname    = ValidateRange(MINLEN_LASTNAME,    lastname,    MAXLEN_LASTNAME,    "Felaktigt efternamn");
            phone       = ValidatePhone(MINLEN_PHONE,       phone,       MAXLEN_PHONE,       "Felaktigt telefonnummer");
            address     = ValidateRange(MINLEN_ADDRESS,     address,     MAXLEN_ADDRESS,     "Felaktig adress");
            floor       = ValidateRange(MINLEN_FLOOR,       floor,       MAXLEN_FLOOR,       "Felaktigt våningsplan");
            apartment   = ValidateRange(MINLEN_APARTMENT,   apartment,   MAXLEN_APARTMENT,   "Felaktig lägenhetsnummer");
            email       = ValidateRange(MINLEN_EMAIL,       email,       MAXLEN_EMAIL,       "Felaktig e-postadress");

            ValidateGreaterThan(startdate, 0, "Felaktigt startdatum/tid");
            ValidateGreaterThan(enddate,   0, "Felaktigt slutdatum/tid");
            ValidateDateTimePeriod(startdate, enddate, "Startdatum kan inte vara senare än slutdatum");
        }
    }
}