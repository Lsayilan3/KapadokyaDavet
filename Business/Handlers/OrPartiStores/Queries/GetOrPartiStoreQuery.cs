
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.OrPartiStores.Queries
{
    public class GetOrPartiStoreQuery : IRequest<IDataResult<OrPartiStore>>
    {
        public int OrPartiStoreId { get; set; }

        public class GetOrPartiStoreQueryHandler : IRequestHandler<GetOrPartiStoreQuery, IDataResult<OrPartiStore>>
        {
            private readonly IOrPartiStoreRepository _orPartiStoreRepository;
            private readonly IMediator _mediator;

            public GetOrPartiStoreQueryHandler(IOrPartiStoreRepository orPartiStoreRepository, IMediator mediator)
            {
                _orPartiStoreRepository = orPartiStoreRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<OrPartiStore>> Handle(GetOrPartiStoreQuery request, CancellationToken cancellationToken)
            {
                var orPartiStore = await _orPartiStoreRepository.GetAsync(p => p.OrPartiStoreId == request.OrPartiStoreId);
                return new SuccessDataResult<OrPartiStore>(orPartiStore);
            }
        }
    }
}
