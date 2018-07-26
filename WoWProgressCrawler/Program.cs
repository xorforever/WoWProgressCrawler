using System;
using System.Diagnostics;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml;

namespace WoWProgressCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            WebClient w = new WebClient();
            w.Headers.Add("content-type: application/x-www-form-urlencoded; charset=UTF-8");
            w.Headers.Add("user-agent: TheFatCatCrawler/0.1 (mono/.net)");
            var raw = w.DownloadString("https://www.wowprogress.com/gearscore/?lfg=1&sortby=ts");
                         
            raw = Core.WoWProgressRequest.FilterData(raw);
            var a = Core.TableLoader.ReadTableData(raw);
            Debug.WriteLine(a.Count);
            Console.ReadLine();
        }
    }
}
