using System;
using System.Text.RegularExpressions;
using ServerLibrary.Utils;

namespace ServerLibrary.Model
{
    /*
     * Base class for validatable model objects.
     * 
     * Note: all validated strings are automatically trimmed during validation.
     */
    public abstract class Validatable
    {
        public const int NOMIN = -1;
        public const int NOMAX = -1;

        public abstract void Validate();

        // Integer

        protected void ValidateRange(long minincl, long value, long maxincl, string error)
        {
            if ((minincl != NOMIN && value < minincl) || (maxincl != NOMAX && value > maxincl))
            {
                throw new ServerValidateException(error);
            }
        }

        protected void ValidateAtLeast(long value, long minincl, string error)
        {
            if (value < minincl)
            {
                throw new ServerValidateException(error);
            }
        }

        protected void ValidateAtMost(long value, long maxincl, string error)
        {
            if (value > maxincl)
            {
                throw new ServerValidateException(error);
            }
        }

        protected void ValidateGreaterThan(long value, long limit, string error)
        {
            if (value <= limit)
            {
                throw new ServerValidateException(error);
            }
        }

        protected void ValidateLessThan(long value, long limit, string error)
        {
            if (value >= limit)
            {
                throw new ServerValidateException(error);
            }
        }

        // String

        protected string ValidateRange(long minincl, string value, long maxincl, string error)
        {
            value = CheckAndTrim(value, error);
            int length = value.Length;
            if ((minincl != NOMIN && length < minincl) || (maxincl != NOMAX && length > maxincl))
            {
                throw new ServerValidateException(error);
            }
            return value;
        }

        protected string ValidatePhone(long minincl, string value, long maxincl, string error)
        {
            value = ValidateRange(minincl, value, maxincl, error);
            if (value.Length > 0)
            {
                value = ValidateRegex(value, @"^[\d-]+$", error);   // 1 or more digits and/or dashes
            }
            return value;
        }

        protected string ValidateEmail(long minincl, string value, long maxincl, string error)
        {
            value = ValidateRange(minincl, value, maxincl, error);
            if (value.Length > 0)
            {
                if (!MailUtils.Instance.IsValid(value))
                {
                    throw new ServerValidateException(error);
                }
            }
            return value;
        }

        protected string ValidateNumber(long minincl, string value, long maxincl, string error)
        {
            value = ValidateRange(minincl, value, maxincl, error);
            if (value.Length > 0)
            {
                value = ValidateRegex(value, @"^[\d]+$", error);   // 1 or more digits
            }
            return value;
        }

        protected string ValidateOrgnum(long minincl, string value, long maxincl, string error)
        {
            value = ValidateRange(minincl, value, maxincl, error);
            if (value.Length > 0)
            {
                value = ValidateRegex(value, @"^[\d]{6}\-[\d]{4}$", error);   // NNNNNN-NNNN
            }
            return value;
        }

        // Regex

        protected string ValidateRegex(string value, string pattern, string error)
        {
            value = CheckAndTrim(value, error);
            if (!Regex.IsMatch(value, pattern))
            {
                throw new ServerValidateException(error);
            }
            return value;
        }

        // Security

        protected void ValidatePassword(int minlength, string password)
        {
            password = CheckAndTrim(password, "Lösenord inte angivet");
            string error = password.IsSecurePassword(pwlThreshold: minlength);
            if (error != null)
            {
                throw new ServerValidateException(error);
            }
        }

        // Date and time

        protected long ValidateDate(string date, string error)
        {
            return ValidateDateTime(date, "12:00:00", error);
        }

        protected long ValidateDateTime(string date, string time, string error)
        {
            date = CheckAndTrim(date, error);
            long timestamp = DateUtils.ConvertToTimeStamp(date + " " + time);
            if (timestamp == 0)
            {
                throw new ServerValidateException(error);
            }
            return timestamp;
        }

        protected void ValidateDateTimePeriod(long t1, long t2, string error)
        {
            if (t1 == 0 || t2 == 0 || t1 >= t2)
            {
                throw new ServerValidateException(error);
            }
        }

        protected void ValidateFutureDateTime(long t, string error)
        {
            long now = DateUtils.TimeStamp;
            if (t <= now)
            {
                throw new ServerValidateException(error);
            }
        }

        // Generic

        protected void ValidateEquals(string s1, string s2, string error)
        {
            if (s1 == null || s2 == null || s1.Equals(s2) == false)
            {
                throw new ServerValidateException(error);
            }
        }

        protected void ValidateCondition(bool condition, string error)
        {
            if (!condition)
            {
                throw new ServerValidateException(error);
            }
        }

        // Private helper

        private string CheckAndTrim(string value, string error)
        {
            if (value == null)
            {
                throw new ServerValidateException(error);
            }
            return StringUtils.Trim(value);
        }
    }
}
