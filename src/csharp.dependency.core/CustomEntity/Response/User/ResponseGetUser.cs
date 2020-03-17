using csharp.dependency.core.CustomEntity.Github;
using System.Collections.Generic;

namespace csharp.dependency.core.CustomEntity.Response.User
{
    public class ResponseGetUser
    {
        public GithubUser user { get; set; }
        public List<GithubFollowers> followers { get; set; }
        public List<GithubFollowing> followings { get; set; }
        public List<GithubStarredRepository> starredRepositories { get; set; }
        public List<GithubRepository> repositories { get; set; }
    }
}
