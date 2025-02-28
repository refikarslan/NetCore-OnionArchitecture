﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore_OnionArchitecture.Domain.Common.Common.Cache
{
    public interface IRedisCacheService
    {
        Task<List<T>> GetAll<T>(string key);
        Task<T?> GetModel<T>(string key);
        Task<string?> GetValueAsync(string key);
        Task<bool> SetValueAsync(string key, string value);
        Task<bool> SetValueAsync(string key, object value);
        Task<T?> GetOrAddAsync<T>(string key, Func<Task<T>> action) where T : class;
        T? GetOrAdd<T>(string key, Func<T> action) where T : class;
        Task Clear(string key);
        void ClearAll();

    }

}

