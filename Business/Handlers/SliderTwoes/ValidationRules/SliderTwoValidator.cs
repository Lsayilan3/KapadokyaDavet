
using Business.Handlers.SliderTwoes.Commands;
using FluentValidation;

namespace Business.Handlers.SliderTwoes.ValidationRules
{

    public class CreateSliderTwoValidator : AbstractValidator<CreateSliderTwoCommand>
    {
        public CreateSliderTwoValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Detay).NotEmpty();
            RuleFor(x => x.Price).NotEmpty();
            RuleFor(x => x.DiscountPrice).NotEmpty();
            RuleFor(x => x.Photo).NotEmpty();

        }
    }
    public class UpdateSliderTwoValidator : AbstractValidator<UpdateSliderTwoCommand>
    {
        public UpdateSliderTwoValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Detay).NotEmpty();
            RuleFor(x => x.Price).NotEmpty();
            RuleFor(x => x.DiscountPrice).NotEmpty();
            RuleFor(x => x.Photo).NotEmpty();

        }
    }
}