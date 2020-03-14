using System.ComponentModel.DataAnnotations;

namespace csharp.dependency.core.CustomEntity.Request.User
{
    public class RequestLogin
    {
        public string email { get; set; }
        public string password { get; set; }
    }
}
