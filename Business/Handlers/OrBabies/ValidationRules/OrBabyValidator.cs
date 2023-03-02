
using Business.Handlers.OrBabies.Commands;
using FluentValidation;

namespace Business.Handlers.OrBabies.ValidationRules
{

    public class CreateOrBabyValidator : AbstractValidator<CreateOrBabyCommand>
    {
        public CreateOrBabyValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();
            RuleFor(x => x.Detay).NotEmpty();

        }
    }
    public class UpdateOrBabyValidator : AbstractValidator<UpdateOrBabyCommand>
    {
        public UpdateOrBabyValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();
            RuleFor(x => x.Detay).NotEmpty();

        }
    }
}