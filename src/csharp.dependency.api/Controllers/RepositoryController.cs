using csharp.dependency.api.Models;
using csharp.dependency.core.CustomEntity.Response.Repository;
using csharp.dependency.core.Entity;
using csharp.dependency.core.Interface;
using csharp.dependency.service.GeneralService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace csharp.dependency.api.Controllers
{
    [Route("api/repository")]
    [ApiController]
    [Authorize]
    public class RepositoryController
        : BaseController
    {
        IGithub _SGithub;
        IUser _SUser;
        public RepositoryController(IMethod _SMethod, SRedisService _SRedisService, IGithub _SGithub, IUser _SUser) 
            : base(_SMethod, _SRedisService)
        {
            this._SGithub = _SGithub;
            this._SUser = _SUser;
        }

        /// <summary>
        /// Kullanıcının Repolarını Getirir.
        /// </summary>
        /// <returns></returns>
        [HttpGet("get")]
        public async Task<IActionResult> Get_Repository()
        {
            BaseResult<ResponseRepository> baseResult = new BaseResult<ResponseRepository>();
            User user = _SUser.Get_By_Id(GetUserId());
            baseResult.data.repositories = await _SGithub.Get_Github_Repository(user, _SRedisService);
            return new JsonResult(baseResult);
        }
    }
}