using csharp.dependency.core.Interface;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System;

namespace csharp.dependency.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public IMethod _SMethod;

        public BaseController(IMethod _SMethod)
        {
            this._SMethod = _SMethod;
        }

        [NonAction]
        public bool Validate<Validator>(object model)
            where Validator : class, new()
        {
            Validator validator = (Validator)Activator.CreateInstance(typeof(Validator));
            return ((IValidator)validator).Validate(model).IsValid;
        }

        [NonAction]
        public long GetUserId()
        {
            return Convert.ToInt64(HttpContext.User.Identity.Name);
        }
    }
}