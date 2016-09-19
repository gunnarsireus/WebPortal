using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using ServerLibrary.Utils;

namespace ServerLibrary.Model
{
    [Table("timetypes")]
    public class TimeType : Activatable
    {
        public const int TIMETYPE_ANY = 0;

        // Database constraints (according to Visma docs)
        public const int MINLEN_CODE        = 1;
        public const int MAXLEN_CODE        = 16;
        public const int MINLEN_NAME        = 1;
        public const int MAXLEN_NAME        = 30;
        public const int MINLEN_UNIT        = 1;
        public const int MAXLEN_UNIT        = 4;
        public const int MINLEN_DESCRIPTION = 0;
        public const int MAXLEN_DESCRIPTION = 30;

        [Key]
        public int    id          { get; set; }
        public string code        { get; set; } // Visma field "ADK_ARTICLE_NUMBER"    (length 16)
        public string name        { get; set; } // Visma field "ADK_ARTICLE_NAME"      (length 30)
        public string unit        { get; set; } // Visma field "ADK_ARTICLE_UNIT_CODE" (length 4)
        public string description { get; set; } // Visma field "ADK_ARTICLE_NAME_X"    (length 30)
        // Activatable.active

        public TimeType()
        {
            this.id          = 0;
            this.code        = "";
            this.name        = "";
            this.unit        = "";
            this.description = "";
            // Activatable.active
        }

        public override void Validate()
        {
             code        = ValidateRange(MINLEN_CODE,        code,        MAXLEN_CODE,        "Felaktig Vismakod");
             name        = ValidateRange(MINLEN_NAME,        name,        MAXLEN_NAME,        "Felaktigt namn");
             unit        = ValidateRange(MINLEN_UNIT,        unit,        MAXLEN_UNIT,        "Felaktig enhet");
             description = ValidateRange(MINLEN_DESCRIPTION, description, MAXLEN_DESCRIPTION, "Felaktig beskrivning");
        }
    }
}