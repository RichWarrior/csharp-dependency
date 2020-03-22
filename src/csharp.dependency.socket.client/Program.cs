using SocketIOClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace csharp.dependency.socket.client
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new SocketIO("http://192.168.2.162:8081");
            Task task = new Task(() => { client.ConnectAsync(); });
            task.RunSynchronously();
            if (client.State == SocketIOState.Connected)
            {
                Console.WriteLine("Socket IO Connected.");
            }
            Dotnet dotnet = new Dotnet(client);
            while (true)
            {
                if (client.State != SocketIOState.Connected)
                {
                    Console.WriteLine("Socket IO Not Connected");
                    break;
                }
                Thread.Sleep(2000);
            }
            Console.ReadKey();
        }
    }
}
