using csharp.dependency.api.Models;
using csharp.dependency.core.CustomEntity.Response.StarredRepository;
using csharp.dependency.core.Entity;
using csharp.dependency.core.Interface;
using csharp.dependency.service.GeneralService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace csharp.dependency.api.Controllers
{
    [Route("api/starredrepository")]
    [ApiController]
    [Authorize]
    public class StarredRepositoryController 
        : BaseController
    {
        IGithub _SGithub;
        IUser _SUser;
        public StarredRepositoryController(IMethod _SMethod, SRedisService _SRedisService, IGithub _SGithub, IUser _SUser) 
            : base(_SMethod, _SRedisService)
        {
            this._SGithub = _SGithub;
            this._SUser = _SUser;
        }

        /// <summary>
        /// Kullanıcının Yıldızlı Repolarını Getirir.
        /// </summary>
        /// <returns></returns>
        [HttpGet("get")]
        public async Task<IActionResult> Get_Starred_Repository()
        {
            BaseResult<ResponseStarredRepository> baseResult = new BaseResult<ResponseStarredRepository>();
            User user = _SUser.Get_By_Id(GetUserId());
            baseResult.data.starredRepositories = await _SGithub.Get_Github_Starred_Repository(user, _SRedisService);
            return new JsonResult(baseResult);
        }
    }
}