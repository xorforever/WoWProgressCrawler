using System;
using WoWProgressCrawler.Core;
using WoWProgressCrawler.Core.APIRequestHandlers;
using WoWProgressCrawler.Model;

namespace WoWProgressCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            //WoWProgressRequest r = new WoWProgressRequest();
            //var chr = r.LFGFetchPage(90);//
            //var chr = r.LFGFetchAll();
            //Debug.WriteLine(chr.Count);
            Core.Cache.Cache.InitCache();
            //FetchAll.Data.Updating = ListState.UPDATED;
            var srv = new EmbeddedHTTP();
            srv.Start();
            Console.ReadLine();
            srv.Dispose();
        }
    }
}
