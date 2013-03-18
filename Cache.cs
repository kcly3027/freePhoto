using Base.Conn;
using Base.Fun;
using Base.IO;
using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.Caching;
namespace Base.Cache
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
        /// <summary>
        /// 设置字符串
        /// </summary>
        /// <param name="Cachename">要保存的全局变量的名称</param>
        /// <param name="strsql">要执行的SQL语句</param>
        /// <param name="ntime">保存全局变量的时间</param>
        /// <returns></returns>
        public string GetValue(string Cachename, string strsql, int ntime)
        {
            return this.GetValue(Cachename, strsql, ntime, "");
        }
        /// <summary>
        /// 设置字符串
        /// </summary>
        /// <param name="Cachename">要保存的全局变量的名称</param>
        /// <param name="strsql">要执行的SQL语句</param>
        /// <param name="ntime">保存全局变量的时间</param>
        /// <param name="sqllink">要链接的数据库</param>
        /// <returns></returns>
        public string GetValue(string Cachename, string strsql, int ntime, string sqllink)
        {
            object obj = "";
            if (ntime <= 0)
            {
                Database database = new Database();
                database.ModiySQL(sqllink);
                obj = database.GetValue(strsql);
                database.Dispose();
            }
            else
            {
                this.GetCacheValue(Cachename);
                if (this.cache != null)
                {
                    obj = this.cache.ToString().Trim();
                }
                if (obj.ToString().Length == 0)
                {
                    Database database = new Database();
                    database.ModiySQL(sqllink);
                    obj = database.GetValue(strsql);
                    database.Dispose();
                    this.AddCacheValue(obj, ntime);
                }
            }
            return obj.ToString();
        }
        /// <summary>
        /// 返回DataTable 根据文件  (容易被移除）
        /// </summary>
        /// <returns>文本</returns>
        public string GetTextFile(string Cachename, string filename)
        {
            string text = "";
            this.GetCacheValue(Cachename);
            if (this.cache != null)
            {
                text = this.cache.ToString();
            }
            if (text.Length == 0)
            {
                if (File.isexist(filename))
                {
                    text = File.ReadFile(filename);
                    this.clean();
                    HttpRuntime.Cache.Insert(this.cacheName, text, new CacheDependency(filename), System.Web.Caching.Cache.NoAbsoluteExpiration, System.Web.Caching.Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                }
            }
            return text;
        }
        /// <summary>
        /// 返回DataTable 根据文件  (不容易移除）
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable GetFile(string Cachename, string filenameJG, string filename)
        {
            DataTable dataTable = new DataTable();
            this.GetCacheValue(Cachename);
            if (this.cache != null)
            {
                if (this.cache.GetType().ToString().Equals("System.Data.DataTable"))
                {
                    dataTable = ((DataTable)this.cache).Copy();
                }
                this.cache = null;
            }
            if (dataTable.Rows.Count == 0 || dataTable.Columns.Count == 0)
            {
                dataTable.Clear();
                dataTable.Dispose();
                if (File.isexist(filenameJG) && File.isexist(filename))
                {
                    dataTable.ReadXmlSchema(filenameJG);
                    dataTable.ReadXml(filename);
                    this.clean();
                    HttpRuntime.Cache.Insert(this.cacheName, dataTable.Copy(), new CacheDependency(filename), System.Web.Caching.Cache.NoAbsoluteExpiration, System.Web.Caching.Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                }
            }
            return dataTable;
        }
        /// <summary>
        /// 把字符跟文件关联  (不容易移除）
        /// </summary>
        /// <param name="Cachename">缓存名称</param>
        /// <param name="str">要存储的字段串</param>
        /// <param name="FileName">要关联的文件名(为空取，不为空保存）</param>
        /// <returns>string</returns>
        public object GetObject(string Cachename, object str, string FileName)
        {
            if (FileName.Length > 0)
            {
                this.clean();
                HttpRuntime.Cache.Insert(this.cacheName, str, new CacheDependency(FileName), System.Web.Caching.Cache.NoAbsoluteExpiration, System.Web.Caching.Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
            }
            else
            {
                this.GetCacheValue(Cachename);
                if (this.cache != null)
                {
                    str = this.cache;
                }
            }
            return str;
        }
        /// <summary>
        /// 把字符跟文件关联  (不容易移除）
        /// </summary>
        /// <param name="Cachename">缓存名称</param>
        /// <param name="str">要存储的字段串(为null时取）</param>
        ///             <param name="ntime">保存的时间</param>
        /// <returns>string</returns>
        public object GetObject(string Cachename, object str, int ntime)
        {
            if (str != null)
            {
                this.clean();
                this.AddCacheValue(str, ntime);
            }
            else
            {
                this.GetCacheValue(Cachename);
                if (this.cache != null)
                {
                    str = this.cache;
                }
            }
            return str;
        }
        /// <summary>
        /// 返回string 根据文件  (不容易移除）
        /// </summary>
        /// <returns>string</returns>
        public string GetFile(string Cachename, string filename)
        {
            string text = "";
            this.GetCacheValue(Cachename);
            if (this.cache != null)
            {
                text = this.cache.ToString();
            }
            if (text.Length == 0)
            {
                if (File.isexist(filename))
                {
                    text = File.ReadFile(filename);
                    this.clean();
                    HttpRuntime.Cache.Insert(this.cacheName, text, new CacheDependency(filename), System.Web.Caching.Cache.NoAbsoluteExpiration, System.Web.Caching.Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                }
            }
            return text;
        }
        /// <summary>
        /// 支持SQL语句。并且根据NTIME的设置保存到全局变量里
        /// </summary>
        /// <param name="Cachename">要保存的全局变量的名称</param>
        /// <param name="strsql">要执行的SQL语句</param>
        /// <param name="ntime">保存全局变量的时间</param>
        /// <returns>返回一个DATATABLE 有可能返回null</returns>
        public DataTable getrows(string Cachename, string strsql, int ntime)
        {
            return this.getrows(Cachename, strsql, ntime, "");
        }
        /// <summary>
        /// 支持SQL语句。并且根据NTIME的设置保存到全局变量里
        /// </summary>
        /// <param name="Cachename">要保存的全局变量的名称</param>
        /// <param name="strsql">要执行的SQL语句</param>
        /// <param name="ntime">保存全局变量的时间</param>
        /// <param name="sqllink">要链接的数据库</param>
        /// <returns>返回一个DATATABLE 有可能返回null</returns>
        public DataTable getrows(string Cachename, string strsql, int ntime, string sqllink)
        {
            DataTable dataTable = null;
            if (ntime <= 0)
            {
                Database database = new Database(sqllink);
                try
                {
                    dataTable = database.GetData(strsql);
                }
                catch (Exception)
                {
                }
                finally
                {
                    database.Dispose();
                }
            }
            else
            {
                this.GetCacheValue(Cachename);
                if (this.cache != null)
                {
                    if (this.cache.GetType().ToString().Equals("System.Data.DataTable"))
                    {
                        dataTable = ((DataTable)this.cache).Copy();
                    }
                    this.cache = null;
                }
                if (dataTable == null)
                {
                    Database database = new Database();
                    database.ModiySQL(sqllink);
                    try
                    {
                        dataTable = database.GetData(strsql);
                    }
                    catch (Exception)
                    {
                    }
                    finally
                    {
                        database.Dispose();
                        database = null;
                    }
                    this.AddCacheValue(dataTable.Copy(), ntime);
                }
            }
            if (dataTable == null)
            {
                dataTable = new DataTable();
            }
            return dataTable;
        }
    }
}