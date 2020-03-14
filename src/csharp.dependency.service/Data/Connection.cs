namespace csharp.dependency.service.Data
{
    public class Connection
    {
        public string mysqlConnection { get; set; }

        public Connection()
        {
            mysqlConnection = "Server=192.168.2.162;Database=csharpdependency;Uid=root;Pwd=03102593;";
        }
    }
}
