using csharp.dependency.core.CustomEntity.Request.User;
using System;
using System.Threading.Tasks;
using Xunit;

namespace csharp.dependency.api.test
{
    public class UserUnit : BaseUnit
    {
        private string authToken;

        public UserUnit()
        {
            this.authToken = token;
        }

        [Fact]
        public async Task Check_Github_User()
        {
            RequestCheckGithubUser githubItem = new RequestCheckGithubUser()
            {
                username = "RichWarrior"
            };
            var request = ConvertStringContent(githubItem);
            var response = await client.PostAsync("/api/user/checkgithubuser", request);
            ThrowNewException(response);
        }

        [Fact]
        public async Task Register()
        {
            RequestRegister requestRegister = new RequestRegister()
            {
                email = Guid.NewGuid().ToString()+"@hotmail.com",
                password = Guid.NewGuid().ToString(),
                github_username = "RichWarrior"
            };
            var request = ConvertStringContent(requestRegister);
            var response = await client.PostAsync("/api/user/register",request);
            ThrowNewException(response);
        }

        [Fact]
        public async Task Change_Language()
        {
            RequestChangeLanguage langItem = new RequestChangeLanguage()
            {
                locale = "tr"
            };
            var request = ConvertStringContent(langItem);
            var response = await client.PostAsync("/api/user/changelanguage",request);
            ThrowNewException(response);
        }

        [Fact]
        public async Task Get_User()
        {
            var response = await client.GetAsync("/api/user/getuser");
            ThrowNewException(response);
        }
    }
}
