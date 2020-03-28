using csharp.dependency.core.Entity;
using csharp.dependency.core.Interface;
using System.Collections.Generic;
using System.Linq;

namespace csharp.dependency.service.GeneralService
{
    public class SServer : IServer
    {
        IDbContext dbContext;
        public SServer(IDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Server GetRandomServer()
        {
            string sql = "SELECT * FROM server WHERE status_id = 2 ORDER BY RAND() LIMIT 1;";
            return dbContext.GetByQuery<Server>(sql, new { });
        }

        public List<Server> GetServers()
        {
            string sql = "SELECT * FROM server WHERE status_id = 2";
            return dbContext.GetByQueryAll<Server>(sql, new { }).ToList();
        }

        public bool UpdateServer(Server server)
        {
            return dbContext.Update(server);
        }
    }
}
