using Dapper.Contrib.Extensions;

namespace csharp.dependency.core.Entity
{
    [Table("user")]
    public class User : BaseEntity
    {
        public string github_username { get; set; }        
        public string email { get; set; }
        public string password { get; set; }
        public string default_lang { get; set; }
    }
}
