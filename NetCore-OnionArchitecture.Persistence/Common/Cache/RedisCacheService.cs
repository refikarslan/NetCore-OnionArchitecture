using NetCore_OnionArchitecture.Domain.Common.Common.Cache;
using StackExchange.Redis;
using Newtonsoft.Json;
using System.Text;


namespace NetCore_OnionArchitecture.Persistence.Common.Cache
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IConnectionMultiplexer _redisCon;
        private readonly StackExchange.Redis.IDatabase _cache;
        private TimeSpan ExpireTime => TimeSpan.FromDays(1);

        public RedisCacheService(IConnectionMultiplexer redisCon)
        {
            _redisCon = redisCon;
            _cache = redisCon.GetDatabase();
        }

        public async Task Clear(string key)
        {
            await _cache.KeyDeleteAsync(key);
        }

        public void ClearAll()
        {
            var endpoints = _redisCon.GetEndPoints(true);
            foreach (var endpoint in endpoints)
            {
                var server = _redisCon.GetServer(endpoint);
                server.FlushAllDatabases();
            }
        }

        public async Task<T?> GetOrAddAsync<T>(string key, Func<Task<T>> action) where T : class
        {
            var result = await _cache.StringGetAsync(key);
            if (result.IsNull)
            {
                result = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(await action());
                await SetValueAsync(key, result);
            }
            return System.Text.Json.JsonSerializer.Deserialize<T>(result);
        }

        public async Task<string?> GetValueAsync(string key)
        {
            return await _cache.StringGetAsync(key);
        }

        public async Task<bool> SetValueAsync(string key, string value)
        {
            return await _cache.StringSetAsync(key, value, ExpireTime);
        }

        public T? GetOrAdd<T>(string key, Func<T> action) where T : class
        {
            var result = _cache.StringGet(key);
            if (result.IsNull)
            {
                result = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(action());
                _cache.StringSet(key, result, ExpireTime);
            }
            return System.Text.Json.JsonSerializer.Deserialize<T>(result);
        }

        public async Task<bool> SetValueAsync(string key, object value)
        {
            var dataSerialize = JsonConvert.SerializeObject(value, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return await _cache.StringSetAsync(key, Encoding.UTF8.GetBytes(dataSerialize), ExpireTime);
        }

        public async Task<List<T>> GetAll<T>(string key)
        {
            var result = await _cache.StringGetAsync(key);

            if (!result.IsNull)
                return JsonConvert.DeserializeObject<List<T>>(result) ?? new List<T>();

            return null;
        }

        public async Task<T?> GetModel<T>(string key)
        {
            var result = await _cache.StringGetAsync(key);
            return JsonConvert.DeserializeObject<T>(result);
        }
    }

}
