using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoWProgressCrawler.Core
{
    public struct UserProfile
    {
        public string Ref;
        public string BattleTag;
        public string[] Languages;
        public string LookingForGuild;
        public string ReadyToTransfer;
        public string[] RaidingStyle;
        public string[] SpecsPlaying;
        public string RaidsPerWeekMin;
        public string RaidsPerWeekMax;
        public string Commentary;

        public override string ToString()
        {
            return Ref.ToString();
        }
    }
}
