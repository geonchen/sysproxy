using sysproxy.Utils;
using System;
using System.IO;
using System.Runtime.Serialization;

namespace sysproxy.Models
{
    public enum ProxyMode
    {
        Disable,
        Global,
        PAC
    }

    [DataContract]
    public sealed class Setting
    {
        public const string SETTING_FILE = "gui-setting.json";
        [DataMember(Name = "Mode")]
        public ProxyMode Mode;
        [DataMember(Name = "LocalPort")]
        public int LocalPort;
        [DataMember(Name = "RemotePort")]
        public int RemotePort;
        [DataMember(Name = "RemoteClientPath")]
        public string RemoteClientPath;
        [DataMember(Name = "RemoteClientArgs")]
        public string RemoteClientArgs;
        [DataMember(Name = "HideClient")]
        public bool HideClient;
        public static Setting Instance { get; private set; } = Load();

        public event EventHandler Saved;

        private static Setting Load()
        {
            Setting setting = null;
            try
            {
                string settingContent = File.ReadAllText(FileUtil.GetPath(SETTING_FILE));
                setting = JsonUtil.DeserializeObject<Setting>(settingContent);
                if (setting.LocalPort == 0)
                    setting.LocalPort = 8848;
                if (setting.RemotePort == 0)
                    setting.RemotePort = 1080;
            }
            catch (Exception e)
            {
                if (e is FileNotFoundException)
                {
                    setting = new Setting
                    {
                        Mode = ProxyMode.Disable,
                        LocalPort = 8848,
                        RemotePort = 1080
                    };
                }
            }
            return setting;
        }

        public void Save()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(File.Open(FileUtil.GetPath(SETTING_FILE), FileMode.Create)))
                {
                    string jsonString = JsonUtil.SerializeObject(Instance);
                    sw.Write(jsonString);
                    sw.Flush();
                }
                Saved.Invoke(this, EventArgs.Empty);
            }
            catch (Exception)
            {
                MsgBox.Error("Save setting file fail");
            }
        }


    }
}
