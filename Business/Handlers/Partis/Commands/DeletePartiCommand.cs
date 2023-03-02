
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


namespace Business.Handlers.Partis.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeletePartiCommand : IRequest<IResult>
    {
        public int PartiId { get; set; }

        public class DeletePartiCommandHandler : IRequestHandler<DeletePartiCommand, IResult>
        {
            private readonly IPartiRepository _partiRepository;
            private readonly IMediator _mediator;

            public DeletePartiCommandHandler(IPartiRepository partiRepository, IMediator mediator)
            {
                _partiRepository = partiRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeletePartiCommand request, CancellationToken cancellationToken)
            {
                var partiToDelete = _partiRepository.Get(p => p.PartiId == request.PartiId);

                _partiRepository.Delete(partiToDelete);
                await _partiRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

