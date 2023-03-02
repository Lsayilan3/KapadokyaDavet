
using Business.Handlers.OrSunnets.Commands;
using FluentValidation;

namespace Business.Handlers.OrSunnets.ValidationRules
{

    public class CreateOrSunnetValidator : AbstractValidator<CreateOrSunnetCommand>
    {
        public CreateOrSunnetValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();
            RuleFor(x => x.Detay).NotEmpty();

        }
    }
    public class UpdateOrSunnetValidator : AbstractValidator<UpdateOrSunnetCommand>
    {
        public UpdateOrSunnetValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();
            RuleFor(x => x.Detay).NotEmpty();

        }
    }
}