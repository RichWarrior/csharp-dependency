using csharp.dependency.core.CustomEntity.Request.User;
using FluentValidation;

namespace csharp.dependency.core.Validation.User
{
    public class RequestChangeLanguageValidator : AbstractValidator<RequestChangeLanguage>
    {
        public RequestChangeLanguageValidator()
        {
            RuleFor(x => x.locale).NotEmpty().MaximumLength(255);
        }
    }
}
