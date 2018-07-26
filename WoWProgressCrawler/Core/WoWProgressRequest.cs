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
        private static String ShittySelectRegEx = "<select.*>(.*)</select>";

        private static String IndexPage = "https://www.wowprogress.com/gearscore/?lfg=1&sortby=ts"; 
        private static String NextPage = "https://www.wowprogress.com/gearscore/char_rating/next/{0}/lfg.1/sortby.ts"; //0-+#NaN

        private static String h_UserAgent = "user-agent: TheFatCatCrawler/0.1 (mono/.net)";
        private static String h_ContentType = "content-type: application/x-www-form-urlencoded; charset=UTF-8";
        private static String RQ = "ajax=1";

        public static String FilterData(String Data)
        {
            Data = Regex.Match(Data, TableRegEx).Value;
            Data = Regex.Replace(Data, ShittySelectRegEx, String.Empty);
            Data = Data.Replace("&nbsp;", String.Empty);
                //.Replace(String., String.Empty)
                //.Replace("</value>", "</option>")
                //.Replace("<th></th></tr>", "<th></th>");
            return Data;
        }


    }
}
