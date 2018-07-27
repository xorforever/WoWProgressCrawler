using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using WoWProgressCrawler.Model;

namespace WoWProgressCrawler.Core.APIRequestHandlers
{
    class FetchPage : IAPIRequest
    {
        public string Run(string[] Args)
        {
            List<Character> _rs;
            var _rq = new WoWProgressRequest();
            var _page = Convert.ToInt32(Args[2]);
            string cached_name = string.Format("lfg_page_{0}", _page);

            if (Cache.Cache.ObjectExist(cached_name))
            {
                _rs = (List<Character>)Cache.Cache.GetObject(cached_name);
            }
            else
            {
                _rs = _rq.LFGFetchPage(_page);
                Cache.Cache.PutObject(cached_name, _rs);
            }
            return new JavaScriptSerializer().Serialize(_rs);
        }
    }
}
