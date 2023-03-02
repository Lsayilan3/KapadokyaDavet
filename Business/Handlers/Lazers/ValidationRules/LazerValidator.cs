
using Business.Handlers.Lazers.Commands;
using FluentValidation;

namespace Business.Handlers.Lazers.ValidationRules
{

    public class CreateLazerValidator : AbstractValidator<CreateLazerCommand>
    {
        public CreateLazerValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Tag).NotEmpty();
            RuleFor(x => x.Price).NotEmpty();
            RuleFor(x => x.DiscountPrice).NotEmpty();

        }
    }
    public class UpdateLazerValidator : AbstractValidator<UpdateLazerCommand>
    {
        public UpdateLazerValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Tag).NotEmpty();
            RuleFor(x => x.Price).NotEmpty();
            RuleFor(x => x.DiscountPrice).NotEmpty();

        }
    }
}