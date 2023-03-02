
using Business.Handlers.Ensatans.Commands;
using FluentValidation;

namespace Business.Handlers.Ensatans.ValidationRules
{

    public class CreateEnsatanValidator : AbstractValidator<CreateEnsatanCommand>
    {
        public CreateEnsatanValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Url).NotEmpty();

        }
    }
    public class UpdateEnsatanValidator : AbstractValidator<UpdateEnsatanCommand>
    {
        public UpdateEnsatanValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Url).NotEmpty();

        }
    }
}