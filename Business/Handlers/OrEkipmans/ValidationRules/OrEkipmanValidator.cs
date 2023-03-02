
using Business.Handlers.OrEkipmans.Commands;
using FluentValidation;

namespace Business.Handlers.OrEkipmans.ValidationRules
{

    public class CreateOrEkipmanValidator : AbstractValidator<CreateOrEkipmanCommand>
    {
        public CreateOrEkipmanValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();
            RuleFor(x => x.Detay).NotEmpty();

        }
    }
    public class UpdateOrEkipmanValidator : AbstractValidator<UpdateOrEkipmanCommand>
    {
        public UpdateOrEkipmanValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();
            RuleFor(x => x.Detay).NotEmpty();

        }
    }
}