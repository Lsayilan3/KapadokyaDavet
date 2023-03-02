
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


namespace Business.Handlers.Ensatans.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteEnsatanCommand : IRequest<IResult>
    {
        public int EnsatanId { get; set; }

        public class DeleteEnsatanCommandHandler : IRequestHandler<DeleteEnsatanCommand, IResult>
        {
            private readonly IEnsatanRepository _ensatanRepository;
            private readonly IMediator _mediator;

            public DeleteEnsatanCommandHandler(IEnsatanRepository ensatanRepository, IMediator mediator)
            {
                _ensatanRepository = ensatanRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteEnsatanCommand request, CancellationToken cancellationToken)
            {
                var ensatanToDelete = _ensatanRepository.Get(p => p.EnsatanId == request.EnsatanId);

                _ensatanRepository.Delete(ensatanToDelete);
                await _ensatanRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

