
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


namespace Business.Handlers.OrAcilises.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteOrAcilisCommand : IRequest<IResult>
    {
        public int OrAcilisId { get; set; }

        public class DeleteOrAcilisCommandHandler : IRequestHandler<DeleteOrAcilisCommand, IResult>
        {
            private readonly IOrAcilisRepository _orAcilisRepository;
            private readonly IMediator _mediator;

            public DeleteOrAcilisCommandHandler(IOrAcilisRepository orAcilisRepository, IMediator mediator)
            {
                _orAcilisRepository = orAcilisRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteOrAcilisCommand request, CancellationToken cancellationToken)
            {
                var orAcilisToDelete = _orAcilisRepository.Get(p => p.OrAcilisId == request.OrAcilisId);

                _orAcilisRepository.Delete(orAcilisToDelete);
                await _orAcilisRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

