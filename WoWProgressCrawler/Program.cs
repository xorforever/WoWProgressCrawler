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
            Core.Cache.Cache.InitCache();

            using (var srv = new EmbeddedHTTP())
            {
                srv.Start();
                Console.ReadLine();
            }
        }
    }
}
