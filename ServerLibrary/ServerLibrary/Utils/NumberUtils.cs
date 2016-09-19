using System.Globalization;

namespace ServerLibrary.Utils
{
    public static class NumberUtils
    {
        public static int RAWFORMAT = -1;

        private static CultureInfo culture = new CultureInfo("sv-SE");
        private static string[] DOUBLE_FORMATS =
        {
            "{0:0}",
            "{0:0.0}",
            "{0:0.00}",
            "{0:0.000}",
            "{0:0.0000}",
            "{0:0.00000}",
            "{0:0.000000}",
            "{0:0.0000000}",
            "{0:0.00000000}",
            "{0:0.000000000}",
            "{0:0.0000000000}"
        };
        private static string[] INTEGER_FORMATS =
        {
            "{0}",
            "{0:0}",
            "{0:00}",
            "{0:000}",
            "{0:0000}",
            "{0:00000}",
            "{0:000000}",
            "{0:0000000}",
            "{0:00000000}",
            "{0:000000000}",
            "{0:0000000000}"
        };

        public static string FromDouble(double value, int decimals=2)
        {
            if (decimals == RAWFORMAT)
            {
                return value.ToString(culture);
            }
            return string.Format(culture, DOUBLE_FORMATS[decimals], value);
        }

        public static string FromDoubleWithDot(double value, int decimals=2)
        {
            if (decimals == RAWFORMAT)
            {
                return value.ToString(System.Globalization.CultureInfo.InvariantCulture);
            }
            return string.Format(System.Globalization.CultureInfo.InvariantCulture, DOUBLE_FORMATS[decimals], value);
        }

        public static string FromInteger(int value, int padding=0)
        {
            return string.Format(culture, INTEGER_FORMATS[padding], value);
        }

        public static bool ToDouble(string s, out double value)
        {
            return double.TryParse(s, NumberStyles.AllowDecimalPoint, culture, out value);
        }

        public static bool ToDoubleWithDot(string s, out double value)
        {
            return double.TryParse(s, NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out value);
        }

        public static bool ToInteger(string s, out int value)
        {
            return int.TryParse(s, NumberStyles.None, culture, out value);
        }
    }
}
