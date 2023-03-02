
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

namespace Business.Handlers.OrCaterings.Queries
{

    public class GetOrCateringsQuery : IRequest<IDataResult<IEnumerable<OrCatering>>>
    {
        public class GetOrCateringsQueryHandler : IRequestHandler<GetOrCateringsQuery, IDataResult<IEnumerable<OrCatering>>>
        {
            private readonly IOrCateringRepository _orCateringRepository;
            private readonly IMediator _mediator;

            public GetOrCateringsQueryHandler(IOrCateringRepository orCateringRepository, IMediator mediator)
            {
                _orCateringRepository = orCateringRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<OrCatering>>> Handle(GetOrCateringsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<OrCatering>>(await _orCateringRepository.GetListAsync());
            }
        }
    }
}