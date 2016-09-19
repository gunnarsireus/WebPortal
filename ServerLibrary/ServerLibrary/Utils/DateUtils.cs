using System;

namespace ServerLibrary.Utils
{
    public static class DateUtils
    {
        private static DateTime BASETIME = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public const long MINUTE = 60;
        public const long HOUR   = MINUTE * MINUTE;
        public const long DAY    = HOUR   * 24;

        // 1440123400
        public static long TimeStamp
        {
            get
            {
                TimeSpan t = DateTime.UtcNow - BASETIME;
                int secondsSinceEpoch = (int)t.TotalSeconds;
                return secondsSinceEpoch;
            }
        }

        // 1...31
        public static int DaysInMonth(int year, int month)
        {
            return DateTime.DaysInMonth(year, month);
        }

        // 0...366
        public static int DaysFromNow(int month, int day)
        {
            DateTime d0 = DateTime.Now;
            DateTime d1 = new DateTime(d0.Year, month, day);
            int diff = (int) (d1 - d0).TotalDays;
            if (diff < 0)
            {
                d1 =  new DateTime(d0.Year + 1, month, day);
            }
            return (int) (d1 - d0).TotalDays;
        }

        // 1440123400 -> 144012... where output is previous 00:00 
        public static long GetDayStartTimestamp(long timestamp)
        {
            DateTime s = DateTime.SpecifyKind(BASETIME.AddSeconds(timestamp), DateTimeKind.Utc).ToLocalTime();
            return timestamp - (s.Hour * 3600) - (s.Minute * 60) - s.Second;
        }

        // 1440123400 -> 144012... where output is next 00:00 
        public static long GetDayEndTimestamp(long timestamp)
        {
            return GetDayStartTimestamp(timestamp + 86400);
        }

        // YYYY, MM, DD -> 1440123400
        public static long ConvertToTimeStamp(int year, int month, int day)
        {
            string date = String.Format("{0}-{1:00}-{2:00} 00:00:00", year, month, day);
            return ConvertToTimeStamp(date);
        }

        // 2015-01-10 12:34:00 -> 1440123400
        public static long ConvertToTimeStamp(string date)
        {
            try
            {
                DateTime s = DateTime.Parse(date).ToUniversalTime();
                TimeSpan t = s - BASETIME;
                int secondsSinceEpoch = (int)t.TotalSeconds;
                return secondsSinceEpoch;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        // 1440123400 -> 2015-01-10
        public static string ConvertToDateString(long timestamp)
        {
            if (timestamp == 0) { return ""; }
            DateTime s = DateTime.SpecifyKind(BASETIME.AddSeconds(timestamp), DateTimeKind.Utc).ToLocalTime();
            return s.ToString("yyyy-MM-dd");
        }

        // 1440123400 -> 12:34
        public static string ConvertToTimeString(long timestamp)
        {
            if (timestamp == 0) { return ""; }
            DateTime s = DateTime.SpecifyKind(BASETIME.AddSeconds(timestamp), DateTimeKind.Utc).ToLocalTime();
            return s.ToString("HH:mm");
        }

        // 1440123400 -> 2015-01-10 12:34
        public static string ConvertToDateTimeString(long timestamp)
        {
            if (timestamp == 0) { return ""; }
            DateTime s = DateTime.SpecifyKind(BASETIME.AddSeconds(timestamp), DateTimeKind.Utc).ToLocalTime();
            return s.ToString("yyyy-MM-dd HH:mm");
        }

        // 1440123400 -> 2015-01-10T12:34:00
        public static string ConvertToISO8601DateTimeString(long timestamp)
        {
            if (timestamp == 0) { return ""; }
            DateTime s = DateTime.SpecifyKind(BASETIME.AddSeconds(timestamp), DateTimeKind.Utc).ToLocalTime();
            return s.ToString("yyyy-MM-ddTHH:mm:ss");
        }
    }
}
