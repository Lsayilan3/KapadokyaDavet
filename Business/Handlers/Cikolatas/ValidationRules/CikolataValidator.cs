
using Business.Handlers.Cikolatas.Commands;
using FluentValidation;

namespace Business.Handlers.Cikolatas.ValidationRules
{

    public class CreateCikolataValidator : AbstractValidator<CreateCikolataCommand>
    {
        public CreateCikolataValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Tag).NotEmpty();
            RuleFor(x => x.Detay).NotEmpty();

        }
    }
    public class UpdateCikolataValidator : AbstractValidator<UpdateCikolataCommand>
    {
        public UpdateCikolataValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Tag).NotEmpty();
            RuleFor(x => x.Detay).NotEmpty();

        }
    }
}