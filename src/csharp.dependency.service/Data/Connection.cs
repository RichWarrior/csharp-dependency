namespace csharp.dependency.service.Data
{
    public class Connection
    {
        public string mysqlConnection { get; set; }
        public string redisHost { get; set; }
        public int redisPort { get; set; }
        public string redisPassword { get; set; }

        public Connection()
        {
            mysqlConnection = "Server=192.168.2.204;Database=csharpdependency;Port=3306;Uid=root;Pwd=03102593;";
            redisHost = "192.168.2.204";
            redisPort = 6379;
            redisPassword = "03102593";
        }
    }
}
