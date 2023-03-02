
using Business.Handlers.OrNisans.Commands;
using FluentValidation;

namespace Business.Handlers.OrNisans.ValidationRules
{

    public class CreateOrNisanValidator : AbstractValidator<CreateOrNisanCommand>
    {
        public CreateOrNisanValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();
            RuleFor(x => x.Detay).NotEmpty();

        }
    }
    public class UpdateOrNisanValidator : AbstractValidator<UpdateOrNisanCommand>
    {
        public UpdateOrNisanValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();
            RuleFor(x => x.Detay).NotEmpty();

        }
    }
}