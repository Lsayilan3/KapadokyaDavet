
using Business.Handlers.OrCaterings.Commands;
using FluentValidation;

namespace Business.Handlers.OrCaterings.ValidationRules
{

    public class CreateOrCateringValidator : AbstractValidator<CreateOrCateringCommand>
    {
        public CreateOrCateringValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();
            RuleFor(x => x.Detay).NotEmpty();

        }
    }
    public class UpdateOrCateringValidator : AbstractValidator<UpdateOrCateringCommand>
    {
        public UpdateOrCateringValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();
            RuleFor(x => x.Detay).NotEmpty();

        }
    }
}