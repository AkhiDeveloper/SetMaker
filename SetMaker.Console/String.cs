using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SetMaker.Console
{
    public static class CustomString
    {
        public static string TrimSymbolStart(string text)
        {
            string result = string.Empty;
            text = text.Trim();
            int flag = 0;
            foreach (var c in text)
            {
                if (!Char.IsLetterOrDigit(c) && flag == 0)
                {
                    continue;
                }
                if (Char.IsLetterOrDigit((char)c))
                {
                    flag = 1;
                    result = result + c;
                    continue;
                }
                result = result + c;
            }
            return result;
        }

        public static bool TrySeperateStartNumberAndLetters(string text, out int? startnumber, out string residualtext)
        {
            startnumber = null;
            residualtext = string.Empty;
            if (text == null)
            {
                
                return false;
            }

            bool isstartwithnumber = false;
            string text1 = string.Empty;
            string text2 = string.Empty;
            int flag = 0;

            foreach (var c in text)
            {
                if (Char.IsDigit(c) && flag == 0)
                {
                    isstartwithnumber = true;
                    text1 = text1 + c;
                }
                if (!Char.IsDigit(c) || flag==1)
                {
                    flag = 1;
                    text2 = text2 + c;
                }
            }
            if (text1 == null) text1 = "0";
            if(!String.IsNullOrEmpty(text1))
            {
                startnumber = int.Parse(text1);
            }
            residualtext = text2;
            return isstartwithnumber;
        }

        public static string GetOpeningTag(this string text)
        {
            string pattern = @"<\w+\b";  // regular expression pattern to match opening tag
            Regex regex = new Regex(pattern);
            Match match = regex.Match(text);
            if (match.Success)
            {
                return match.Value;
            }
            else
            {
                return String.Empty;  // no opening tag found
            }
        }

        public static string GetTagText(this string htmlString, string tagName)
        {
            string pattern = $"<{tagName}\\b[^>]*>(.*?)</{tagName}>";  // regular expression pattern to match tag
            Regex regex = new Regex(pattern, RegexOptions.Singleline);
            Match match = regex.Match(htmlString);
            if (match.Success)
            {
                return match.Groups[1].Value;
            }
            else
            {
                return String.Empty;  // no matching tag found
            }
        }
    }
}

