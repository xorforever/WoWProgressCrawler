using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoWProgressCrawler.Core.APIRequestHandlers
{
    interface IAPIRequest
    {
        string Run(string In);
    }
}
