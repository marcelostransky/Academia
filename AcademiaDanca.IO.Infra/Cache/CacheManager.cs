using FluentValidator;
using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Text;


namespace AcademiaDanca.IO.Infra.Cache
{
    public class CacheManager : Notifiable, IDisposable
    {

        public void AdicionarAoCache<T>(T data, string chave, double minutos)
        {
            if (data != null)
            {
                var cachePolicy = new CacheItemPolicy();
                cachePolicy.AbsoluteExpiration = DateTime.Now.AddMinutes(minutos);
                MemoryCache.Default.Add(new CacheItem(chave, data), cachePolicy);
            }
        }

        public T ObterDoCache<T>(string chave)
        {
            CacheItem item = MemoryCache.Default.GetCacheItem(chave) as CacheItem;
            if (item != null)
            {
                return item.Value is T ? (T)item.Value : default(T);
            }
            else
            {
                return default(T);
            }
        }

        public void Dispose()
        {
            //TODO: Implement dispose for cache manager
        }
    }
}
