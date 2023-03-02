
using Business.Handlers.Organizasyons.Commands;
using FluentValidation;

namespace Business.Handlers.Organizasyons.ValidationRules
{

    public class CreateOrganizasyonValidator : AbstractValidator<CreateOrganizasyonCommand>
    {
        public CreateOrganizasyonValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Tag).NotEmpty();
            RuleFor(x => x.Detay).NotEmpty();

        }
    }
    public class UpdateOrganizasyonValidator : AbstractValidator<UpdateOrganizasyonCommand>
    {
        public UpdateOrganizasyonValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Tag).NotEmpty();
            RuleFor(x => x.Detay).NotEmpty();

        }
    }
}