using csharp.dependency.core.Entity;
using csharp.dependency.core.Interface;

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
    }
}
