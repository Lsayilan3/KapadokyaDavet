
using Business.Handlers.Spots.Commands;
using FluentValidation;

namespace Business.Handlers.Spots.ValidationRules
{

    public class CreateSpotValidator : AbstractValidator<CreateSpotCommand>
    {
        public CreateSpotValidator()
        {
            RuleFor(x => x.CategoryId).NotEmpty();
            RuleFor(x => x.Photo).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Tag).NotEmpty();
            RuleFor(x => x.Price).NotEmpty();
            RuleFor(x => x.DiscountPrice).NotEmpty();

        }
    }
    public class UpdateSpotValidator : AbstractValidator<UpdateSpotCommand>
    {
        public UpdateSpotValidator()
        {
            RuleFor(x => x.CategoryId).NotEmpty();
            RuleFor(x => x.Photo).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Tag).NotEmpty();
            RuleFor(x => x.Price).NotEmpty();
            RuleFor(x => x.DiscountPrice).NotEmpty();

        }
    }
}