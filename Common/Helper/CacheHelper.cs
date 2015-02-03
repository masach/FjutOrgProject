using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Cmj_Common
{
    /// <summary>
    /// 缓存管理类，提供有效生存期管理
    /// </summary>
    public class CacheHelper
    {
        private static CacheHelper cacheManager = new CacheHelper();
        private Hashtable _caches;//缓冲池

        private CacheHelper()
        {
            _caches = new Hashtable();
        }
        /// <summary>
        /// 返回一个实例
        /// </summary>
        /// <returns></returns>
        public static CacheHelper GetInstance()
        {
            if (System.Web.HttpContext.Current.Application["ltd.Cache.Manager"] != null)
                return System.Web.HttpContext.Current.Application["ltd.Cache.Manager"] as CacheHelper;
            else
            {
                System.Web.HttpContext.Current.Application["ltd.Cache.Manager"] = cacheManager;
                return System.Web.HttpContext.Current.Application["ltd.Cache.Manager"] as CacheHelper;
            }
        }
        /// <summary>
        /// 把CacheHelper推入堆中
        /// </summary>
        /// <param name="gm"></param>
        private void Push(CacheHelper gm)
        {
            System.Web.HttpContext.Current.Application["ltd.Cache.Manager"] = gm;
        }
        /// <summary>
        /// 添加一个缓冲，缓冲时间默认两分钟
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="key"></param>
        public bool Add(string key, object obj)
        {
            try
            {
                if (HasKey(key))
                    Remove(key);
                _caches.Add(key, new Cache(obj));
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 添加一个缓冲，缓冲时间需要指定
        /// </summary>
        /// <param name="key">键值</param>
        /// <param name="obj">缓冲对象</param>
        /// <param name="spanSecond">有效时间，单位/秒</param>
        public bool Add(string key, object obj, int spanSecond)
        {
            try
            {
                if (HasKey(key))
                    Remove(key);
                _caches.Add(key, new Cache(obj, spanSecond));
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 添加一个缓冲，并指定永不过期
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="IsConstCache">只能为ture</param>
        /// <returns></returns>
        public bool Add(string key, object obj, bool IsConstCache)
        {
            try
            {
                if (HasKey(key))
                    Remove(key);
                _caches.Add(key, new Cache(obj, IsConstCache));
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 添加缓冲
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="typeSpan">指定缓冲的时间,1=小时,2=分钟,3=秒,其余数值均当成永不过期处理</param>
        /// <param name="span"></param>
        /// <returns></returns>
        public bool Add(string key, object obj, int typeSpan, int span)
        {
            try
            {
                if (HasKey(key))
                    Remove(key);
                _caches.Add(key, new Cache(obj, typeSpan, span));
                return true;
            }
            catch
            {
                return false;
            }

        }
        /// <summary>
        /// 更新一个缓冲，如果该缓冲不存在，则新添加一个缓冲
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        public void Update(string key, object obj)
        {
            if (HasKey(key))
            {
                Cache cc = _caches[key] as Cache;
                cc.Update(obj);
            }
            else
            {
                Add(key, obj);
            }

        }
        /// <summary>
        /// 是否存在该KEY
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool HasKey(string key)
        {
            if (_caches[key] != null)
                return true;
            return false;
        }
        /// <summary>
        /// 移除一个缓冲
        /// </summary>
        /// <param name="key"></param>
        public void Remove(string key)
        {
            _caches.Remove(key);
        }
        /// <summary>
        /// 返回缓冲的数目
        /// </summary>  
        public int Length
        {
            get { return _caches.Count; }
        }

        /// <summary>
        /// 索引返回被缓冲对象，如果已经过期将返回空
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object this[string key]
        {
            get
            {
                if (!HasKey(key))
                    return null;
                // check the cache is timeout
                if (((Cache)_caches[key]).IsTimeout())
                {
                    Remove(key);
                    return null;
                }
                return ((Cache)_caches[key])._cache;
            }
            set
            {
                Update(key, value);
            }

        }
        /// <summary>
        /// 返回对对象有效性的常规描述
        /// </summary>
        public string GetDetail(string key)
        {
            object buf = _caches[key];
            if (buf != null)
                return ((Cache)buf).LimitTimeSpan();
            return "该缓冲已经失效";
        }
    }

    /// <summary>
    /// 缓冲类
    /// </summary>
    internal class Cache
    {
        public DateTime _startTime;
        public TimeSpan _timespan;//时间间隔
        public object _cache;//缓冲对象
        public bool TimeLimit;

        public Cache(object o)
        {
            _startTime = DateTime.Now;
            _timespan = new TimeSpan(0, 0, 120);
            _cache = o;
            TimeLimit = true;

        }
        public Cache(object o, int spanSecond)
        {
            _startTime = DateTime.Now;
            _timespan = new TimeSpan(0, 0, spanSecond);
            _cache = o;

        }
        public Cache(object o, bool IsConstCache)
        {
            _startTime = DateTime.Now;
            _timespan = new TimeSpan(0, 0, 0);
            _cache = o;
            SetNoLimitCache();
        }
        public Cache(object o, int typeSpan, int spanSecond)
        {
            _startTime = DateTime.Now;
            TimeLimit = true;//20090601 修改添加
            switch (typeSpan)
            {
                case 1:
                    _timespan = new TimeSpan(spanSecond, 0, 0);
                    break;
                case 2:
                    _timespan = new TimeSpan(0, spanSecond, 0);
                    break;
                case 3:
                    _timespan = new TimeSpan(0, 0, spanSecond);
                    break;
                default:
                    SetNoLimitCache();
                    break;
            }
            _cache = o;

        }

        public void SetNoLimitCache()
        {
            TimeLimit = false;
        }
        /// <summary>
        /// 设置过期时间
        /// </summary>
        /// <param name="span"></param>
        public void SetTimeSpan(int span)
        {
            _timespan = new TimeSpan(0, 0, span);
        }
        /// <summary>
        /// 检查是否过期
        /// </summary>
        /// <returns></returns>
        public bool IsTimeout()
        {
            if (!TimeLimit)
                return false;
            if (DateTime.Now <= (_startTime + _timespan))
                return false;
            return true;
        }
        /// <summary>
        /// 更新缓冲
        /// </summary>
        /// <param name="obj"></param>
        public void Update(object obj)
        {
            _cache = obj;
            _startTime = DateTime.Now;
        }
        /// <summary>
        /// 返回对对象有效性的常规描述
        /// </summary>
        /// <returns></returns>
        public string LimitTimeSpan()
        {
            return string.Format("该缓冲对象的有效时间长度为:{0}秒,将在{1}失效。", _timespan.Seconds.ToString(), ((DateTime)(_startTime + _timespan)).ToString());
        }

    }
}
