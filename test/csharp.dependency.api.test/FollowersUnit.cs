using System.Threading.Tasks;
using Xunit;

namespace csharp.dependency.api.test
{
    public class FollowersUnit :BaseUnit
    {
        private string authToken;

        public FollowersUnit()
        {
            this.authToken = token;
        }

        [Fact]
        public async Task Get_Followers()
        {
            var response = await client.GetAsync("/api/followers/get");
            ThrowNewException(response);
        }
    }
}
