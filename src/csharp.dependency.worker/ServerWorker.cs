using csharp.dependency.core.Entity;
using csharp.dependency.core.Enums;
using csharp.dependency.core.Interface;
using csharp.dependency.service.Data;
using csharp.dependency.service.GeneralService;
using Microsoft.Extensions.Hosting;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace csharp.dependency.worker
{
    public class ServerWorker : BackgroundService
    {
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine("Server Worker Çalıştırıldı!");
                IServer _SServer = new SServer(new DbContext());
                List<Server> servers = _SServer.GetServers();
                foreach (Server server in servers)
                {
                    server.status_id = (int)enumStatus.Pasif;
                    if (_SServer.UpdateServer(server))
                    {
                        Console.WriteLine($"{server.server_name} Sunucusu Pasif Duruma Güncellendi!");
                        if (Debugger.IsAttached)
                            Thread.Sleep((int)TimeSpan.FromSeconds(10).TotalMilliseconds);
                        else
                            Thread.Sleep((int)TimeSpan.FromMinutes(10).TotalMilliseconds);
                        SshClient sshClient = null;
                        if (Debugger.IsAttached)
                            sshClient = new SshClient(server.local_ip, server.local_port, "root", "03102593");
                        else
                            sshClient = new SshClient(server.remote_ip, server.remote_port, "root", "031002593");
                        try
                        {
                            using (sshClient)
                            {
                                sshClient.Connect();
                                SshCommand lsCommand = sshClient.CreateCommand("ls");
                                lsCommand.Execute();
                                string lsCommandResult = lsCommand.Result;
                                string[] bufferArr = lsCommandResult.Split('\n');
                                List<string> directories = new List<string>(bufferArr);
                                directories.RemoveAll(x=> String.IsNullOrEmpty(x));
                                foreach (string directory in directories)
                                {
                                    SshCommand rmCommand = sshClient.CreateCommand($"rm -r {directory}");
                                    rmCommand.Execute();
                                }
                                server.status_id = (int)enumStatus.Aktif;
                                if (_SServer.UpdateServer(server))
                                {
                                    Console.WriteLine($"{server.server_name} Sunucusu Aktif Duruma Başarıyla Güncellendi!");
                                }
                                else
                                {
                                    Console.WriteLine($"{server.server_name} Sunucusu Aktif Duruma Güncellenemedi!");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"{ex.Message}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"{server.server_name} Sunucusu Pasif Duruma Güncellenemedi!");
                    }
                }
                if (Debugger.IsAttached)
                    await Task.Delay((int)TimeSpan.FromSeconds(30).TotalMilliseconds, stoppingToken);
                else
                    await Task.Delay((int)TimeSpan.FromDays(1).TotalMilliseconds, stoppingToken);
            }
        }
    }
}
