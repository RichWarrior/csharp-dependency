using csharp.dependency.api.Models;
using csharp.dependency.core.CustomEntity.Response.Following;
using csharp.dependency.core.Entity;
using csharp.dependency.core.Interface;
using csharp.dependency.service.GeneralService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace csharp.dependency.api.Controllers
{
    [Route("api/following")]
    [ApiController]
    [Authorize]
    public class FollowingController : BaseController
    {
        IGithub _SGithub;
        IUser _SUser;
        public FollowingController(IMethod _SMethod, SRedisService _SRedisService, IGithub _SGithub, IUser _SUser)
            : base(_SMethod, _SRedisService)
        {
            this._SGithub = _SGithub;
            this._SUser = _SUser;
        }
        /// <summary>
        /// Kullanıcının Github Takip Ettiklerini Getirir.
        /// </summary>
        /// <returns></returns>
        [HttpGet("get")]
        public async Task<IActionResult> Get_Following()
        {
            BaseResult<ResponseFollowing> baseResult = new BaseResult<ResponseFollowing>();
            User user = _SUser.Get_By_Id(GetUserId());
            baseResult.data.followings = await _SGithub.Get_Github_Following(user, _SRedisService);
            return new JsonResult(baseResult);
        }
    }
}