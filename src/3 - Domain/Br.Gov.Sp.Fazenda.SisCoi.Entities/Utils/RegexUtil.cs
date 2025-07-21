using System;
using System.Text.RegularExpressions;

namespace SisCOI_crt.Utils
{
    class RegexUtil
    {
        private static string defaultThumbprintRegex = @"[A-Za-z0-9]{40,40}";
        private static string defaultBase64Regex = @"^(?:[A-Za-z0-9+/]{4}){11,}(?:[A-Za-z0-9+/]{2}==|[A-Za-z0-9+/]{3}=)?$";
        private static string defaultDBConfigRegex = @"\b([\w\.])+\b";

        public static Regex GetThumbprintRegex(string thumbprintRegex)
        {
            Regex ret;
            try
            {
                ret = new Regex(thumbprintRegex, RegexOptions.Compiled);
            }
            catch (Exception)
            {
                ret = new Regex(defaultThumbprintRegex, RegexOptions.Compiled);
            }
            return ret;
        }

        public static Regex GetBase64Regex(string base64Regex)
        {
            Regex ret;
            try
            {
                ret = new Regex(base64Regex, RegexOptions.Compiled);
            }
            catch (Exception)
            {
                ret = new Regex(defaultBase64Regex, RegexOptions.Compiled);
            }
            return ret;
        }

        public static Regex GetDBConfigRegex(string dbConfigRegex)
        {
            Regex ret;
            try
            {
                ret = new Regex(dbConfigRegex, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            }
            catch (Exception)
            {
                ret = new Regex(defaultDBConfigRegex, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            }
            return ret;
        }
    }
}
