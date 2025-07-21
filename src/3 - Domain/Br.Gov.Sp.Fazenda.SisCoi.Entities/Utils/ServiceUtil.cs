using Microsoft.Win32;
using System;

namespace SisCOI_crt.Utils
{
    class ServiceUtil
    {
        public static string GetServicePath(string serviceName)
        {
            string registryPath = @"SYSTEM\CurrentControlSet\Services\" + serviceName;
            RegistryKey keyHKLM = Registry.LocalMachine;

            using (RegistryKey key = keyHKLM.OpenSubKey(registryPath))
            {
                string value = key.GetValue("ImagePath").ToString();
                var path = Environment.ExpandEnvironmentVariables(value);

                if (path.StartsWith("\"", StringComparison.Ordinal))
                {
                    return path.Split('"')[1];
                }
                else
                {
                    return path.Split(' ')[0];
                }
            }
        }
    }
}
