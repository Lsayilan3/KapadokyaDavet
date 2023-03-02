
using Business.Handlers.GalleryTwoes.Commands;
using FluentValidation;

namespace Business.Handlers.GalleryTwoes.ValidationRules
{

    public class CreateGalleryTwoValidator : AbstractValidator<CreateGalleryTwoCommand>
    {
        public CreateGalleryTwoValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();

        }
    }
    public class UpdateGalleryTwoValidator : AbstractValidator<UpdateGalleryTwoCommand>
    {
        public UpdateGalleryTwoValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();

        }
    }
}