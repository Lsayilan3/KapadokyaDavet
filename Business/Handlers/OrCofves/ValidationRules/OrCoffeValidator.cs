
using Business.Handlers.OrCofves.Commands;
using FluentValidation;

namespace Business.Handlers.OrCofves.ValidationRules
{

    public class CreateOrCoffeValidator : AbstractValidator<CreateOrCoffeCommand>
    {
        public CreateOrCoffeValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();
            RuleFor(x => x.Detay).NotEmpty();

        }
    }
    public class UpdateOrCoffeValidator : AbstractValidator<UpdateOrCoffeCommand>
    {
        public UpdateOrCoffeValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();
            RuleFor(x => x.Detay).NotEmpty();

        }
    }
}