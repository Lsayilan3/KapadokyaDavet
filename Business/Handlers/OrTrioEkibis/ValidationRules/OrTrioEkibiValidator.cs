
using Business.Handlers.OrTrioEkibis.Commands;
using FluentValidation;

namespace Business.Handlers.OrTrioEkibis.ValidationRules
{

    public class CreateOrTrioEkibiValidator : AbstractValidator<CreateOrTrioEkibiCommand>
    {
        public CreateOrTrioEkibiValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();
            RuleFor(x => x.Detay).NotEmpty();

        }
    }
    public class UpdateOrTrioEkibiValidator : AbstractValidator<UpdateOrTrioEkibiCommand>
    {
        public UpdateOrTrioEkibiValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();
            RuleFor(x => x.Detay).NotEmpty();

        }
    }
}