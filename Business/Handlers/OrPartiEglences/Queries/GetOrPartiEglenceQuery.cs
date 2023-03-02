
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.OrPartiEglences.Queries
{
    public class GetOrPartiEglenceQuery : IRequest<IDataResult<OrPartiEglence>>
    {
        public int OrPartiEglenceId { get; set; }

        public class GetOrPartiEglenceQueryHandler : IRequestHandler<GetOrPartiEglenceQuery, IDataResult<OrPartiEglence>>
        {
            private readonly IOrPartiEglenceRepository _orPartiEglenceRepository;
            private readonly IMediator _mediator;

            public GetOrPartiEglenceQueryHandler(IOrPartiEglenceRepository orPartiEglenceRepository, IMediator mediator)
            {
                _orPartiEglenceRepository = orPartiEglenceRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<OrPartiEglence>> Handle(GetOrPartiEglenceQuery request, CancellationToken cancellationToken)
            {
                var orPartiEglence = await _orPartiEglenceRepository.GetAsync(p => p.OrPartiEglenceId == request.OrPartiEglenceId);
                return new SuccessDataResult<OrPartiEglence>(orPartiEglence);
            }
        }
    }
}
