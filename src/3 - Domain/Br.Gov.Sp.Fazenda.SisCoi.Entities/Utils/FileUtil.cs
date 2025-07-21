using System;
using System.Collections.Generic;
using System.IO;

namespace SisCOI_crt.Utils
{
    class FileUtil
    {
        private static string defaultConfigFile = @"SisCOI_crt.json";
        private static string defaultOutputPath = @"\\SRVCSFS01\siscoi$\CERT";

        public static string GetConfigFile(string configFile)
        {
            string ret;
            if (!string.IsNullOrEmpty(configFile))
            {
                ret = configFile;
            }
            else
            {
                ret = defaultConfigFile;
            }
            return ret;
        }

        public static string GetOutputPath(string outputPath)
        {
            string ret = string.Empty;
            try
            {
                Directory.CreateDirectory(outputPath);
                ret = outputPath;
            }
            catch (Exception)
            {
                Directory.CreateDirectory(defaultOutputPath);
                ret = defaultOutputPath;
            }
            return ret;
        }

        public static IEnumerable<string> GetPathList(string[] paths)
        {
            List<string> ret = new List<string>();
            if (paths != null)
            {
                ret.AddRange(paths);
            }
            return ret;
        }
    }
}
