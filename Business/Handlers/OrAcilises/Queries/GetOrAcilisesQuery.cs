
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

namespace Business.Handlers.OrAcilises.Queries
{

    public class GetOrAcilisesQuery : IRequest<IDataResult<IEnumerable<OrAcilis>>>
    {
        public class GetOrAcilisesQueryHandler : IRequestHandler<GetOrAcilisesQuery, IDataResult<IEnumerable<OrAcilis>>>
        {
            private readonly IOrAcilisRepository _orAcilisRepository;
            private readonly IMediator _mediator;

            public GetOrAcilisesQueryHandler(IOrAcilisRepository orAcilisRepository, IMediator mediator)
            {
                _orAcilisRepository = orAcilisRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<OrAcilis>>> Handle(GetOrAcilisesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<OrAcilis>>(await _orAcilisRepository.GetListAsync());
            }
        }
    }
}