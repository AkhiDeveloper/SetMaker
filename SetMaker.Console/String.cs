using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}

