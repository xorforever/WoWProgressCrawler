using System;
using System.Diagnostics;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml;
using WoWProgressCrawler.Core;

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
            var srv = new EmbeddedHTTP();
            srv.Start();
            Console.ReadLine();
            srv.Dispose();
        }
    }
}
