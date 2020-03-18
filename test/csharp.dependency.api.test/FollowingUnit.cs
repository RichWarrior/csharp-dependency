using System.Threading.Tasks;
using Xunit;

namespace csharp.dependency.api.test
{
    public class FollowingUnit : BaseUnit
    {
        private string authToken;

        public FollowingUnit()
        {
            this.authToken = token;
        }

        [Fact]
        public async Task Get_Following()
        {
            var response = await client.GetAsync("/api/following/get");
            ThrowNewException(response);
        }
    }
}
