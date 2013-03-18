using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.Caching;
namespace freePhoto.Tools
{
    /// <summary>
    /// 缓存 
    /// </summary>
    public class Cache
    {
        private object cache = null;
        private string cacheName = null;
        private string path = null;
        /// <summary>
        /// 取
        /// </summary>
        /// <param name="str"></param>
        private void GetCacheValue(string str)
        {
            this.path = Fetch.ServerDomain();
            this.cacheName = str + this.path;
            try
            {
                this.cache = HttpRuntime.Cache.Get(this.cacheName);
            }
            catch (Exception)
            {
                this.cache = null;
            }
        }
        /// <summary>
        /// 对缓存赋值/方法
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="varExpireTime"></param>
        private void AddCacheValue(object cache, int varExpireTime)
        {
            if (cache != null)
            {
                this.clean();
                DateTime absoluteExpiration = DateTime.Now.AddSeconds((double)varExpireTime);
                HttpRuntime.Cache.Insert(this.cacheName, cache, null, absoluteExpiration, TimeSpan.Zero, CacheItemPriority.Normal, null);
            }
        }
        /// <summary>
        /// 释放缓存/方法
        /// </summary>
        private void clean()
        {
            HttpRuntime.Cache.Remove(this.cacheName);
        }
        /// <summary>
        /// 删除所有缓存
        /// </summary>
        public static void RemoveAllCache()
        {
            System.Web.Caching.Cache cache = HttpRuntime.Cache;
            IDictionaryEnumerator enumerator = cache.GetEnumerator();
            ArrayList arrayList = new ArrayList();
            while (enumerator.MoveNext())
            {
                arrayList.Add(enumerator.Key);
            }
            foreach (string key in arrayList)
            {
                cache.Remove(key);
            }
        }
        /// <summary>
        /// 请除定义的DATATABLE变量
        /// </summary>
        public void Dispose()
        {
            try
            {
                this.cache = null;
            }
            catch (Exception)
            {
            }
        }
        /// <summary>
        /// 传入一个全局变量的值，把其在全局变量中删除
        /// </summary>
        /// <param name="str">要删除的全局变量的名称</param>
        public void clear(string str)
        {
            this.path = Fetch.ServerDomain();
            this.cacheName = str + this.path;
            this.clean();
        }
    }
}