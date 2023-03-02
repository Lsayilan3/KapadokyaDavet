
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

namespace Business.Handlers.Ensatans.Queries
{

    public class GetEnsatansQuery : IRequest<IDataResult<IEnumerable<Ensatan>>>
    {
        public class GetEnsatansQueryHandler : IRequestHandler<GetEnsatansQuery, IDataResult<IEnumerable<Ensatan>>>
        {
            private readonly IEnsatanRepository _ensatanRepository;
            private readonly IMediator _mediator;

            public GetEnsatansQueryHandler(IEnsatanRepository ensatanRepository, IMediator mediator)
            {
                _ensatanRepository = ensatanRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Ensatan>>> Handle(GetEnsatansQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Ensatan>>(await _ensatanRepository.GetListAsync());
            }
        }
    }
}