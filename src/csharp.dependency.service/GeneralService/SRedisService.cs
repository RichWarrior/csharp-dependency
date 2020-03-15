using csharp.dependency.core.Interface;
using csharp.dependency.service.Data;
using StackExchange.Redis;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace csharp.dependency.service.GeneralService
{
    public class SRedisService : IRedisService
    {
        private ConnectionMultiplexer _redis;

        Connection connection = new Connection();
        public void Connect()
        {
            try
            {
                string conn = $"{connection.redisHost}:{connection.redisPort},password={connection.redisPassword}";
                _redis = ConnectionMultiplexer.Connect(conn);
            }
            catch (RedisConnectionException err)
            {
                Debug.WriteLine(err);                
            }
        }       

        private IDatabase GetDatabase(int id)
        {
            return _redis.GetDatabase(id);
        }

        public async Task<string> Get(int db, string key)
        {
            IDatabase rdb = GetDatabase(db);
            return await rdb.StringGetAsync(key);
        }

        public async Task<bool> Set(int db, string key, string value,DateTime? expireDate)
        {
            bool rtn = true;
            try
            {
                IDatabase rdb = GetDatabase(db);
                if (expireDate != null)
                {
                    var expiryTimeSpan = expireDate.Value.Subtract(DateTime.UtcNow);
                    await rdb.StringSetAsync(key, value,expiryTimeSpan);
                }
                else
                {
                    await rdb.StringSetAsync(key, value);
                }                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                rtn = false;
            }
            return rtn;
        }
    }
}
