using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoWProgressCrawler.Core.APIRequestHandlers
{
    class APIBase
    {
        private Dictionary<string, IAPIRequest> _handlers = new Dictionary<string, IAPIRequest>();

        public APIBase()
        {
            _handlers.Add("FetchAll", new FetchAll());
            _handlers.Add("FetchPage", new FetchPage());
        }

        public void RunMethod(string Request, ref string Response)
        {
            string[] args = Request.Split(new char[] { '/' });
            if (!_handlers.ContainsKey(args[1]))
            {
                Response = "Unknown Method";
                return;
            }
            Response = _handlers[args[1]].Run(args);
        }
    }
}
