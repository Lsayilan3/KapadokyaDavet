
using Business.Handlers.OrAnimasyones.Commands;
using FluentValidation;

namespace Business.Handlers.OrAnimasyones.ValidationRules
{

    public class CreateOrAnimasyoneValidator : AbstractValidator<CreateOrAnimasyoneCommand>
    {
        public CreateOrAnimasyoneValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();
            RuleFor(x => x.Detay).NotEmpty();

        }
    }
    public class UpdateOrAnimasyoneValidator : AbstractValidator<UpdateOrAnimasyoneCommand>
    {
        public UpdateOrAnimasyoneValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();
            RuleFor(x => x.Detay).NotEmpty();

        }
    }
}