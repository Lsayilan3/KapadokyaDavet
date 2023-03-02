
using Business.Handlers.SpotCategoryies.Commands;
using FluentValidation;

namespace Business.Handlers.SpotCategoryies.ValidationRules
{

    public class CreateSpotCategoryyValidator : AbstractValidator<CreateSpotCategoryyCommand>
    {
        public CreateSpotCategoryyValidator()
        {
            RuleFor(x => x.CategoryId).NotEmpty();
            RuleFor(x => x.CategoryName).NotEmpty();

        }
    }
    public class UpdateSpotCategoryyValidator : AbstractValidator<UpdateSpotCategoryyCommand>
    {
        public UpdateSpotCategoryyValidator()
        {
            RuleFor(x => x.CategoryId).NotEmpty();
            RuleFor(x => x.CategoryName).NotEmpty();

        }
    }
}