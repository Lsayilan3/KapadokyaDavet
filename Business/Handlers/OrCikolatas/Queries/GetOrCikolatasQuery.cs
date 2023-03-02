
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

namespace Business.Handlers.OrCikolatas.Queries
{

    public class GetOrCikolatasQuery : IRequest<IDataResult<IEnumerable<OrCikolata>>>
    {
        public class GetOrCikolatasQueryHandler : IRequestHandler<GetOrCikolatasQuery, IDataResult<IEnumerable<OrCikolata>>>
        {
            private readonly IOrCikolataRepository _orCikolataRepository;
            private readonly IMediator _mediator;

            public GetOrCikolatasQueryHandler(IOrCikolataRepository orCikolataRepository, IMediator mediator)
            {
                _orCikolataRepository = orCikolataRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<OrCikolata>>> Handle(GetOrCikolatasQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<OrCikolata>>(await _orCikolataRepository.GetListAsync());
            }
        }
    }
}