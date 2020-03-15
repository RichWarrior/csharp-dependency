using csharp.dependency.core.Entity;
using csharp.dependency.core.Interface;

namespace csharp.dependency.service.GeneralService
{
    public class SUser : IUser
    {
        IDbContext dbContext;
        public SUser(IDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public User Check_User(string email, string github_username)
        {
            string sql = "SELECT * FROM user WHERE (email=@email OR github_username=@github_username) AND status_id = 2";
            return dbContext.GetByQuery<User>(sql, new { email = email, github_username = github_username });
        }

        public User Check_User_With_Password(string email, string password)
        {
            string sql = "SELECT * FROM user WHERE email=@email AND password=@password AND status_id = 2";
            return dbContext.GetByQuery<User>(sql, new { email = email, password = password });
        }

        public long Insert_User(User user)
        {
            return dbContext.Insert(user);
        }
    }
}
