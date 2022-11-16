using Memory.Cache.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Memory.Cache
{
    public interface ICacheProvider
    {
        Task<IEnumerable<Employee>> GetCachedResponse();
    }
}
