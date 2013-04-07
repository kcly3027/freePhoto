using System;
using System.Web;
using System.Web.Caching;

namespace freePhoto.Tools
{
    /// <summary>
    ///  缓存助手
    /// </summary>
    public class cache
    {
        /// <summary>
        ///  建立缓存（永不过期）
        /// </summary>
        public static void AddCache( string key, object obj )
        {
            Cache cache = HttpRuntime.Cache;
            cache.Insert( key, obj );
        }
        /// <summary>
        ///  建立缓存（依赖文件）
        /// </summary>
        /// <param name="dependencies">依赖的文件路径</param>
        public static void AddCache( string key, object obj, string dependencies )
        {
            Cache cache = HttpRuntime.Cache;
            cache.Insert( key, obj, new CacheDependency( dependencies ) );
        }
        /// <summary>
        ///  建立缓存（相对过期）
        /// </summary>
        /// <param name="seconds">过期秒数</param>
        public static void AddCache( string key, object obj, int seconds )
        {
            Cache cache = HttpRuntime.Cache;
            cache.Insert( key, obj, null, DateTime.Now.AddSeconds( seconds ), TimeSpan.Zero );
        }

        /// <summary>
        ///  获取已缓存的项
        /// </summary>
        public static object GetCache( string key )
        {
            Cache cache = HttpRuntime.Cache;
            return cache[key];
        }
        /// <summary>
        ///  移除已缓存的项，并返回移除的项
        /// </summary>
        public static object RemoveCache( string key )
        {
            Cache cache = HttpRuntime.Cache;
            return cache.Remove( key );
        }
    }//class end
}
