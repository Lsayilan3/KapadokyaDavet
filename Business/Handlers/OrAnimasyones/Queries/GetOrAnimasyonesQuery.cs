
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

namespace Business.Handlers.OrAnimasyones.Queries
{

    public class GetOrAnimasyonesQuery : IRequest<IDataResult<IEnumerable<OrAnimasyone>>>
    {
        public class GetOrAnimasyonesQueryHandler : IRequestHandler<GetOrAnimasyonesQuery, IDataResult<IEnumerable<OrAnimasyone>>>
        {
            private readonly IOrAnimasyoneRepository _orAnimasyoneRepository;
            private readonly IMediator _mediator;

            public GetOrAnimasyonesQueryHandler(IOrAnimasyoneRepository orAnimasyoneRepository, IMediator mediator)
            {
                _orAnimasyoneRepository = orAnimasyoneRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<OrAnimasyone>>> Handle(GetOrAnimasyonesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<OrAnimasyone>>(await _orAnimasyoneRepository.GetListAsync());
            }
        }
    }
}