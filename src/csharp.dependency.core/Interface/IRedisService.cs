using System;
using System.Threading.Tasks;

namespace csharp.dependency.core.Interface
{
    public interface IRedisService
    {
        void Connect();
        Task<bool>Set(int db,string key, string value,DateTime? expireDate);
        Task<string> Get(int db,string key);
    }
}
