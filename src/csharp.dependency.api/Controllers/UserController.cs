using csharp.dependency.api.Models;
using csharp.dependency.core.CustomEntity.Request.User;
using csharp.dependency.core.CustomEntity.Response.User;
using csharp.dependency.core.Entity;
using csharp.dependency.core.Enums;
using csharp.dependency.core.Interface;
using csharp.dependency.core.Validation.User;
using csharp.dependency.service.GeneralService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace csharp.dependency.api.Controllers
{
    /// <summary>
    /// Kullanıcı ve Oturum İşlemlerini Yönetir.
    /// </summary>
    [Route("api/user")]
    [ApiController]
    [Authorize]
    public class UserController : BaseController
    {
        IGithub _SGithub;
        IUser _SUser;
        public UserController(IMethod _SMethod,SRedisService _SRedisService,
            IGithub _SGithub,IUser _SUser) 
            : base(_SMethod,_SRedisService)
        {
            this._SGithub = _SGithub;
            this._SUser = _SUser;
        }

        /// <summary>
        /// Giriş Yapmak İçin Kullanılır.
        /// </summary>
        /// <returns></returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]RequestLogin credentialItem)
        {
            BaseResult<ResponseLogin> baseResult = new BaseResult<ResponseLogin>();
            if (!Validate<RequestLoginValidator>(credentialItem))
            {
                baseResult.statusCode = HttpStatusCode.NotFound;
                baseResult.message = _SMethod.Get_Enum_Description(enumErrorMessage.invalidModel);
                return new NotFoundObjectResult(baseResult);
            }
            User user = _SUser.Check_User_With_Password(credentialItem.email,_SMethod.Generate_MD5(credentialItem.password));
            if (user == null)
            {
                baseResult.statusCode = HttpStatusCode.NotFound;
                baseResult.message = _SMethod.Get_Enum_Description(enumErrorMessage.loginFailed);
                return new NotFoundObjectResult(baseResult);
            }
            baseResult.data.user = user;
            baseResult.data.token = Generate_Token(user.id.ToString());
            return new JsonResult(baseResult);
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
            baseResult.data = _SGithub.Check_Github_User(githubItem.username).Item1;
            if (!baseResult.data)
            {
                baseResult.statusCode = HttpStatusCode.NotFound;
                baseResult.message = _SMethod.Get_Enum_Description(enumErrorMessage.githubUserNotFound);
                return new NotFoundObjectResult(baseResult);
            }
            return new JsonResult(baseResult.data);
        }
        /// <summary>
        /// Kayıt Olmak İçin Kullanılır
        /// </summary>
        /// <param name="registerItem"></param>
        /// <returns></returns>
        [HttpPost("register")]
        [AllowAnonymous]
        public IActionResult Register([FromBody]RequestRegister registerItem)
        {
            BaseResult<long> baseResult = new BaseResult<long>();
            if (!Validate<RequestRegisterValidaitor>(registerItem))
            {
                baseResult.statusCode = HttpStatusCode.NotFound;
                baseResult.message = _SMethod.Get_Enum_Description(enumErrorMessage.invalidModel);
                return new NotFoundObjectResult(baseResult);
            }
            bool checkGithubUser = _SGithub.Check_Github_User(registerItem.github_username).Item1;
            if (!checkGithubUser)
            {
                baseResult.statusCode = HttpStatusCode.NotFound;
                baseResult.message = _SMethod.Get_Enum_Description(enumErrorMessage.githubUserNotFound);
                return new NotFoundObjectResult(baseResult);
            }
            User user= _SUser.Check_User(registerItem.email,registerItem.github_username);
            if (user != null)
            {
                baseResult.statusCode = HttpStatusCode.NotFound;
                baseResult.message = _SMethod.Get_Enum_Description(enumErrorMessage.duplicatedUser);
                return new NotFoundObjectResult(baseResult);
            }

            baseResult.data = _SUser.Insert_User(new User
            { 
                github_username = registerItem.github_username,
                email = registerItem.email,
                password = _SMethod.Generate_MD5(registerItem.password),
                default_lang = "en",
                creation_date = DateTime.Now,
                creator_id = 0,
                status_id = (int)enumStatus.Aktif
            });
            if (baseResult.data<1)
            {
                baseResult.statusCode = HttpStatusCode.NotFound;
                baseResult.message = _SMethod.Get_Enum_Description(enumErrorMessage.unSuccessful);
                return new NotFoundObjectResult(baseResult);
            }
            return new JsonResult(baseResult);
        }
        /// <summary>
        /// Varsayılan Dili Değiştirmek İçin Kullanılır.
        /// </summary>
        /// <param name="languageItem"></param>
        /// <returns></returns>
        [HttpPost("changelanguage")]
        public IActionResult Change_Language([FromBody]RequestChangeLanguage languageItem)
        {
            BaseResult<bool> baseResult = new BaseResult<bool>();
            if (!Validate<RequestChangeLanguageValidator>(languageItem))
            {
                baseResult.statusCode = HttpStatusCode.NotFound;
                baseResult.message = _SMethod.Get_Enum_Description(enumErrorMessage.invalidModel);
                return new NotFoundObjectResult(baseResult);
            }
            User user = _SUser.Get_By_Id(GetUserId());
            user.default_lang = languageItem.locale;
            baseResult.data = _SUser.Update_User(user);
            if (!baseResult.data)
            {
                baseResult.statusCode = HttpStatusCode.NotFound;
                baseResult.message = _SMethod.Get_Enum_Description(enumErrorMessage.unSuccessful);
                return new NotFoundObjectResult(baseResult);
            }
            return new JsonResult(baseResult);
        }
        /// <summary>
        /// Giriş Yapmış Kullanıcıya Ait Bilgileri Geriye Döndürür.
        /// </summary>
        /// <returns></returns>
        [HttpGet("getuser")]
        public async Task<IActionResult> Get_User()
        {
            BaseResult<ResponseGetUser> baseResult = new BaseResult<ResponseGetUser>();
            User user = _SUser.Get_By_Id(GetUserId());
            baseResult.data.user = await _SGithub.Get_Github_User(user,_SRedisService);
            baseResult.data.followers = await _SGithub.Get_Github_Followers(user, _SRedisService);
            baseResult.data.followings = await _SGithub.Get_Github_Following(user, _SRedisService);
            baseResult.data.starredRepositories = await _SGithub.Get_Github_Starred_Repository(user,_SRedisService);
            baseResult.data.repositories = await _SGithub.Get_Github_Repository(user,_SRedisService);
            return new JsonResult(baseResult);
        }

        #region JWT
        [NonAction]
        private string Generate_Token(string userId)
        {
            var someClaims = new Claim[]{
                new Claim(JwtRegisteredClaimNames.UniqueName,userId),
                new Claim(JwtRegisteredClaimNames.Email, userId),
                new Claim(JwtRegisteredClaimNames.NameId,Guid.NewGuid().ToString()),
            };
            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("csharpdependencyauth"));
            var token = new JwtSecurityToken(
                claims: someClaims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion
    }
}