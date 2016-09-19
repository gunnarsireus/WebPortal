using System;

namespace WebPortal.Utils
{
    public static class WebConfig
    {
        public static bool LoggingOn
        {
            get
            {
                string value = System.Web.Configuration.WebConfigurationManager.AppSettings["LoggingOn"];
                if (!String.IsNullOrWhiteSpace(value))
                {
                    if (value.Equals("true"))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public static string EconomyUrl
        {
            get
            {
                string value = System.Web.Configuration.WebConfigurationManager.AppSettings["EconomyUrl"];
                if (!String.IsNullOrWhiteSpace(value))
                {
                    return value;
                }
                return "";
            }
        }

        public static string MobileProxyUrl
        {
            get
            {
                string value = System.Web.Configuration.WebConfigurationManager.AppSettings["MobileProxyUrl"];
                if (!String.IsNullOrWhiteSpace(value))
                {
                    return value;
                }
                return "";
            }
        }
    }
}
