using System;
using System.IO;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using WoWProgressCrawler.Model;
using NHttp;
using System.Net;
using System.Configuration;

namespace WoWProgressCrawler.Core.APIRequestHandlers
{
    class FetchPage : IAPIRequest
    {
        private static string cached_name = "lfg_page_{0}";
        private static int LifeTime = 300;

        private static String IndexPage = ConfigurationManager.AppSettings.Get("IndexPage");
        private static String NextPage = ConfigurationManager.AppSettings.Get("NextPage"); //0-+#NaN
        private static String h_UserAgent = ConfigurationManager.AppSettings.Get("UserAgent");
        private static String RQ = "ajax=1";

        public void Run(string[] Args, HttpRequestEventArgs e)
        {
            using (var writer = new StreamWriter(e.Response.OutputStream))
            {
                var _page = Convert.ToInt32(Args[2]);              
                
                string cached_name = string.Format(FetchPage.cached_name, _page);

                if (Cache.Cache.ObjectExist(cached_name))
                {
                    var _rs = (List<Character>)Cache.Cache.GetObject(cached_name);
                    writer.Write(new JavaScriptSerializer().Serialize(_rs));
                }
                else
                {
                    var _rs = LFGFetchPage(_page);
                    Cache.Cache.PutObject(cached_name, _rs, LifeTime);
                    writer.Write(new JavaScriptSerializer().Serialize(_rs));
                }

            }
        }

        private List<Character> LFGFetchPage(int Index)
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
