using csharp.dependency.core.CustomEntity.Github;
using csharp.dependency.core.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace csharp.dependency.core.Interface
{
    public interface IGithub
    {
        Tuple<bool,GithubUser> Check_Github_User(string username);
        GithubUser Get_Github_User(string username);
        List<GithubFollowers> Get_Github_Followers(string username);
        List<GithubFollowing> Get_Github_Following(string username);
        List<GithubStarredRepository> Get_Github_Starred_Repository(string username);
        List<GithubRepository> Get_Github_Repository(string username);
        Task<GithubUser> Get_Github_User(User user, IRedisService _SRedisService);
        Task<List<GithubFollowers>> Get_Github_Followers(User user, IRedisService _SRedisService);
        Task<List<GithubFollowing>> Get_Github_Following(User user, IRedisService _SRedisService);
        Task<List<GithubStarredRepository>> Get_Github_Starred_Repository(User user, IRedisService _SRedisService);
        Task<List<GithubRepository>> Get_Github_Repository(User user, IRedisService _SRedisService);
    }
}
