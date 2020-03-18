using System.Threading.Tasks;
using Xunit;

namespace csharp.dependency.api.test
{
    public class RepositoryUnit : BaseUnit
    {
        private string authToken;

        public RepositoryUnit()
        {
            this.authToken = token;
        }

        [Fact]
        public async Task Get_Repository()
        {
            var response = await client.GetAsync("/api/repository/get");
            ThrowNewException(response);
        }
    }
}
