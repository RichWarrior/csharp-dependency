using csharp.dependency.core.CustomEntity.Request.User;
using FluentValidation;

namespace csharp.dependency.core.Validation.User
{
    public class RequestRegisterValidaitor : AbstractValidator<RequestRegister>
    {
        public RequestRegisterValidaitor()
        {
            RuleFor(x => x.github_username).NotEmpty().MaximumLength(255);
            RuleFor(x => x.email).NotEmpty().EmailAddress().MaximumLength(255);
            RuleFor(x => x.password).NotEmpty().MaximumLength(255);
        }
    }
}
