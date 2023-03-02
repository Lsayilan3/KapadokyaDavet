
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

namespace Business.Handlers.Spots.Queries
{

    public class GetSpotsQuery : IRequest<IDataResult<IEnumerable<Spot>>>
    {
        public class GetSpotsQueryHandler : IRequestHandler<GetSpotsQuery, IDataResult<IEnumerable<Spot>>>
        {
            private readonly ISpotRepository _spotRepository;
            private readonly IMediator _mediator;

            public GetSpotsQueryHandler(ISpotRepository spotRepository, IMediator mediator)
            {
                _spotRepository = spotRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Spot>>> Handle(GetSpotsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Spot>>(await _spotRepository.GetListAsync());
            }
        }
    }
}