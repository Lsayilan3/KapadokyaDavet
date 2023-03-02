
using Business.Handlers.OrPikniks.Commands;
using FluentValidation;

namespace Business.Handlers.OrPikniks.ValidationRules
{

    public class CreateOrPiknikValidator : AbstractValidator<CreateOrPiknikCommand>
    {
        public CreateOrPiknikValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();
            RuleFor(x => x.Detay).NotEmpty();

        }
    }
    public class UpdateOrPiknikValidator : AbstractValidator<UpdateOrPiknikCommand>
    {
        public UpdateOrPiknikValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();
            RuleFor(x => x.Detay).NotEmpty();

        }
    }
}