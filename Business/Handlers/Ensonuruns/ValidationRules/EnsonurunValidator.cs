
using Business.Handlers.Ensonuruns.Commands;
using FluentValidation;

namespace Business.Handlers.Ensonuruns.ValidationRules
{

    public class CreateEnsonurunValidator : AbstractValidator<CreateEnsonurunCommand>
    {
        public CreateEnsonurunValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Url).NotEmpty();

        }
    }
    public class UpdateEnsonurunValidator : AbstractValidator<UpdateEnsonurunCommand>
    {
        public UpdateEnsonurunValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Url).NotEmpty();

        }
    }
}