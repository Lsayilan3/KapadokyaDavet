
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Partis.Queries
{
    public class GetPartiQuery : IRequest<IDataResult<Parti>>
    {
        public int PartiId { get; set; }

        public class GetPartiQueryHandler : IRequestHandler<GetPartiQuery, IDataResult<Parti>>
        {
            private readonly IPartiRepository _partiRepository;
            private readonly IMediator _mediator;

            public GetPartiQueryHandler(IPartiRepository partiRepository, IMediator mediator)
            {
                _partiRepository = partiRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Parti>> Handle(GetPartiQuery request, CancellationToken cancellationToken)
            {
                var parti = await _partiRepository.GetAsync(p => p.PartiId == request.PartiId);
                return new SuccessDataResult<Parti>(parti);
            }
        }
    }
}
