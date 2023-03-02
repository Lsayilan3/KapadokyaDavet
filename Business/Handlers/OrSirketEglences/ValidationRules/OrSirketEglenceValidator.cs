
using Business.Handlers.OrSirketEglences.Commands;
using FluentValidation;

namespace Business.Handlers.OrSirketEglences.ValidationRules
{

    public class CreateOrSirketEglenceValidator : AbstractValidator<CreateOrSirketEglenceCommand>
    {
        public CreateOrSirketEglenceValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();
            RuleFor(x => x.Detay).NotEmpty();

        }
    }
    public class UpdateOrSirketEglenceValidator : AbstractValidator<UpdateOrSirketEglenceCommand>
    {
        public UpdateOrSirketEglenceValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();
            RuleFor(x => x.Detay).NotEmpty();

        }
    }
}