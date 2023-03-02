
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace Business.Handlers.OrPartiEglences.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteOrPartiEglenceCommand : IRequest<IResult>
    {
        public int OrPartiEglenceId { get; set; }

        public class DeleteOrPartiEglenceCommandHandler : IRequestHandler<DeleteOrPartiEglenceCommand, IResult>
        {
            private readonly IOrPartiEglenceRepository _orPartiEglenceRepository;
            private readonly IMediator _mediator;

            public DeleteOrPartiEglenceCommandHandler(IOrPartiEglenceRepository orPartiEglenceRepository, IMediator mediator)
            {
                _orPartiEglenceRepository = orPartiEglenceRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteOrPartiEglenceCommand request, CancellationToken cancellationToken)
            {
                var orPartiEglenceToDelete = _orPartiEglenceRepository.Get(p => p.OrPartiEglenceId == request.OrPartiEglenceId);

                _orPartiEglenceRepository.Delete(orPartiEglenceToDelete);
                await _orPartiEglenceRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

