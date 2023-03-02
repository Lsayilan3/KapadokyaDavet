
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Spots.Queries
{
    public class GetSpotQuery : IRequest<IDataResult<Spot>>
    {
        public int SpotId { get; set; }

        public class GetSpotQueryHandler : IRequestHandler<GetSpotQuery, IDataResult<Spot>>
        {
            private readonly ISpotRepository _spotRepository;
            private readonly IMediator _mediator;

            public GetSpotQueryHandler(ISpotRepository spotRepository, IMediator mediator)
            {
                _spotRepository = spotRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Spot>> Handle(GetSpotQuery request, CancellationToken cancellationToken)
            {
                var spot = await _spotRepository.GetAsync(p => p.SpotId == request.SpotId);
                return new SuccessDataResult<Spot>(spot);
            }
        }
    }
}
