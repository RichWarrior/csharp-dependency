using csharp.dependency.api.Models;
using csharp.dependency.core.CustomEntity.Request.User;
using csharp.dependency.core.Enums;
using csharp.dependency.core.Interface;
using csharp.dependency.core.Validation.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace csharp.dependency.api.Controllers
{
    /// <summary>
    /// Kullanıcı ve Oturum İşlemlerini Yönetir.
    /// </summary>
    [Route("api/user")]
    [ApiController]
    public class UserController : BaseController
    {
        IGithub _SGithub;      
        public UserController(IMethod _SMethod,IGithub _SGithub) 
            : base(_SMethod)
        {
            this._SGithub = _SGithub;
        }

        /// <summary>
        /// Giriş Yapmak İçin Kullanılır.
        /// </summary>
        /// <returns></returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody]RequestLogin credentialItem)
        {
            if (!Validate<RequestLoginValidator>(credentialItem))
            {

            }
            return new JsonResult(credentialItem);
        }

        /// <summary>
        /// İlgili Kullanıcı Adına Ait Github Kullanıcısı Var Mı ? 
        /// Kontrol Etmek İçin Kullanılır.
        /// </summary>
        /// <returns></returns>
        [HttpPost("checkgithubuser")]
        [AllowAnonymous]
        public IActionResult Check_Github_User([FromBody]RequestCheckGithubUser githubItem)
        {
            BaseResult<bool> baseResult = new BaseResult<bool>();
            if (!Validate<RequestCheckGithubUserValidator>(githubItem))
            {
                baseResult.statusCode = HttpStatusCode.NotFound;
                baseResult.message = _SMethod.Get_Enum_Description(enumErrorMessage.invalidModel);
                return new NotFoundObjectResult(baseResult);
            }
            baseResult.data = _SGithub.Check_Github_User(githubItem.username);
            return new JsonResult(baseResult.data);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public IActionResult Register([FromBody]RequestRegister registerItem)
        {
            if (!Validate<RequestRegisterValidaitor>(registerItem))
            {

            }
            return new JsonResult(new { });
        }
    }
}