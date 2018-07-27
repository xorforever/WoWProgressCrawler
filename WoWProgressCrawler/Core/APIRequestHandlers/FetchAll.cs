using System;
using System.Collections.Generic;
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
        

        public string Run(string[] Args)
        {
            var _rs = FetchAll.Data;

            if (Cache.Cache.ObjectExist(cached_name))
            {
                _rs = (LFGFetchAllData)Cache.Cache.GetObject(cached_name);
            }
            else
            {
                _rs = FetchAll.Data;
                Update();
            }

            return new JavaScriptSerializer().Serialize(_rs);
        }

        private static void Update()
        {
            Console.WriteLine("Update call");
            ThreadPool.QueueUserWorkItem(o => UpdateJob());
        }

        private static void UpdateJob()
        {
            if (FetchAll.Data.Updating == ListState.UPDATING) return;
            Console.WriteLine("Update call job start");
            var _rq = new WoWProgressRequest();
            FetchAll.Data.Updating = ListState.UPDATING;
            FetchAll.Data.LastUpdated = DateTime.Now;
            var _rs = _rq.LFGFetchAll();
            FetchAll.Data.Characters = _rs;
            FetchAll.Data.Updating = ListState.UPDATED;
            Cache.Cache.PutObject(cached_name, FetchAll.Data, LifeTime);
            Console.WriteLine("Update call job completed");
        }
    }
}
