
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

namespace Business.Handlers.OrDuguns.Queries
{

    public class GetOrDugunsQuery : IRequest<IDataResult<IEnumerable<OrDugun>>>
    {
        public class GetOrDugunsQueryHandler : IRequestHandler<GetOrDugunsQuery, IDataResult<IEnumerable<OrDugun>>>
        {
            private readonly IOrDugunRepository _orDugunRepository;
            private readonly IMediator _mediator;

            public GetOrDugunsQueryHandler(IOrDugunRepository orDugunRepository, IMediator mediator)
            {
                _orDugunRepository = orDugunRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<OrDugun>>> Handle(GetOrDugunsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<OrDugun>>(await _orDugunRepository.GetListAsync());
            }
        }
    }
}