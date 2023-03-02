
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace Business.Handlers.GalleryTwoes.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteGalleryTwoCommand : IRequest<IResult>
    {
        public int GalleryTwoId { get; set; }

        public class DeleteGalleryTwoCommandHandler : IRequestHandler<DeleteGalleryTwoCommand, IResult>
        {
            private readonly IGalleryTwoRepository _galleryTwoRepository;
            private readonly IMediator _mediator;

            public DeleteGalleryTwoCommandHandler(IGalleryTwoRepository galleryTwoRepository, IMediator mediator)
            {
                _galleryTwoRepository = galleryTwoRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteGalleryTwoCommand request, CancellationToken cancellationToken)
            {
                var galleryTwoToDelete = _galleryTwoRepository.Get(p => p.GalleryTwoId == request.GalleryTwoId);

                _galleryTwoRepository.Delete(galleryTwoToDelete);
                await _galleryTwoRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

