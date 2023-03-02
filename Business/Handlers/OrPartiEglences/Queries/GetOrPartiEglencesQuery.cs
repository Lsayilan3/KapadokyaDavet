
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

namespace Business.Handlers.OrPartiEglences.Queries
{

    public class GetOrPartiEglencesQuery : IRequest<IDataResult<IEnumerable<OrPartiEglence>>>
    {
        public class GetOrPartiEglencesQueryHandler : IRequestHandler<GetOrPartiEglencesQuery, IDataResult<IEnumerable<OrPartiEglence>>>
        {
            private readonly IOrPartiEglenceRepository _orPartiEglenceRepository;
            private readonly IMediator _mediator;

            public GetOrPartiEglencesQueryHandler(IOrPartiEglenceRepository orPartiEglenceRepository, IMediator mediator)
            {
                _orPartiEglenceRepository = orPartiEglenceRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<OrPartiEglence>>> Handle(GetOrPartiEglencesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<OrPartiEglence>>(await _orPartiEglenceRepository.GetListAsync());
            }
        }
    }
}