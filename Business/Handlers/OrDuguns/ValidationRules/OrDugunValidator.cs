
using Business.Handlers.OrDuguns.Commands;
using FluentValidation;

namespace Business.Handlers.OrDuguns.ValidationRules
{

    public class CreateOrDugunValidator : AbstractValidator<CreateOrDugunCommand>
    {
        public CreateOrDugunValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();
            RuleFor(x => x.Detay).NotEmpty();

        }
    }
    public class UpdateOrDugunValidator : AbstractValidator<UpdateOrDugunCommand>
    {
        public UpdateOrDugunValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();
            RuleFor(x => x.Detay).NotEmpty();

        }
    }
}