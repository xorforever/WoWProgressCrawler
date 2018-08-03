using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Text.RegularExpressions;
using WoWProgressCrawler.Model;

namespace WoWProgressCrawler.Core
{
    class TableLoader
    {
        private static readonly string tr_pattern = "<tr>(.*?)</tr>";
        private static readonly string td_pattern = "<td.*?>(.*?)</td>";
        private static readonly string tag_pattern = "<(.*?)>";
        private static readonly string chref_pattern = "href=\"(.*?)\"";
        private static readonly string ts_pattern = "data-ts=\"(.*?)\"";

        private static readonly string TableRegEx = "<table class=\"rating.*>(.*)</table>";
        private static readonly string ShittySelectRegEx = "<select.*>(.*)</select>";

        private static string FilterHTMLeData(string Data)
        {
            Data = Data.Replace("&nbsp;", string.Empty);
            Data = Regex.Match(Data, TableRegEx).Value;
            //Data = Regex.Replace(Data, ShittySelectRegEx, String.Empty);
            //.Replace(String., String.Empty)
            //.Replace("</value>", "</option>")
            //.Replace("<th></th></tr>", "<th></th>");
            return Data;
        }

        public static List<Character> ReadTableData(string Data)
        {         
            List<Character> chars = new List<Character>();

            Data = FilterHTMLeData(Data);
            MatchCollection rows = Regex.Matches(Data, tr_pattern);

            foreach (Match row in rows)
            {
                MatchCollection cols = Regex.Matches(row.Value, td_pattern);

                chars.Add(new Character()
                {
                    Name = Regex.Replace(cols[0].Value, tag_pattern, string.Empty),
                    Guild = Regex.Replace(cols[1].Value, tag_pattern, string.Empty),
                    Raid = Regex.Replace(cols[2].Value, tag_pattern, string.Empty),
                    Server = Regex.Replace(cols[3].Value, tag_pattern, string.Empty),
                    Itmlvl = Regex.Replace(cols[4].Value, tag_pattern, string.Empty),
                    WPRef = Regex.Match(cols[0].Value, chref_pattern).Value.Split(new char[] { '=' })[1].Replace("\"", string.Empty),
                    Timestamp = Regex.Replace(cols[5].Value, tag_pattern, string.Empty),
                    Timestamp_num = Convert.ToInt64(Regex.Match(cols[5].Value, ts_pattern).Value.Split(new char[] { '=' })[1].Replace("\"", string.Empty))
                });
            }
            return chars;
        }
    }
}
