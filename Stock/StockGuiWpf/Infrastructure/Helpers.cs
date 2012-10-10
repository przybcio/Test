using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockGuiWpf.Infrastructure
{
    static public class MyExtension
    {
        public static string StockSubstring(this string str, string stockName, int additionalOffset, int characteCount)
        {

            var myIndex = str.IndexOf(stockName);

            var mystring = str.Substring(myIndex + additionalOffset, characteCount);

            var secondIndex = mystring.IndexOf("<");

            return mystring.Substring(0, secondIndex);
        }

    }
}
