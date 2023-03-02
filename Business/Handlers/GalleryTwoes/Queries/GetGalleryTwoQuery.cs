
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.GalleryTwoes.Queries
{
    public class GetGalleryTwoQuery : IRequest<IDataResult<GalleryTwo>>
    {
        public int GalleryTwoId { get; set; }

        public class GetGalleryTwoQueryHandler : IRequestHandler<GetGalleryTwoQuery, IDataResult<GalleryTwo>>
        {
            private readonly IGalleryTwoRepository _galleryTwoRepository;
            private readonly IMediator _mediator;

            public GetGalleryTwoQueryHandler(IGalleryTwoRepository galleryTwoRepository, IMediator mediator)
            {
                _galleryTwoRepository = galleryTwoRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<GalleryTwo>> Handle(GetGalleryTwoQuery request, CancellationToken cancellationToken)
            {
                var galleryTwo = await _galleryTwoRepository.GetAsync(p => p.GalleryTwoId == request.GalleryTwoId);
                return new SuccessDataResult<GalleryTwo>(galleryTwo);
            }
        }
    }
}
