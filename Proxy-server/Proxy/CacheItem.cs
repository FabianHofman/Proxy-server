using Proxy_server.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proxy_server.Proxy
{
    class CacheItem
    {
        public HttpResponse Response { get; }
        public byte[] ResponseInBytes { get; }
        public DateTime CachedDate { get; }
        public int MaxAge;

        public CacheItem(HttpRequest httpRequest, HttpResponse httpResponse)
        {
            Response = httpResponse;
            ResponseInBytes = httpResponse.MessageInBytes;
            CachedDate = DateTime.UtcNow;
            MaxAge = 0;

            // If request has MaxAge header, set MaxAge to this value.
            if (httpRequest.HasHeader("Cache-Control") && httpRequest.GetHeader("Cache-Control").Value.Contains("max-age="))
            {
                int.TryParse(httpRequest.GetHeader("Cache-Control").Value.Split('=')[1], out MaxAge);
            }
        }

        public bool IsValid(int cacheTimeOutInSeconds)
        {
            if (MaxAge > 0)
            {
                return ((DateTime.UtcNow - CachedDate).TotalSeconds <= MaxAge && (DateTime.UtcNow - CachedDate).TotalSeconds <= cacheTimeOutInSeconds);
            }

            return ((DateTime.UtcNow - CachedDate).TotalSeconds <= cacheTimeOutInSeconds);
        }
    }
}
