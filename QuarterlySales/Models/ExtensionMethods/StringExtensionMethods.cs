using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuarterlySales.Models
{
    public static class StringExtensions
    {
        public static string Slug(this string s)
        {
            var sb = new StringBuilder();

            foreach (char c in s)
            {
                if (!(char.IsPunctuation(c) || c == '-'))
                {
                    sb.Append(c);
                }
            }

            return sb.ToString().Replace(' ', '-').ToLower();
        }

        public static bool EqualsNoCase(this string s, string toCompare) =>
            s?.ToLower() == toCompare?.ToLower();

        public static int ToInt(this string s)
        {
            int.TryParse(s, out int id);
            return id;
        }
    }
}
