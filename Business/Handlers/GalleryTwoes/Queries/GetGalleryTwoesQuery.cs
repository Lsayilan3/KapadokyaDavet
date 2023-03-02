
using Business.BusinessAspects;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Caching;

namespace Business.Handlers.GalleryTwoes.Queries
{

    public class GetGalleryTwoesQuery : IRequest<IDataResult<IEnumerable<GalleryTwo>>>
    {
        public class GetGalleryTwoesQueryHandler : IRequestHandler<GetGalleryTwoesQuery, IDataResult<IEnumerable<GalleryTwo>>>
        {
            private readonly IGalleryTwoRepository _galleryTwoRepository;
            private readonly IMediator _mediator;

            public GetGalleryTwoesQueryHandler(IGalleryTwoRepository galleryTwoRepository, IMediator mediator)
            {
                _galleryTwoRepository = galleryTwoRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<GalleryTwo>>> Handle(GetGalleryTwoesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<GalleryTwo>>(await _galleryTwoRepository.GetListAsync());
            }
        }
    }
}