using System.Collections.Generic;

namespace csharp.dependency.socket.client.Entity.Dotnet
{
    public class SocketEntity
    {
        public List<Project> projects { get; set; }

        public SocketEntity()
        {
            this.projects = new List<Project>();
        }

        public sealed class Project
        {
            public string name { get; set; }
            public string sdk { get; set; }
            public string targetFramework { get; set; }
            public List<Reference> references { get; set; }

            public Project()
            {
                this.references = new List<Reference>();
            }
        }       

        public sealed class Reference
        {
            public string include { get; set; }
            public int includeType { get; set; }
            public string version { get; set; }
        }        
    }
}
