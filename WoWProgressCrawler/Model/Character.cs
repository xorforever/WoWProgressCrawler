using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoWProgressCrawler.Model
{
    public struct Character
    {
        public string Name;
        public string Guild;
        public string Raid; //wtf?
        public string Server;
        public string Itmlvl;
        public string WPRef;
        public string Timestamp;
        public int Timestamp_num;

        public override string ToString()
        {
            return string.Format("{0}-{1}", Name, Guild);
        }
    }
}
