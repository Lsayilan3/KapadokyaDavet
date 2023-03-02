
using Business.Handlers.OrCikolatas.Commands;
using FluentValidation;

namespace Business.Handlers.OrCikolatas.ValidationRules
{

    public class CreateOrCikolataValidator : AbstractValidator<CreateOrCikolataCommand>
    {
        public CreateOrCikolataValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Tag).NotEmpty();
            RuleFor(x => x.Detay).NotEmpty();

        }
    }
    public class UpdateOrCikolataValidator : AbstractValidator<UpdateOrCikolataCommand>
    {
        public UpdateOrCikolataValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Tag).NotEmpty();
            RuleFor(x => x.Detay).NotEmpty();

        }
    }
}