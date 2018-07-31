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
                var _rs = FetchAll.Data;

                if (Cache.Cache.ObjectExist(cached_name))
                {
                    _rs = (LFGFetchAllData)Cache.Cache.GetObject(cached_name);
                }
                else
                {
                    _rs = FetchAll.Data;

                    ThreadPool.QueueUserWorkItem(o => UpdateJob());
                }

                writer.Write(new JavaScriptSerializer().Serialize(_rs));
            }
        }

        private static void UpdateJob()
        {
            if (FetchAll.Data.Updating == ListState.UPDATING) return;
            Console.WriteLine("Update call job start");
            FetchAll.Data.Updating = ListState.UPDATING;
            FetchAll.Data.LastUpdated = DateTime.Now;
            FetchAll.Data.Characters = LFGFetchAll();
            FetchAll.Data.Updating = ListState.UPDATED;
            Cache.Cache.PutObject(cached_name, FetchAll.Data, LifeTime);
            Console.WriteLine("Update call job completed");
        }

        private static List<Character> LFGFetchAll()
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

                Console.WriteLine();
                while (HasData)
                {
                    Console.Write(Index + " ");
                    Raw = w.UploadString(String.Format(NextPage, Index), RQ);
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
