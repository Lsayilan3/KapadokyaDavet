
using Business.Handlers.Muziks.Commands;
using FluentValidation;

namespace Business.Handlers.Muziks.ValidationRules
{

    public class CreateMuzikValidator : AbstractValidator<CreateMuzikCommand>
    {
        public CreateMuzikValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Tag).NotEmpty();
            RuleFor(x => x.Detay).NotEmpty();

        }
    }
    public class UpdateMuzikValidator : AbstractValidator<UpdateMuzikCommand>
    {
        public UpdateMuzikValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Tag).NotEmpty();
            RuleFor(x => x.Detay).NotEmpty();

        }
    }
}