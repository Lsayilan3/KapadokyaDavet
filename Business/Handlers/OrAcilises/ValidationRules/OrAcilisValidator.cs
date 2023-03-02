
using Business.Handlers.OrAcilises.Commands;
using FluentValidation;

namespace Business.Handlers.OrAcilises.ValidationRules
{

    public class CreateOrAcilisValidator : AbstractValidator<CreateOrAcilisCommand>
    {
        public CreateOrAcilisValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();
            RuleFor(x => x.Detay).NotEmpty();

        }
    }
    public class UpdateOrAcilisValidator : AbstractValidator<UpdateOrAcilisCommand>
    {
        public UpdateOrAcilisValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();
            RuleFor(x => x.Detay).NotEmpty();

        }
    }
}