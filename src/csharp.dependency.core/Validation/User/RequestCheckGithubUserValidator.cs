using csharp.dependency.core.CustomEntity.Request.User;
using FluentValidation;

namespace csharp.dependency.core.Validation.User
{
    public class RequestCheckGithubUserValidator : AbstractValidator<RequestCheckGithubUser>
    {
        public RequestCheckGithubUserValidator()
        {
            RuleFor(x => x.username).NotEmpty().MaximumLength(255);
        }
    }
}
