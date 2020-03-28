using csharp.dependency.core.Entity;
using System.Collections.Generic;

namespace csharp.dependency.core.Interface
{
    public interface IServer
    {
        Server GetRandomServer();
        List<Server> GetServers();
        bool UpdateServer(Server server);
    }
}
