using System;
using System.Web.Script.Serialization;

namespace WoWProgressCrawler.Core.APIRequestHandlers
{
    class FetchPage : IAPIRequest
    {
        public string Run(string[] Args)
        {
            var _rq = new WoWProgressRequest();
            var _rs = _rq.LFGFetchPage(Convert.ToInt32(Args[2]));
            var json = new JavaScriptSerializer().Serialize(_rs);
            return json;
        }
    }
}
