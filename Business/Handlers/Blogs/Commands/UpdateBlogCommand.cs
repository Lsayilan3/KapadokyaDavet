
using Business.Constants;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Core.Aspects.Autofac.Validation;
using Business.Handlers.Blogs.ValidationRules;


namespace Business.Handlers.Blogs.Commands
{


    public class UpdateBlogCommand : IRequest<IResult>
    {
        public int BlogId { get; set; }
        public string Photo { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string PostDate { get; set; }
        public string Author { get; set; }

        public class UpdateBlogCommandHandler : IRequestHandler<UpdateBlogCommand, IResult>
        {
            private readonly IBlogRepository _blogRepository;
            private readonly IMediator _mediator;

            public UpdateBlogCommandHandler(IBlogRepository blogRepository, IMediator mediator)
            {
                _blogRepository = blogRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateBlogValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateBlogCommand request, CancellationToken cancellationToken)
            {
                var isThereBlogRecord = await _blogRepository.GetAsync(u => u.BlogId == request.BlogId);


                isThereBlogRecord.Photo = request.Photo;
                isThereBlogRecord.Title = request.Title;
                isThereBlogRecord.Text = request.Text;
                isThereBlogRecord.PostDate = request.PostDate;
                isThereBlogRecord.Author = request.Author;


                _blogRepository.Update(isThereBlogRecord);
                await _blogRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

