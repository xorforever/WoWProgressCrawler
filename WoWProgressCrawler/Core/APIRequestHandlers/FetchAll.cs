using NHttp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Threading;
using System.Web.Script.Serialization;
using WoWProgressCrawler.Model;

namespace WoWProgressCrawler.Core.APIRequestHandlers
{
    class FetchAll : IAPIRequest
    {
        private static string cached_name = "lfg_page_all";
        private static int LifeTime = 300;
        public static LFGFetchAllData Data = new LFGFetchAllData();

        private static String IndexPage = ConfigurationManager.AppSettings.Get("IndexPage");
        private static String NextPage = ConfigurationManager.AppSettings.Get("NextPage"); //0-+#NaN
        private static String h_UserAgent = ConfigurationManager.AppSettings.Get("UserAgent");
        private static String h_ContentType = "content-type: application/x-www-form-urlencoded; charset=UTF-8";
        private static String RQ = "ajax=1";

        public void Run(string[] Args, HttpRequestEventArgs e)
        {
            using (var writer = new StreamWriter(e.Response.OutputStream))
            {
                if (Cache.Cache.ObjectExist(cached_name))
                {
                    var _rs = (LFGFetchAllData)Cache.Cache.GetObject(cached_name);
                    writer.Write(new JavaScriptSerializer().Serialize(_rs));
                }
                else
                {
                    writer.Write(new JavaScriptSerializer().Serialize(FetchAll.Data));
                    ThreadPool.QueueUserWorkItem(o => UpdateJob());
                }
            }
        }

        private static void UpdateJob()
        {
            if (FetchAll.Data.Updating == ListState.UPDATING) return;
            Console.WriteLine("Update job start");
            FetchAll.Data.Updating = ListState.UPDATING;
            FetchAll.Data.Characters = LFGFetchAll();
            FetchAll.Data.LastUpdated = Util.ToUnixTime(DateTime.UtcNow);
            FetchAll.Data.Updating = ListState.UPDATED;
            Cache.Cache.PutObject(cached_name, FetchAll.Data, LifeTime);
            Console.WriteLine("Update job completed");
        }

        private static List<Character> LFGFetchAll()
        {
            using (var w = new WebClient())
            {
                string Raw;
                List<Character> lfg = new List<Character>();
                w.Encoding = System.Text.Encoding.UTF8;

                //Download first page
                w.Headers.Add(h_UserAgent);
                Raw = w.DownloadString(IndexPage);
                lfg.AddRange(TableLoader.ReadTableData(Raw));

                //Download all remaining          
                bool HasData = true;
                int Index = 0;

                w.Headers.Add(h_ContentType);

                Console.WriteLine();
                while (HasData)
                {
                    Console.Write(Index + " ");
                    Raw = w.UploadString(string.Format(NextPage, Index), RQ);
                    if (Raw.Length == 0)
                    {
                        HasData = false;
                        break;
                    }
                    lfg.AddRange(TableLoader.ReadTableData(Raw));
                    Index++;
                }
                Console.WriteLine();
                return lfg;
            }
        }
    }
}
