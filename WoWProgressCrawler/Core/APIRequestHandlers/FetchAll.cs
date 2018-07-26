﻿using System.Web.Script.Serialization;

namespace WoWProgressCrawler.Core.APIRequestHandlers
{
    class FetchAll : IAPIRequest
    {
        public string Run(string[] In)
        {
            var _rq = new WoWProgressRequest();
            var _rs = _rq.LFGFetchAll();
            var json = new JavaScriptSerializer().Serialize(_rs);
            return json;
        }
    }
}
