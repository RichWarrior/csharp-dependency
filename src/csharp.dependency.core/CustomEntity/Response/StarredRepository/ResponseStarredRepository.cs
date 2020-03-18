using csharp.dependency.core.CustomEntity.Github;
using System.Collections.Generic;

namespace csharp.dependency.core.CustomEntity.Response.StarredRepository
{
    public class ResponseStarredRepository
    {
        public List<GithubStarredRepository> starredRepositories{ get; set; }
    }
}
