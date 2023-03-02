
using Business.Handlers.Hediyeliks.Commands;
using FluentValidation;

namespace Business.Handlers.Hediyeliks.ValidationRules
{

    public class CreateHediyelikValidator : AbstractValidator<CreateHediyelikCommand>
    {
        public CreateHediyelikValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Tag).NotEmpty();
            RuleFor(x => x.Price).NotEmpty();
            RuleFor(x => x.DiscountPrice).NotEmpty();

        }
    }
    public class UpdateHediyelikValidator : AbstractValidator<UpdateHediyelikCommand>
    {
        public UpdateHediyelikValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Tag).NotEmpty();
            RuleFor(x => x.Price).NotEmpty();
            RuleFor(x => x.DiscountPrice).NotEmpty();

        }
    }
}