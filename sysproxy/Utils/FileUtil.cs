using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Windows.Forms;

namespace sysproxy.Utils
{
    public static class FileUtil
    {

        public static string GetPath(string filename)
        {
            return Path.Combine(Application.StartupPath, filename);
        }


        public static void ByteArrayToFile(string fileName, byte[] content)
        {
            try
            {
                using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                    fs.Write(content, 0, content.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in process: {0}",
                                  ex.ToString());
            }
        }

        public static void UnGzip(string path, byte[] content)
        {
            // Because the uncompressed size of the file is unknown,
            // we are using an arbitrary buffer size.
            if (File.Exists(path))
                return;
            byte[] buffer = new byte[4096];
            int n;
            using (var fs = File.Create(path))
            using (var input = new GZipStream(new MemoryStream(content),
                    CompressionMode.Decompress, false))
            {
                while ((n = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    fs.Write(buffer, 0, n);
                }
            }
        }

        /// <summary>
        /// Decrypt gfwlist.txt
        /// </summary>
        public static readonly IEnumerable<char> IgnoredLineBegins = new[] { '!', '[' };
        public static List<string> ParseResult(string response)
        {
            byte[] bytes = Convert.FromBase64String(response);
            string content = Encoding.ASCII.GetString(bytes);
            List<string> valid_lines = new List<string>();
            using (var sr = new StringReader(content))
            {
                foreach (var line in sr.NonWhiteSpaceLines())
                {
                    if (line.BeginWithAny(IgnoredLineBegins))
                        continue;
                    valid_lines.Add(line);
                }
            }
            return valid_lines;
        }

     
    }
}
