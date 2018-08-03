using NHttp;
using System;
using System.Collections.Generic;

namespace WoWProgressCrawler.Core.APIRequestHandlers
{
    class APIBase
    {
        private Dictionary<string, IAPIRequest> _handlers = new Dictionary<string, IAPIRequest>();

        public APIBase()
        {
            _handlers.Add("FetchAll", new FetchAll());
            _handlers.Add("FetchPage", new FetchPage());
            _handlers.Add("FetchProfile", new FetchProfile());
            _handlers.Add("favicon.ico", new FavIcon());
        }

        public void RunMethod(HttpRequestEventArgs e)
        {
            string[] args = e.Request.RawUrl.Split(new char[] { '/' }, 3);

            if (!_handlers.ContainsKey(args[1]))
            {
                Console.WriteLine("Unknown method \"{0}\"", args[1]);
                e.Response.Status = "Unknown method";
                e.Response.StatusCode = 404;
                return;
            }
            Console.WriteLine("Call \"{0}\"", args[1]);
            _handlers[args[1]].Run(args, e);
        }
    }
}
