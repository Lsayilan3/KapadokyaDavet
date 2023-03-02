
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Business.Handlers.Blogs.ValidationRules;

namespace Business.Handlers.Blogs.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateBlogCommand : IRequest<IResult>
    {

        public string Photo { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string PostDate { get; set; }
        public string Author { get; set; }


        public class CreateBlogCommandHandler : IRequestHandler<CreateBlogCommand, IResult>
        {
            private readonly IBlogRepository _blogRepository;
            private readonly IMediator _mediator;
            public CreateBlogCommandHandler(IBlogRepository blogRepository, IMediator mediator)
            {
                _blogRepository = blogRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateBlogValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
            {
                //var isThereBlogRecord = _blogRepository.Query().Any(u => u.Photo == request.Photo);

                //if (isThereBlogRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedBlog = new Blog
                {
                    Photo = request.Photo,
                    Title = request.Title,
                    Text = request.Text,
                    PostDate = request.PostDate,
                    Author = request.Author,

                };

                _blogRepository.Add(addedBlog);
                await _blogRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}