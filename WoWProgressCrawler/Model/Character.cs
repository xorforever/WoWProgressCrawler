using System;

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
        public long Timestamp_num;

        public override string ToString()
        {
            return string.IsNullOrEmpty(Guild) ? Name : string.Format("{0}-{1}", Name, Guild);
        }
    }
}
