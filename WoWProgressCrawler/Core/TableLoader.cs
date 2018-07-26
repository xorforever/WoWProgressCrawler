using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using WoWProgressCrawler.Model;

namespace WoWProgressCrawler.Core
{
    public class TableLoader
    {
        private static string tr_pattern = "<tr>(.*?)</tr>";
        private static string td_pattern = "<td.*?>(.*?)</td>";
        private static string tag_pattern = "<(.*?)>";
        private static string chref_pattern = "href=\"(.*?)\"";
        private static string ts_pattern = "data-ts=\"(.*?)\"";

        public static List<Character> ReadTableData(string Data)
        {
            Character chr;

            List<Character> chars = new List<Character>();

            List<string> fields = new List<string>();

            MatchCollection rows = Regex.Matches(Data, tr_pattern);

            foreach (Match row in rows)
            {
                MatchCollection cols = Regex.Matches(row.Value, td_pattern);
                foreach (Match col in cols)
                {
                    //Name//Guild//Raid//Srv//ItmLvl//LastUpd
                    fields.Add(col.Value);
                }

                //Make character struct
                chr = new Character()
                {
                    Name = Regex.Replace(fields[0], tag_pattern, string.Empty),
                    Guild = Regex.Replace(fields[1], tag_pattern, string.Empty),
                    Raid = Regex.Replace(fields[2], tag_pattern, string.Empty),
                    Server = Regex.Replace(fields[3], tag_pattern, string.Empty),
                    Itmlvl = Regex.Replace(fields[4], tag_pattern, string.Empty),
                    WPRef = Regex.Match(fields[0], chref_pattern).Value.Split(new char[] { '=' })[1].Replace("\"", string.Empty),
                    Timestamp = Regex.Replace(fields[5], tag_pattern, string.Empty),
                    Timestamp_num = Convert.ToInt32(Regex.Match(fields[5], ts_pattern).Value.Split(new char[] { '=' })[1].Replace("\"", string.Empty))
                };
                chars.Add(chr);
                fields.Clear();
            }
            return chars;
        }
    }
}
