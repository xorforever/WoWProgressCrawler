using System.Runtime.Caching;

namespace WoWProgressCrawler.Core.Cache
{
    public static class Cache
    {
        private static MemoryCache _c;

        public static void InitCache()
        {
            _c = MemoryCache.Default;
        }

        public static void PutObject(string s, object o)
        {
            _c.Set(s, o, System.DateTimeOffset.Now.AddMinutes(10.0));
        }

        public static bool ObjectExist(string key)
        {
            return _c.Contains(key);
        }
        
        public static object GetObject(string key)
        {
            return _c.Get(key);
        }
    }
}
