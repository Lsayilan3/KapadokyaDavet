
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

namespace Business.Handlers.SpotCategoryies.Queries
{

    public class GetSpotCategoryiesQuery : IRequest<IDataResult<IEnumerable<SpotCategoryy>>>
    {
        public class GetSpotCategoryiesQueryHandler : IRequestHandler<GetSpotCategoryiesQuery, IDataResult<IEnumerable<SpotCategoryy>>>
        {
            private readonly ISpotCategoryyRepository _spotCategoryyRepository;
            private readonly IMediator _mediator;

            public GetSpotCategoryiesQueryHandler(ISpotCategoryyRepository spotCategoryyRepository, IMediator mediator)
            {
                _spotCategoryyRepository = spotCategoryyRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<SpotCategoryy>>> Handle(GetSpotCategoryiesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<SpotCategoryy>>(await _spotCategoryyRepository.GetListAsync());
            }
        }
    }
}