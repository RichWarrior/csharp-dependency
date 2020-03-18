using System.Threading.Tasks;
using Xunit;

namespace csharp.dependency.api.test
{
    public class StarredRepositoryUnit : BaseUnit
    {
        private string authToken;

        public StarredRepositoryUnit()
        {
            this.authToken = token;
        }

        [Fact]
        public async Task Get_Starred_Repository()
        {
            var response = await client.GetAsync("/api/starredrepository/get");
            ThrowNewException(response);
        }
    }
}
