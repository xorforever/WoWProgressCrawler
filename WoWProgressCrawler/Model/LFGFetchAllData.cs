using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoWProgressCrawler.Model
{
    public struct LFGFetchAllData
    {
        public ListState Updating;
        public DateTime LastUpdated;
        public List<Character> Characters;
    }

    public enum ListState
    {
        UPDATED,
        UPDATING
    }
}
