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

            //Удаляем выпердыши кривоногого верстальщика. 
            raw = Core.WoWProgressRequest.FilterData(raw);
            
            XmlDocument data = new XmlDocument();
            data.LoadXml(raw);

            foreach(XmlElement node in data.SelectNodes("descendant::tr"))
            {
                foreach (XmlElement node_node in node.SelectNodes("descendant::td"))
                {
                    Console.Write(node_node.InnerText+" ");
                }
                Console.WriteLine();
            }

            AjaxGetter g = new AjaxGetter(0);
            g = new AjaxGetter(1);
            
            Console.ReadLine();
        }
    }
}
