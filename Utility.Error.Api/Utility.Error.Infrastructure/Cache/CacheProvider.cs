using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Extensions.Caching.Distributed;

using Utility.Error.Application.Interfaces;

namespace Utility.Error.Infrastructure.Cache
{
    /// <summary>
    /// CacheProvider.
    /// </summary>
    public class CacheProvider : ICacheProvider
    {
        private readonly IDistributedCache _distributedCache;

        // Public Methods.
        #region PublicMethods

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="distributedCache"></param>
        public CacheProvider(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        #endregion

        // Interface Implementation.
        #region InterfaceImplementation

        /// <summary>
        /// Add.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        public void Add(string key, object obj)
        {
            var cacheEntryOptions = new DistributedCacheEntryOptions() { SlidingExpiration = new TimeSpan(0, 1, 0, 0) };  // Needs reading from Config.
            _distributedCache.Set(key, ObjectToByteArray(obj), cacheEntryOptions);
        }

        /// <summary>
        /// Get.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object Get(string key)
        {
            var byteArray = _distributedCache.Get(key);
            return null == byteArray ? null : ByteArrayToObject(byteArray);
        }

        #endregion

        // Private Methods.
        #region PrivateMethods

        /// <summary>
        /// ObjectToByteArray.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private byte[] ObjectToByteArray(object obj)
        {
            var formatter = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, obj);
                return stream.ToArray();
            }
        }

        /// <summary>
        /// ByteArrayToObject.
        /// </summary>
        /// <param name="byteArray"></param>
        /// <returns></returns>
        private object ByteArrayToObject(byte[] byteArray)
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                stream.Write(byteArray, 0, byteArray.Length);
                stream.Seek(0, SeekOrigin.Begin);
                return formatter.Deserialize(stream);
            }
        }

        #endregion
    }
}
