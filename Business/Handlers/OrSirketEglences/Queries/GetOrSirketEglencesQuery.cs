
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

namespace Business.Handlers.OrSirketEglences.Queries
{

    public class GetOrSirketEglencesQuery : IRequest<IDataResult<IEnumerable<OrSirketEglence>>>
    {
        public class GetOrSirketEglencesQueryHandler : IRequestHandler<GetOrSirketEglencesQuery, IDataResult<IEnumerable<OrSirketEglence>>>
        {
            private readonly IOrSirketEglenceRepository _orSirketEglenceRepository;
            private readonly IMediator _mediator;

            public GetOrSirketEglencesQueryHandler(IOrSirketEglenceRepository orSirketEglenceRepository, IMediator mediator)
            {
                _orSirketEglenceRepository = orSirketEglenceRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<OrSirketEglence>>> Handle(GetOrSirketEglencesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<OrSirketEglence>>(await _orSirketEglenceRepository.GetListAsync());
            }
        }
    }
}