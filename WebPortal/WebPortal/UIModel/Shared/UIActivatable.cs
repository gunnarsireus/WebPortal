using System;
using ServerLibrary.Model;

namespace WebPortal.UIModel
{
    public static class UIActivatable
    {
        public static string ActiveAsString(int active)
        {
            return (active == 1) ? "Aktiv" : "Inaktiv";
        }

        public static string ActiveAsYesNo(int active)
        {
            return (active == 1) ? "Ja" : "Nej";
        }

        public static bool ActiveAsBool(int active)
        {
            return (active == 1);
        }

        public static string ActiveAsString(bool active)
        {
            return active ? "Aktiv" : "Inaktiv";
        }

        public static string ActiveAsYesNo(bool active)
        {
            return active ? "Ja" : "Nej";
        }

        public static int ActiveAsInt(bool active)
        {
            return active ? 1 : 0;
        }
    }
}