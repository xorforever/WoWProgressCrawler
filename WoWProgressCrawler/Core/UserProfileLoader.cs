using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WoWProgressCrawler.Core
{
    class UserProfileLoader
    {
        private static readonly string StrSplitRegEx = "[,]{1}[\\s]?";
        private static readonly string TagStripRegEx = "<(.*?)>";

        private static readonly string ProfileRegEx = "<div class=\"registeredTo.*>(.*?)<div class=\"clear.*></div>";
        private static readonly string BattletagRegEx = "<span class=\"profileBattletag\">(.*?)</spanc>";
        private static readonly string LanguagesRegEx = "Languages: <strong>(.*?)</strong>";
        private static readonly string LFGRegEx = "Looking for guild: <.*?>(.*?)</.*?>";
        private static readonly string RaidingStyleRegEx = "<div class=\"raiding_style.*?><strong>(.*?)<.*strong>";
        private static readonly string RaidsPerWeekRegEx = "Raids per week: <strong>(.*?) - (.*?)</strong>";
        private static readonly string SpecsPlayingRegEx = "Specs playing: (.*?)</div>";
        private static readonly string CharCommentaryRegEx = "<div class=\"charCommentary\">(.*?)</div>";

        #region ToDo
        /*
        private static readonly string MPlusBaseRegEx = "Plus Dungeons: <ul>(.*?)</ul>";
        private static readonly string MPlusLFGroupRegEx = ".*>Looking for group to push high Mythic Plus keys<.*";
        private static readonly string MPlusLFRoleRegEx = ".*LF <span class='mplus_role'>.*";
        private static readonly string MPlusLFRoleListRegEx = "<span class='mplus_role'>(.*?)</span>";

        private static readonly string MPlusLFChestRegEx = ".*>LF to finish weekly chest<.*";
        */
        #endregion

        private static string FilterHTMLData(string Data)
        {
            Data = Data.Replace("&nbsp;", string.Empty);
            Data = Regex.Match(Data, ProfileRegEx).Value;
            return Data;
        }

        public static UserProfile ReadUserProfile(string Data)
        {
            Data = UserProfileLoader.FilterHTMLData(Data);
            string btag = Regex.Matches(Data, BattletagRegEx)[0].Groups[1].Value;
            string languages = Regex.Matches(Data, LanguagesRegEx)[0].Groups[1].Value;
            string[] lang_arr = Regex.Split(languages, StrSplitRegEx);
            string lfg = Regex.Matches(Data, LFGRegEx)[0].Groups[1].Value;
            string[] lfg_arr = Regex.Split(lfg, StrSplitRegEx); //1 - no, 2-Yes, Transfer<y,n>
            string lfg_state = lfg_arr[0];
            string transfer = "";

            if (lfg_arr.Length > 1)
            {
                transfer = lfg_arr[1];
            }
            string rs = Regex.Matches(Data, RaidingStyleRegEx)[0].Groups[1].Value;
            string[] rs_arr = Regex.Split(rs, StrSplitRegEx);

            string rpw_min = Regex.Matches(Data, RaidsPerWeekRegEx)[0].Groups[1].Value;
            string rpw_max = Regex.Matches(Data, RaidsPerWeekRegEx)[0].Groups[2].Value;

            string spec = Regex.Replace(Regex.Matches(Data, SpecsPlayingRegEx)[0].Groups[1].Value, TagStripRegEx, string.Empty);
            string[] spec_arr = Regex.Split(spec, StrSplitRegEx);

            string comment = Regex.Matches(Data, CharCommentaryRegEx)[0].Groups[1].Value;

            #region ToDo
            /*
            var mpb = Regex.Matches(Data, MPlusBaseRegEx)[0].Groups[1].Value;
            var MPLFG = Regex.Match(Data, MPlusLFGroupRegEx).Success;
            Console.WriteLine("Looking for m+ group "+MPLFG);
            var MPLFR = Regex.Match(Data, MPlusLFRoleRegEx).Success;
            Console.WriteLine("Looking for m+ group members "+MPLFR);

            if (MPLFG)
            {
                var mroles = Regex.Matches(Data, MPlusLFRoleListRegEx)[0].Groups[1].Value;
                Console.WriteLine(mroles);
                var mrolesarr = Regex.Split(mroles, MPlusLFRoleListSplitRegEx);
                Console.WriteLine(mrolesarr);
            }

            var MPLFC = Regex.Match(Data, MPlusLFChestRegEx).Success;
            Console.WriteLine("Looking for m+ weekly chest "+MPLFC);
            */
            #endregion

            return new UserProfile()
            {
                BattleTag = btag,
                Languages = lang_arr,
                LookingForGuild = lfg_state,
                ReadyToTransfer = transfer,
                RaidingStyle = rs_arr,
                SpecsPlaying = spec_arr,
                RaidsPerWeekMin = rpw_min,
                RaidsPerWeekMax = rpw_max,
                Commentary = comment
            };
        }
    }
}
