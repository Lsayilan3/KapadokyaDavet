
using Business.Handlers.Blogs.Commands;
using FluentValidation;

namespace Business.Handlers.Blogs.ValidationRules
{

    public class CreateBlogValidator : AbstractValidator<CreateBlogCommand>
    {
        public CreateBlogValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Text).NotEmpty();
            RuleFor(x => x.PostDate).NotEmpty();
            RuleFor(x => x.Author).NotEmpty();

        }
    }
    public class UpdateBlogValidator : AbstractValidator<UpdateBlogCommand>
    {
        public UpdateBlogValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Text).NotEmpty();
            RuleFor(x => x.PostDate).NotEmpty();
            RuleFor(x => x.Author).NotEmpty();

        }
    }
}