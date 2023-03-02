
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

namespace Business.Handlers.OrKinaas.Queries
{

    public class GetOrKinaasQuery : IRequest<IDataResult<IEnumerable<OrKinaa>>>
    {
        public class GetOrKinaasQueryHandler : IRequestHandler<GetOrKinaasQuery, IDataResult<IEnumerable<OrKinaa>>>
        {
            private readonly IOrKinaaRepository _orKinaaRepository;
            private readonly IMediator _mediator;

            public GetOrKinaasQueryHandler(IOrKinaaRepository orKinaaRepository, IMediator mediator)
            {
                _orKinaaRepository = orKinaaRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<OrKinaa>>> Handle(GetOrKinaasQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<OrKinaa>>(await _orKinaaRepository.GetListAsync());
            }
        }
    }
}