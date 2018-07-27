using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using WoWProgressCrawler.Model;

namespace WoWProgressCrawler.Core
{
    public class WoWProgressRequest
    {
        private static String IndexPage = ConfigurationManager.AppSettings.Get("IndexPage");
        private static String NextPage = ConfigurationManager.AppSettings.Get("NextPage"); //0-+#NaN

        private static String h_UserAgent = ConfigurationManager.AppSettings.Get("UserAgent"); 
        private static String h_ContentType = "content-type: application/x-www-form-urlencoded; charset=UTF-8";
        private static String RQ = "ajax=1";


        public List<Character> LFGFetchAll()
        {          
            using (var w = new WebClient())
            {
                List<Character> lfg = new List<Character>();
                w.Encoding = System.Text.Encoding.UTF8;
                String Raw;

                //Download first page
                w.Headers.Add(h_UserAgent);
                Raw = w.DownloadString(IndexPage);
                lfg.AddRange(TableLoader.ReadTableData(Raw));

                //Download all remaining          
                bool HasData = true;
                int Index = 0;

                w.Headers.Add(h_ContentType);
                while (HasData)
                {
                    //Console.Write(Index+" ");
                    Raw = w.UploadString(String.Format(NextPage, Index), RQ);
                    if (Raw.Length == 0)
                    {
                        HasData = false;
                        break;
                    }
                    lfg.AddRange(TableLoader.ReadTableData(Raw));
                    Index++;
                }
                return lfg;
            }
        }

        public List<Character> LFGFetchPage(int Index)
        {
            using (var w = new WebClient())
            {
                w.Encoding = System.Text.Encoding.UTF8;
                String Raw;

                w.Headers.Add(h_UserAgent);

                if (Index == -1)
                {
                    Raw = w.DownloadString(IndexPage);
                    return TableLoader.ReadTableData(Raw);
                }
                else
                {
                    Raw = w.UploadString(String.Format(NextPage, Index), RQ);
                    return TableLoader.ReadTableData(Raw);
                }
            }
        }
    }
}
