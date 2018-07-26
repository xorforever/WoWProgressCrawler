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

        }
    }
}
