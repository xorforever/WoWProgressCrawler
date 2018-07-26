using System;

namespace WoWProgressCrawler.Core.APIRequestHandlers
{
    class FetchAll : IAPIRequest
    {
        public string Run(string In)
        {
            var _rq = new WoWProgressRequest();
            _rq.LFGFetchAll();
        }
    }
}
