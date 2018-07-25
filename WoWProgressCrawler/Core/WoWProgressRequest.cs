using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WoWProgressCrawler.Core
{
    class WoWProgressRequest
    {
        private static String TableRegEx = "<table class=\"rating.*>(.*)</table>";
        private static String ShittySelecRegEx = "<select.*>(.*)</select>";

        public static String FilterData(String Data)
        {
            Data = Regex.Match(Data, TableRegEx).Value;
            Data = Regex.Replace(Data, ShittySelecRegEx, String.Empty);
            Data = Data.Replace("&nbsp;", String.Empty)
                //.Replace(String., String.Empty)
                //.Replace("</value>", "</option>")
                .Replace("<th></th></tr>", "<th></th>");
            return Data;
        }
    }
}
