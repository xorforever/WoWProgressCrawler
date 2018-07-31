using NHttp;

namespace WoWProgressCrawler.Core.APIRequestHandlers
{
    interface IAPIRequest
    {
        void Run(string[] Args, HttpRequestEventArgs e);
    }
}
