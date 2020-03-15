using csharp.dependency.core.Interface;
using csharp.dependency.service.GeneralService;
using Microsoft.AspNetCore.Mvc;

namespace csharp.dependency.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StarredRepositoryController 
        : BaseController
    {
        public StarredRepositoryController(IMethod _SMethod, SRedisService _SRedisService) 
            : base(_SMethod, _SRedisService)
        {
        }
    }
}