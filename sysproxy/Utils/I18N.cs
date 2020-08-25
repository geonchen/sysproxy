using System.Collections.Generic;
using System.Globalization;
using System.IO;
using sysproxy.Properties;

namespace sysproxy.Utils
{
    public class I18N
    {
        protected static Dictionary<string, string> Strings;

        static void Init(string res)
        {
            using (var sr = new StringReader(res))
            {
                foreach (var line in sr.NonWhiteSpaceLines())
                {
                    if (line[0] == '#')
                        continue;

                    var pos = line.IndexOf('=');
                    if (pos < 1)
                        continue;
                    Strings[line.Substring(0, pos)] = line.Substring(pos + 1);
                }
            }
        }


        static I18N()
        {
            Strings = new Dictionary<string, string>();
            string name = CultureInfo.CurrentCulture.Name;
            if (name.StartsWith("zh"))
            {
                Init(Resources.cn);
            }
        }

        public static string GetString(string key)
        {
            if (Strings.ContainsKey(key))
            {
                return Strings[key];
            }
            else
            {
                return key;
            }
        }


    }
}
