using csharp.dependency.core.CustomEntity.Request.User;
using FluentValidation;

namespace csharp.dependency.core.Validation.User
{
    public class RequestLoginValidator : AbstractValidator<RequestLogin>
    {
        public RequestLoginValidator()
        {
            RuleFor(x => x.email).NotEmpty().MaximumLength(255).EmailAddress();
            RuleFor(x => x.password).NotEmpty().MaximumLength(255);
        }
    }
}
