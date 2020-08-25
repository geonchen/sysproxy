using Microsoft.Win32;
using System;
using sysproxy.Utils;

namespace sysproxy.Services
{
    public class AutoStart
    {
        const string key = "sysproxy";
        static string path = FileUtil.GetPath("sysproxy.exe");
        public static void Set(bool enabled)
        {
            RegistryKey runKey = null;
            try
            {
                runKey = OpenUserRegKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                if (runKey == null)
                {
                    MsgBox.Error(@"Cannot find HKCU\Software\Microsoft\Windows\CurrentVersion\Run");
                    return;
                }
                if (enabled)
                {
                    runKey.SetValue(key, path);
                }
                else
                {
                    runKey.DeleteValue(key);
                }
            }
            catch (Exception)
            {
                //Can't OpenRemoteBaseKey;
            }
        }

        static RegistryKey OpenUserRegKey(string name, bool writable)
        {
            return RegistryKey.OpenRemoteBaseKey(RegistryHive.CurrentUser, "").OpenSubKey(name, writable);
        }

        public static bool Check()
        {
            try
            {
                RegistryKey runKey = OpenUserRegKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                if (runKey == null)
                {
                    MsgBox.Error(@"Cannot find HKCU\Software\Microsoft\Windows\CurrentVersion\Run");
                    return false;
                }
                string[] runList = runKey.GetValueNames();
                foreach (string item in runList)
                {
                    if (item.Equals(key, StringComparison.OrdinalIgnoreCase))
                        return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
