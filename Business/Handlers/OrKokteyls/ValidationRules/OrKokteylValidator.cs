
using Business.Handlers.OrKokteyls.Commands;
using FluentValidation;

namespace Business.Handlers.OrKokteyls.ValidationRules
{

    public class CreateOrKokteylValidator : AbstractValidator<CreateOrKokteylCommand>
    {
        public CreateOrKokteylValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();
            RuleFor(x => x.Detay).NotEmpty();

        }
    }
    public class UpdateOrKokteylValidator : AbstractValidator<UpdateOrKokteylCommand>
    {
        public UpdateOrKokteylValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();
            RuleFor(x => x.Detay).NotEmpty();

        }
    }
}