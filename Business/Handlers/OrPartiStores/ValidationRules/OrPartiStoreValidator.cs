
using Business.Handlers.OrPartiStores.Commands;
using FluentValidation;

namespace Business.Handlers.OrPartiStores.ValidationRules
{

    public class CreateOrPartiStoreValidator : AbstractValidator<CreateOrPartiStoreCommand>
    {
        public CreateOrPartiStoreValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();
            RuleFor(x => x.Detay).NotEmpty();

        }
    }
    public class UpdateOrPartiStoreValidator : AbstractValidator<UpdateOrPartiStoreCommand>
    {
        public UpdateOrPartiStoreValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();
            RuleFor(x => x.Detay).NotEmpty();

        }
    }
}