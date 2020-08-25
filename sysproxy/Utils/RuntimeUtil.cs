using Microsoft.Win32;
using System;


namespace sysproxy.Utils
{
    public static class RuntimeUtil
    {
        public static bool IsWinVistaOrHigher()
        {
            return Environment.OSVersion.Version.Major > 5;
        }

        // See: https://msdn.microsoft.com/en-us/library/hh925568(v=vs.110).aspx
        public static bool GetVersionFromRegistry()
        {
            using (RegistryKey ndpKey = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\NET Framework Setup\NDP\v3.5", false))
            {
                if (ndpKey?.GetValue("Install") != null)
                {
                    var installKey = (int)ndpKey?.GetValue("Install");
                    if (installKey == 1)
                        return true;
                }
            }
            return false;
        }
    }
}
