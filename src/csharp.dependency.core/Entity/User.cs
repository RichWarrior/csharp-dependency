using System;

namespace csharp.dependency.core.Entity
{
    public class User : BaseEntity
    {
        public string github_username { get; set; }
        public string email { get; set; }
        public string password { get; set; }  
    }
}
