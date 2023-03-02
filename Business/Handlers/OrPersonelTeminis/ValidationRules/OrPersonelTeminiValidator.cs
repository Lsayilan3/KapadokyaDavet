
using Business.Handlers.OrPersonelTeminis.Commands;
using FluentValidation;

namespace Business.Handlers.OrPersonelTeminis.ValidationRules
{

    public class CreateOrPersonelTeminiValidator : AbstractValidator<CreateOrPersonelTeminiCommand>
    {
        public CreateOrPersonelTeminiValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();
            RuleFor(x => x.Detay).NotEmpty();

        }
    }
    public class UpdateOrPersonelTeminiValidator : AbstractValidator<UpdateOrPersonelTeminiCommand>
    {
        public UpdateOrPersonelTeminiValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();
            RuleFor(x => x.Detay).NotEmpty();

        }
    }
}