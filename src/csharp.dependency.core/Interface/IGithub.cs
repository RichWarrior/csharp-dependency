using csharp.dependency.core.CustomEntity.Github;
using System;

namespace csharp.dependency.core.Interface
{
    public interface IGithub
    {
        Tuple<bool,GithubUser> Check_Github_User(string username);
    }
}
