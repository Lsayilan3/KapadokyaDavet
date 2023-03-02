
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.OrAcilises.Queries
{
    public class GetOrAcilisQuery : IRequest<IDataResult<OrAcilis>>
    {
        public int OrAcilisId { get; set; }

        public class GetOrAcilisQueryHandler : IRequestHandler<GetOrAcilisQuery, IDataResult<OrAcilis>>
        {
            private readonly IOrAcilisRepository _orAcilisRepository;
            private readonly IMediator _mediator;

            public GetOrAcilisQueryHandler(IOrAcilisRepository orAcilisRepository, IMediator mediator)
            {
                _orAcilisRepository = orAcilisRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<OrAcilis>> Handle(GetOrAcilisQuery request, CancellationToken cancellationToken)
            {
                var orAcilis = await _orAcilisRepository.GetAsync(p => p.OrAcilisId == request.OrAcilisId);
                return new SuccessDataResult<OrAcilis>(orAcilis);
            }
        }
    }
}
