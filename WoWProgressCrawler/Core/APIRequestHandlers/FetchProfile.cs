using System;
using NHttp;
using System.Net;
using System.Configuration;
using System.IO;
using System.Web.Script.Serialization;

namespace WoWProgressCrawler.Core.APIRequestHandlers
{
    class FetchProfile : IAPIRequest
    {
        private static string cached_name = "profile_{0}";
        private static int LifeTime = 10;

        private static String WPRoot = ConfigurationManager.AppSettings.Get("WPRoot");

        public void Run(string[] Args, HttpRequestEventArgs e)
        {
            using (var writer = new StreamWriter(e.Response.OutputStream))
            {
                string profile = Args[2];
                string cached_name = string.Format(FetchProfile.cached_name, profile);

                if (Cache.Cache.ObjectExist(cached_name))
                {
                    var _rs = (UserProfile)Cache.Cache.GetObject(cached_name);
                    writer.Write(new JavaScriptSerializer().Serialize(_rs));
                }
                else
                {
                    var _rs = GetProfile(profile);
                    Cache.Cache.PutObject(cached_name, _rs, LifeTime);
                    writer.Write(new JavaScriptSerializer().Serialize(_rs));
                }
            }
        }

        private UserProfile GetProfile(string Profile)
        {
            using (var w = new WebClient())
            {
                string Raw;
                var _url = new Uri(new Uri(WPRoot), Profile);
                w.Encoding = System.Text.Encoding.UTF8;

                Raw = w.DownloadString(_url);
                var _profile = UserProfileLoader.ReadUserProfile(Raw);
                _profile.Ref = Profile;
                return _profile;
            }
        }
    }
}
