
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


namespace Business.Handlers.OrSirketEglences.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteOrSirketEglenceCommand : IRequest<IResult>
    {
        public int OrSirketEglenceId { get; set; }

        public class DeleteOrSirketEglenceCommandHandler : IRequestHandler<DeleteOrSirketEglenceCommand, IResult>
        {
            private readonly IOrSirketEglenceRepository _orSirketEglenceRepository;
            private readonly IMediator _mediator;

            public DeleteOrSirketEglenceCommandHandler(IOrSirketEglenceRepository orSirketEglenceRepository, IMediator mediator)
            {
                _orSirketEglenceRepository = orSirketEglenceRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteOrSirketEglenceCommand request, CancellationToken cancellationToken)
            {
                var orSirketEglenceToDelete = _orSirketEglenceRepository.Get(p => p.OrSirketEglenceId == request.OrSirketEglenceId);

                _orSirketEglenceRepository.Delete(orSirketEglenceToDelete);
                await _orSirketEglenceRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

