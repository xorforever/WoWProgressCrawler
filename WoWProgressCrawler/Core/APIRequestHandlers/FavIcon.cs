using System.Configuration;
using System.IO;
using NHttp;

namespace WoWProgressCrawler.Core.APIRequestHandlers
{
    class FavIcon : IAPIRequest
    {
        private static string cached_name = "favicon";
        private static int LifeTime = 24*60*60;

        public void Run(string[] Args, HttpRequestEventArgs e)
        {
            if (Cache.Cache.ObjectExist(cached_name))
            {
                e.Response.ContentType = "image/x-icon";
                byte[] data = (byte[])Cache.Cache.GetObject(cached_name);
                e.Response.OutputStream.Write(data, 0, data.Length);
            }
            else
            {
                using (var file = File.Open(ConfigurationManager.AppSettings.Get("EmbeddedHTTP_Favicon"), FileMode.Open))
                {
                    using (var sr = new BinaryReader(file))
                    {
                        byte[] data = sr.ReadBytes((int)file.Length);
                        Cache.Cache.PutObject(cached_name, data, LifeTime);

                        e.Response.ContentType = "image/x-icon";
                        e.Response.OutputStream.Write(data, 0, data.Length);
                        sr.Close();
                    }
                    file.Close();
                }
            }

        }
    }
}
