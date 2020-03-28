using Dapper.Contrib.Extensions;

namespace csharp.dependency.core.Entity
{
    [Table("server")]
    public class Server : BaseEntity
    {
        public string server_name { get; set; }
        public string local_ip { get; set; }
        public int local_port { get; set; }
        public string remote_ip { get; set; }
        public int remote_port { get; set; }
    }
}
