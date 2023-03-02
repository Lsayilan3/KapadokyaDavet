
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.OrNisans.Queries
{
    public class GetOrNisanQuery : IRequest<IDataResult<OrNisan>>
    {
        public int OrNisanId { get; set; }

        public class GetOrNisanQueryHandler : IRequestHandler<GetOrNisanQuery, IDataResult<OrNisan>>
        {
            private readonly IOrNisanRepository _orNisanRepository;
            private readonly IMediator _mediator;

            public GetOrNisanQueryHandler(IOrNisanRepository orNisanRepository, IMediator mediator)
            {
                _orNisanRepository = orNisanRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<OrNisan>> Handle(GetOrNisanQuery request, CancellationToken cancellationToken)
            {
                var orNisan = await _orNisanRepository.GetAsync(p => p.OrNisanId == request.OrNisanId);
                return new SuccessDataResult<OrNisan>(orNisan);
            }
        }
    }
}
