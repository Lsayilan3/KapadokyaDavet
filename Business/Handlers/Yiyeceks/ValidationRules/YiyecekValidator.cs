
using Business.Handlers.Yiyeceks.Commands;
using FluentValidation;

namespace Business.Handlers.Yiyeceks.ValidationRules
{

    public class CreateYiyecekValidator : AbstractValidator<CreateYiyecekCommand>
    {
        public CreateYiyecekValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Tag).NotEmpty();
            RuleFor(x => x.Price).NotEmpty();
            RuleFor(x => x.DiscountPrice).NotEmpty();

        }
    }
    public class UpdateYiyecekValidator : AbstractValidator<UpdateYiyecekCommand>
    {
        public UpdateYiyecekValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Tag).NotEmpty();
            RuleFor(x => x.Price).NotEmpty();
            RuleFor(x => x.DiscountPrice).NotEmpty();

        }
    }
}