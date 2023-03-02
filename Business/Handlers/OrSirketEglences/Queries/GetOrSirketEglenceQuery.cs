
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.OrSirketEglences.Queries
{
    public class GetOrSirketEglenceQuery : IRequest<IDataResult<OrSirketEglence>>
    {
        public int OrSirketEglenceId { get; set; }

        public class GetOrSirketEglenceQueryHandler : IRequestHandler<GetOrSirketEglenceQuery, IDataResult<OrSirketEglence>>
        {
            private readonly IOrSirketEglenceRepository _orSirketEglenceRepository;
            private readonly IMediator _mediator;

            public GetOrSirketEglenceQueryHandler(IOrSirketEglenceRepository orSirketEglenceRepository, IMediator mediator)
            {
                _orSirketEglenceRepository = orSirketEglenceRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<OrSirketEglence>> Handle(GetOrSirketEglenceQuery request, CancellationToken cancellationToken)
            {
                var orSirketEglence = await _orSirketEglenceRepository.GetAsync(p => p.OrSirketEglenceId == request.OrSirketEglenceId);
                return new SuccessDataResult<OrSirketEglence>(orSirketEglence);
            }
        }
    }
}
