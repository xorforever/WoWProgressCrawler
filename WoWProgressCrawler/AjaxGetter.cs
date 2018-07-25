using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml;

namespace WoWProgressCrawler
{
    class AjaxGetter
    {
        public AjaxGetter(int num)
        {
            WebClient w = new WebClient();
            w.Headers.Add("content-type: application/x-www-form-urlencoded; charset=UTF-8");
            w.Headers.Add("user-agent: TheFatCatCrawler/0.1 (mono/.net)");
            var raw = w.UploadString("https://www.wowprogress.com/gearscore/char_rating/next/"+num+"/lfg.1/sortby.ts", "ajax=1");

            //Удаляем выпердыши кривоногого верстальщика. 
            raw = Core.WoWProgressRequest.FilterData(raw);


            XmlDocument data = new XmlDocument();
            data.LoadXml(raw);

            foreach (XmlElement node in data.SelectNodes("descendant::tr"))
            {
                //Console.WriteLine(node.InnerXml);
                foreach (XmlElement node_node in node.SelectNodes("descendant::td"))
                {
                    Console.Write(node_node.InnerText + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
