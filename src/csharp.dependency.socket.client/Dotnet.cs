using csharp.dependency.core.CustomEntity.Github;
using csharp.dependency.core.Entity;
using csharp.dependency.core.Interface;
using csharp.dependency.service.Data;
using csharp.dependency.service.GeneralService;
using csharp.dependency.socket.client.Entity.Dotnet;
using csharp.dependency.socket.client.Enums;
using Newtonsoft.Json;
using Renci.SshNet;
using SocketIOClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

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
                Task.Run(async () =>
                {
                    GithubRepository githubRepository = JsonConvert.DeserializeObject<GithubRepository>(repo.Text);
                    if (githubRepository != null)
                    {
                        IServer _SServer = new SServer(new DbContext());
                        Console.WriteLine($"{githubRepository.full_name} Proje Bağımlılıkları Görüntüleme İşlemi Başladı.");
                        Server server = _SServer.GetRandomServer();
                        if (server != null)
                        {
                            Console.WriteLine($"{githubRepository.full_name} Projesi {server.server_name} Sunucusunda İşlenecek!");
                            SshClient sshClient = null;
                            if (Debugger.IsAttached)
                            {
                                sshClient = new SshClient(server.local_ip, server.local_port, "root", "03102593");
                            }
                            else
                            {
                                sshClient = new SshClient(server.remote_ip, server.remote_port, "root", "CB4434000bc");
                            }
                            using (sshClient)
                            {
                                try
                                {
                                    sshClient.Connect();
                                    SshCommand lsCommand = sshClient.CreateCommand("ls");
                                    using (lsCommand)
                                    {
                                        lsCommand.Execute();
                                        string message = lsCommand.Result;
                                        string[] bufferArr = message.Split('\n');
                                        List<string> directories = new List<string>(bufferArr);
                                        directories.RemoveAll(x => String.IsNullOrEmpty(x));
                                        if (directories.Contains(githubRepository.name))
                                        {
                                            Console.WriteLine($"{githubRepository.full_name} {server.server_name} Sunucusunda Bulundu ve Silindi!");
                                            SshCommand rmCommand = sshClient.CreateCommand($"rm -r {githubRepository.name}");
                                            using (rmCommand)
                                            {
                                                rmCommand.Execute();
                                            }
                                        }
                                    }
                                    SshCommand cloneCommand = sshClient.CreateCommand($"git clone {githubRepository.clone_url} && echo repository cloned");
                                    using (cloneCommand)
                                    {
                                        cloneCommand.BeginExecute();
                                        while (true)
                                        {
                                            Stream commandStream = cloneCommand.OutputStream;
                                            byte[] streamArr = new byte[commandStream.Length];
                                            commandStream.Read(streamArr, 0, (int)commandStream.Length);
                                            string message = Encoding.ASCII.GetString(streamArr);
                                            if (message.Contains("repository cloned"))
                                            {
                                                Console.WriteLine($"{githubRepository.full_name} Projesi {server.server_name} Sunucusuna Başarıyla İndirildi!");
                                                break;
                                            }
                                            Thread.Sleep(2000);
                                        }
                                    }
                                    SshCommand searchCommand = sshClient.CreateCommand($"find /root/{githubRepository.name}/ -name '*.csproj'");
                                    using (searchCommand)
                                    {
                                        searchCommand.Execute();
                                        string searchCommandResult = searchCommand.Result;
                                        string[] bufferArr = searchCommandResult.Split('\n');
                                        List<string> csprojFiles = new List<string>(bufferArr);
                                        csprojFiles.RemoveAll(x => String.IsNullOrEmpty(x));
                                        if (csprojFiles.Count > 0)
                                        {
                                            Console.WriteLine($"{githubRepository.full_name} Projesine Ait {csprojFiles.Count} Adet CSPROJ Dosyası Bulundu!");
                                            SftpClient sftp = null;
                                            if (Debugger.IsAttached)
                                            {
                                                sftp = new SftpClient(server.local_ip, server.local_port, "root", "03102593");
                                            }
                                            else
                                            {
                                                sftp = new SftpClient(server.remote_ip, server.remote_port, "root", "03102593");
                                            }
                                            using (sftp)
                                            {
                                                sftp.Connect();
                                                SocketEntity socketEntity = new SocketEntity();
                                                foreach (string csprojfile in csprojFiles)
                                                {
                                                    string[] csprojArr = csprojfile.Split('/');
                                                    string projectName = (csprojArr[csprojArr.Length - 1]).Replace(".csproj", "");
                                                    string fileName = Guid.NewGuid().ToString() + ".csproj";
                                                    string path = Directory.GetCurrentDirectory() + $"/{fileName}";
                                                    using (Stream stream = File.Create(path))
                                                    {
                                                        sftp.DownloadFile(csprojfile, stream);
                                                    }
                                                    if (File.Exists(path))
                                                    {
                                                        string csprojContent = File.ReadAllText(path);
                                                        File.Delete(path);
                                                        Project project = null;
                                                        using (var stringReader = new System.IO.StringReader(csprojContent))
                                                        {
                                                            var serializer = new XmlSerializer(typeof(Project));
                                                            project = serializer.Deserialize(stringReader) as Project;
                                                        }
                                                        if (project != null)
                                                        {
                                                            SocketEntity.Project socketProject = new SocketEntity.Project();
                                                            socketProject.name = projectName;
                                                            socketProject.sdk = project.Sdk;
                                                            foreach (ItemGroup itemGroup in project.ItemGroup)
                                                            {
                                                                if (itemGroup.PackageReference.Count > 0)
                                                                {
                                                                    List<SocketEntity.Reference> references = itemGroup.PackageReference.Select(x => new SocketEntity.Reference
                                                                    {
                                                                        include = x.Include,
                                                                        includeType = (int)enumIncludeType.package,
                                                                        version = x.Version
                                                                    }).ToList();
                                                                    socketProject.references.AddRange(references);
                                                                }
                                                                if (itemGroup.ProjectReference.Count > 0)
                                                                {
                                                                    foreach (ProjectReference projectReference in itemGroup.ProjectReference)
                                                                    {
                                                                        string include = projectReference.Include;
                                                                        string[] includeArr = include.Split('\\');
                                                                        include = (includeArr[includeArr.Length - 1]).Replace(".csproj", "");
                                                                        socketProject.references.Add(new SocketEntity.Reference
                                                                        {
                                                                            include = include,
                                                                            includeType = (int)enumIncludeType.project,
                                                                            version = ""
                                                                        });
                                                                    }
                                                                }
                                                            }
                                                            foreach (PropertyGroup propertyGroup in project.PropertyGroup)
                                                            {
                                                                if (!String.IsNullOrEmpty(propertyGroup.TargetFramework))
                                                                {
                                                                    socketProject.targetFramework = propertyGroup.TargetFramework;
                                                                }
                                                                if (!String.IsNullOrEmpty(socketProject.targetFramework))
                                                                {
                                                                    break;
                                                                }
                                                            }
                                                            socketEntity.projects.Add(socketProject);
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine($"{githubRepository.full_name} CSPROJ Dosyası Okunamadı!");
                                                            return;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine($"{githubRepository.full_name} CSPROJ Dosyası İndirilemedi!");
                                                        return;
                                                    }
                                                }
                                                Console.WriteLine($"{githubRepository.full_name} Projesi Başarıyla İşlendi!");
                                                client.EmitAsync("showDependency", githubRepository, socketEntity).Wait();
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine($"{githubRepository.full_name} Reposuna Ait Bir CSPROJ Dosyası Bulunamadı!");
                                            return;
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"{ex.Message}");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Aktif Bir Sunucu Bulunamadı!");
                            return;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Hatalı Bir Veri Geldi!");
                        return;
                    }
                });
            });
        }
    }
}
