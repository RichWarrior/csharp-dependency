using csharp.dependency.api.Models;
using csharp.dependency.core.CustomEntity.Response.Followers;
using csharp.dependency.core.Entity;
using csharp.dependency.core.Interface;
using csharp.dependency.service.GeneralService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace csharp.dependency.api.Controllers
{
    [Route("api/followers")]
    [ApiController]
    [Authorize]
    public class FollowersController :
        BaseController
    {
        IGithub _SGithub;
        IUser _SUser;
        public FollowersController(IMethod _SMethod, SRedisService _SRedisService,IGithub _SGithub,IUser _SUser)
            : base(_SMethod, _SRedisService)
        {
            this._SGithub = _SGithub;
            this._SUser = _SUser;
        }

        /// <summary>
        /// Kullanıcının Github Takipçilerini Getirir.
        /// </summary>
        /// <returns></returns>
        [HttpGet("get")]
        public async Task<IActionResult> Get_Followers()
        {
            BaseResult<ResponseFollowers> baseResult = new BaseResult<ResponseFollowers>();
            User user = _SUser.Get_By_Id(GetUserId());
            baseResult.data.followers = await _SGithub.Get_Github_Followers(user,_SRedisService);
            return new JsonResult(baseResult);
        }
    }
}