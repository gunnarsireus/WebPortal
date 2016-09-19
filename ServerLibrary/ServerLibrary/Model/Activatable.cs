using System;

namespace ServerLibrary.Model
{
    public abstract class Activatable : Validatable
    {
        public const int ACTIVE_ANY = 99;
        public const int INACTIVE   = 0;
        public const int ACTIVE     = 1;

        public int active { get; set; }

        public Activatable()
        {
            this.active = ACTIVE;
        }

        public bool IsActive()
        {
            return (active == ACTIVE);
        }

        public bool IsInactive()
        {
            return (active == INACTIVE);
        }

        public void SetActive(bool active)
        {
            this.active = active ? ACTIVE : INACTIVE;
        }

        public static string ActiveAsString(int active)
        {
            return (active == 0) ? "Nej" : "Ja";
        }
    }
}
