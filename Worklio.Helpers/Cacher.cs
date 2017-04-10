using System;
using System.Configuration;
using System.Linq;
using System.Runtime.Caching;

namespace Worklio.Helpers
{
    public interface ICasher
    {
        object Add(string key, object value);
        void Remove(string key);
        object Get(string key);
        void Clear();

    }
    public class Cacher : ICasher
    {
        private MemoryCache _cache { get; } = MemoryCache.Default;
        private static int _cachetimer = int.Parse(ConfigurationManager.AppSettings["cacheTimer"]);
        private CacheItemPolicy _defaultPolicy { get; } = new CacheItemPolicy();

        public void Clear()
        {
            var keys = _cache.Select(i => i.Key).ToList();
            keys.ForEach(k => _cache.Remove(k));
        }

        public object Get(string key)
        {
            return _cache.Get(key);
        }

        public void Remove(string key)
        {
            if (_cache.Contains(key)) _cache.Remove(key);
        }

        public object Add(string key, object value)
        {
            return _cache.Add(key, value, DateTimeOffset.Now.AddHours(_cachetimer));
        }
    }
}
