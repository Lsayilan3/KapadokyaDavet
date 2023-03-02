
using Business.Handlers.Partis.Commands;
using FluentValidation;

namespace Business.Handlers.Partis.ValidationRules
{

    public class CreatePartiValidator : AbstractValidator<CreatePartiCommand>
    {
        public CreatePartiValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Tag).NotEmpty();
            RuleFor(x => x.Price).NotEmpty();
            RuleFor(x => x.DiscountPrice).NotEmpty();

        }
    }
    public class UpdatePartiValidator : AbstractValidator<UpdatePartiCommand>
    {
        public UpdatePartiValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Tag).NotEmpty();
            RuleFor(x => x.Price).NotEmpty();
            RuleFor(x => x.DiscountPrice).NotEmpty();

        }
    }
}