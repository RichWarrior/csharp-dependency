using csharp.dependency.worker.Enums;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace csharp.dependency.worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    //args = new string[1];
                    //args[0] = "0";
                    if (args.Length>0)
                    {
                        try
                        {
                            int _args = Convert.ToInt32(args[0]);
                            enumArgs enumArgs = (enumArgs)_args;
                            switch (enumArgs)
                            {
                                case Enums.enumArgs.serverWorker:
                                    services.AddHostedService<ServerWorker>();
                                    break;
                                default:
                                    Console.WriteLine($"Bilinmeyen Args:{_args}");
                                    break;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }                    
                });
    }
}
