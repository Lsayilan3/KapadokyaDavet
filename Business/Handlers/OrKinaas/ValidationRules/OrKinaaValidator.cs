
using Business.Handlers.OrKinaas.Commands;
using FluentValidation;

namespace Business.Handlers.OrKinaas.ValidationRules
{

    public class CreateOrKinaaValidator : AbstractValidator<CreateOrKinaaCommand>
    {
        public CreateOrKinaaValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();
            RuleFor(x => x.Detay).NotEmpty();

        }
    }
    public class UpdateOrKinaaValidator : AbstractValidator<UpdateOrKinaaCommand>
    {
        public UpdateOrKinaaValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();
            RuleFor(x => x.Detay).NotEmpty();

        }
    }
}