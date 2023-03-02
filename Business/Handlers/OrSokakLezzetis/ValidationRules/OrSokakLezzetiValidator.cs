
using Business.Handlers.OrSokakLezzetis.Commands;
using FluentValidation;

namespace Business.Handlers.OrSokakLezzetis.ValidationRules
{

    public class CreateOrSokakLezzetiValidator : AbstractValidator<CreateOrSokakLezzetiCommand>
    {
        public CreateOrSokakLezzetiValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();
            RuleFor(x => x.Detay).NotEmpty();

        }
    }
    public class UpdateOrSokakLezzetiValidator : AbstractValidator<UpdateOrSokakLezzetiCommand>
    {
        public UpdateOrSokakLezzetiValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();
            RuleFor(x => x.Detay).NotEmpty();

        }
    }
}