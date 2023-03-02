
using Business.Handlers.OrPartiEglences.Commands;
using FluentValidation;

namespace Business.Handlers.OrPartiEglences.ValidationRules
{

    public class CreateOrPartiEglenceValidator : AbstractValidator<CreateOrPartiEglenceCommand>
    {
        public CreateOrPartiEglenceValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();
            RuleFor(x => x.Detay).NotEmpty();

        }
    }
    public class UpdateOrPartiEglenceValidator : AbstractValidator<UpdateOrPartiEglenceCommand>
    {
        public UpdateOrPartiEglenceValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();
            RuleFor(x => x.Detay).NotEmpty();

        }
    }
}