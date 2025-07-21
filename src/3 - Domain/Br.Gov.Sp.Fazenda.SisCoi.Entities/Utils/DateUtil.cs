using System;

namespace SisCOI_crt.Utils
{
    class DateUtil
    {
        private static string defaultDateFormat = @"yyyy-MM-dd HH:mm:ss";

        public static string GetDateFormat(string dateFormat)
        {
            string ret = dateFormat;

            try
            {
                DateTime.Now.ToString(dateFormat);
            }
            catch (Exception)
            {
                ret = defaultDateFormat;
            }

            return ret;
        }
    }
}
