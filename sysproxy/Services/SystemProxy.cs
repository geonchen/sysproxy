using System;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace sysproxy.Services
{
    public static class SystemProxy
    {
        [DllImport("wininet.dll")]
        public static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int dwBufferLength);
        public const int INTERNET_OPTION_SETTINGS_CHANGED = 39;
        public const int INTERNET_OPTION_REFRESH = 37;
        public const string LOCAL_ADDRESS = "localhost;127.*;10.*;172.16.*;172.17.*;172.18.*;172.19.*;172.20.*;172.21.*;172.22.*;172.23.*;172.24.*;172.25.*;172.26.*;172.27.*;172.28.*;172.29.*;172.30.*;172.31.*;172.32.*;192.168.*;<local>";
   
        static RegistryKey Key
        {
            get => Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);
        }

        static void Refresh()
        {
            InternetSetOption(IntPtr.Zero, INTERNET_OPTION_SETTINGS_CHANGED, IntPtr.Zero, 0);
            InternetSetOption(IntPtr.Zero, INTERNET_OPTION_REFRESH, IntPtr.Zero, 0);
        }

        public static void Disable()
        {
            var registry = Key;
            registry.SetValue("ProxyEnable", 0);
            registry.SetValue("ProxyServer", "");
            registry.SetValue("AutoConfigURL", "");
            registry.SetValue("ProxyOverride", "");
            Refresh();
        }

        public static void EnableGlobal(int port)
        {
            var registry = Key;
            registry.SetValue("ProxyEnable", 1);
            registry.SetValue("ProxyServer", $"http://127.0.0.1:{port}");
            registry.SetValue("AutoConfigURL", "");
            registry.SetValue("ProxyOverride","");
            Refresh();
        }

        public static void EnablePAC(string pacUrl)
        {
            var registry = Key;
            registry.SetValue("ProxyEnable", 0);
            registry.SetValue("ProxyServer", "");
            registry.SetValue("AutoConfigURL", pacUrl);
            registry.SetValue("ProxyOverride", "");
            Refresh();
        }


    }
}
