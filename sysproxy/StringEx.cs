using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace sysproxy
{
    static partial class StringEx
    {
        public static IEnumerable<string> NonWhiteSpaceLines(this TextReader reader)
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (line.IsWhiteSpace()) continue;
                yield return line;
            }
        }

        public static bool IsWhiteSpace(this string value)
        {
            foreach (var c in value)
            {
                if (char.IsWhiteSpace(c)) continue;

                return false;
            }
            return true;
        }

        public static bool IsNullOrEmpty(this string value)
       => string.IsNullOrEmpty(value);
        public static bool BeginWithAny(this string s, IEnumerable<char> chars)
        {
            if (s.IsNullOrEmpty()) return false;
            return chars.Contains(s[0]);
        }
    }
}
