using Memory.Cache.Entity;
using Memory.Cache.Services;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Memory.Cache
{
    public class CacheProvider : ICacheProvider
    {
        private static readonly SemaphoreSlim GetUsersSemaphore = new SemaphoreSlim(1, 1);

        private readonly IMemoryCache _cache;

        public CacheProvider(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        public async Task<IEnumerable<Employee>> GetCachedResponse()
        {
            try
            {
                return await GetCachedResponse(CacheKeys.Employees);
            }
            catch
            {
                throw;
            }
        }

        private async Task<IEnumerable<Employee>> GetCachedResponse(string employeesChaceKey)
        {
            var isAvaiable = _cache.TryGetValue(employeesChaceKey, out List<Employee> employees);

            if (isAvaiable)
            {
                return employees;
            }

            try
            {
                await GetUsersSemaphore.WaitAsync();

                isAvaiable = _cache.TryGetValue(employeesChaceKey, out employees);

                if (isAvaiable)
                {
                    return employees;
                }

                employees = EmployeeService.GetEmployeesDetailsFromDB();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                    SlidingExpiration = TimeSpan.FromMinutes(2),
                    Size = 1024
                };

                _cache.Set(employeesChaceKey, employees, cacheEntryOptions);
            }
            catch
            {
                throw;
            }
            finally
            {
                GetUsersSemaphore.Release();
            }

            return employees;
        }
    }
}
