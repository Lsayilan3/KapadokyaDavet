
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Ensatans.Queries
{
    public class GetEnsatanQuery : IRequest<IDataResult<Ensatan>>
    {
        public int EnsatanId { get; set; }

        public class GetEnsatanQueryHandler : IRequestHandler<GetEnsatanQuery, IDataResult<Ensatan>>
        {
            private readonly IEnsatanRepository _ensatanRepository;
            private readonly IMediator _mediator;

            public GetEnsatanQueryHandler(IEnsatanRepository ensatanRepository, IMediator mediator)
            {
                _ensatanRepository = ensatanRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Ensatan>> Handle(GetEnsatanQuery request, CancellationToken cancellationToken)
            {
                var ensatan = await _ensatanRepository.GetAsync(p => p.EnsatanId == request.EnsatanId);
                return new SuccessDataResult<Ensatan>(ensatan);
            }
        }
    }
}
