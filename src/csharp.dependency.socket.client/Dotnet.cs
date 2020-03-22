using csharp.dependency.core.CustomEntity.Github;
using Newtonsoft.Json;
using SocketIOClient;
using System.Threading.Tasks;

namespace csharp.dependency.socket.client
{
    /// <summary>
    /// Dotnet ve Dotnet Core Dependency İşlemlerinden Sorumludur.
    /// </summary>
    public class Dotnet
    {
        private SocketIO client;
        public Dotnet(SocketIO client)
        {
            this.client = client;

            client.On("_visualizedependency", repo =>
            {
                Task task = new Task(()=> {
                    GithubRepository githubRepository = JsonConvert.DeserializeObject<GithubRepository>(repo.Text);
                    client.EmitAsync("showDependency", githubRepository);
                });
                task.RunSynchronously();
            });
        }        
    }
}
