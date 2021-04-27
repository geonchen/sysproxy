using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.IO;
using sysproxy.Properties;
using sysproxy.Utils;
using sysproxy.Models;
using sysproxy.Presenters;
using System.Globalization;
using System.Collections;
using System.Threading;

namespace sysproxy.Services
{
    public class PACServer:IService
    {
        private const string folder = "Rule";
        private const string gfwlist_file = "gfwlist.txt";
        private const string user_rule_file = "user-rule.txt";
        private const string abp_js_file = "abp.js";
        private const string pac_file = "pac.txt";
        private string directory = string.Empty;
        private int localPort;
        private int remotePort;
        private int pacPort;
        public string pac_url;
        private HttpListener listener;
        private string script;

        public event EventHandler ShowBalloonTip;

        public event EventHandler FileChanged;

        public PACServer(Setting setting)
        {
            localPort = setting.LocalPort;
            remotePort = setting.RemotePort;
            pacPort = SocketUtil.GetFreePort(localPort);
            pac_url = $"http://127.0.0.1:{pacPort}/pac.js";
            directory = FileUtil.GetPath(folder);
            Directory.CreateDirectory(directory);
            UpdatePACFile();
            WatchFile(gfwlist_file);
            WatchFile(user_rule_file);
        }


        public void Start()
        {
            try
            {
                script = File.ReadAllText(Path.Combine(directory,pac_file), Encoding.UTF8);
            }
            catch
            {
                MsgBox.Error("Load PAC file fail");
                return;
            }
            if (listener == null)
            {
                listener = new HttpListener() { Prefixes = { $"http://127.0.0.1:{pacPort}/" } };
                listener.Start();
                listener.BeginGetContext(new AsyncCallback(ListenerCallback), listener);
            }
        }

        private void ListenerCallback(IAsyncResult ar)
        {
            try
            {
                var http = (HttpListener)ar.AsyncState;
                var context = http.EndGetContext(ar);
                var request = context.Request;
                var response = context.Response;
                response.ContentLength64 = Encoding.UTF8.GetByteCount(script);
                response.ContentType = "application/x-ns-proxy-autoconfig; charset=UTF-8";
                var writer = new StreamWriter(response.OutputStream);
                writer.AutoFlush = true;
                writer.Write(script);
                writer.Close();
                response.Close();
                listener.BeginGetContext(ListenerCallback, listener);
            }
            catch (HttpListenerException ex)
            {
                if (ex.NativeErrorCode == 995)
                {
                    //The I/ O operation has been aborted due to thread exit or application request
                    return;
                }
            }
            catch (ObjectDisposedException)
            {
                //Cannot access the released object;
                return;
            }
        }

        public void Refresh(Setting setting)
        {
            if (localPort == setting.LocalPort && remotePort==setting.RemotePort)
                return;
            localPort = setting.LocalPort;
            remotePort = setting.RemotePort;
            UpdatePACFile();
        }

        private void UpdatePACFile()
        {

            string gfwlist;
            try
            { 
                gfwlist = File.ReadAllText(Path.Combine(directory,gfwlist_file), Encoding.UTF8); 
            }
            catch 
            {
                gfwlist = Resources.gfwlist;
                File.WriteAllText(Path.Combine(directory, gfwlist_file), gfwlist, Encoding.UTF8);
            }
            string userRule;
            string userPath = Path.Combine(directory, user_rule_file);
            if (!File.Exists(userPath)){
                userRule = Resources.user_rule;
                File.WriteAllText(Path.Combine(directory, user_rule_file), userRule, Encoding.UTF8);
            }
            else
            {
                //以只读共享方式打开文件
                using (FileStream fs = new FileStream(userPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    int length = (int)fs.Length;
                    byte[] content = new byte[length];
                    fs.Read(content, 0, content.Length);
                    userRule = Encoding.UTF8.GetString(content);
                }
            }
            string abpContent;
            try
            {
                abpContent = File.ReadAllText(Path.Combine(directory, abp_js_file), Encoding.UTF8);
            }
            catch
            {
                FileUtil.UnGzip(Path.Combine(directory, abp_js_file), Resources.abp_js);
                abpContent = File.ReadAllText(Path.Combine(directory, abp_js_file), Encoding.UTF8);
            }
            try
            {
                List<string> lines = FileUtil.ParseResult(gfwlist);
                using (var sr = new StringReader(userRule))
                {
                    foreach (var rule in sr.NonWhiteSpaceLines())
                    {
                        if (rule.BeginWithAny(FileUtil.IgnoredLineBegins))
                            continue;
                        lines.Add(rule);
                    }
                }
                abpContent = abpContent.Replace("__PROXY__", $"PROXY 127.0.0.1:{remotePort}");
                abpContent = abpContent.Replace("__RULES__", JsonUtil.SerializeObject(lines));
                if (File.Exists(Path.Combine(directory,pac_file)))
                {
                    string original = File.ReadAllText(Path.Combine(directory, pac_file), Encoding.UTF8);
                    if (original == abpContent)
                    {
                        return;
                    }
                }
                File.WriteAllText(Path.Combine(directory, pac_file), abpContent, Encoding.UTF8);
                BalloonTip tip = new BalloonTip()
                {
                    IconIndex = 1,
                    Message = "Update PAC file success",
                    timeout = 3000
                };
                ShowBalloonTip?.Invoke(this, tip);
            }
            catch(Exception)
            {
                BalloonTip tip = new BalloonTip()
                {
                    IconIndex = 1,
                    Message = "Update PAC file fail",
                    timeout = 3000
                };
                ShowBalloonTip?.Invoke(this, tip);
            }
        }
        public string GetGFWListFilePath()
        {
            string path = Path.Combine(directory, gfwlist_file);
            if (!File.Exists(path))
                File.WriteAllText(path, Resources.gfwlist);
            return path;
        }

        public string GetUserRuleFilePath()
        {
            string path = Path.Combine(directory, user_rule_file);
            if(!File.Exists(path))
                File.WriteAllText(path, Resources.user_rule);
            return path;
        }

        private void WatchFile(string filename)
        {
            FileSystemWatcher watcher = new FileSystemWatcher(directory);
            watcher.Filter = filename;
            watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            watcher.Changed += File_Changed; ;
            watcher.EnableRaisingEvents = true;
        }

        // FileSystemWatcher Changed event is raised twice
        // http://stackoverflow.com/questions/1764809/filesystemwatcher-changed-event-is-raised-twice
        private static Hashtable fileChangedTime = new Hashtable();
        private void File_Changed(object sender, FileSystemEventArgs e)
        {
            string path = e.FullPath.ToString();
            string currentLastWriteTime = File.GetLastWriteTime(e.FullPath).ToString(CultureInfo.InvariantCulture);

            // if there is no path info stored yet or stored path has different time of write then the one now is inspected
            if (!fileChangedTime.ContainsKey(path) || fileChangedTime[path].ToString() != currentLastWriteTime)
            {
                Thread thread = new Thread(new ThreadStart(() =>
                {
                    UpdatePACFile();
                    FileChanged?.Invoke(this, EventArgs.Empty);
                    fileChangedTime[path] = currentLastWriteTime;
                }));
                thread.Start();
            }
        }

        public void Stop()
        {
            if(listener != null)
            {
                listener.Abort();
                listener = null;
            }
        }

    }
}
