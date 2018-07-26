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
            WoWProgressRequest r = new WoWProgressRequest();
            var chr = r.LFGFetchPage(90);//r.LFGFetchAll();
            Debug.WriteLine(chr.Count);
            Console.ReadLine();
        }
    }
}
