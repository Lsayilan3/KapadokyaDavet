
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


namespace Business.Handlers.OrPartiStores.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteOrPartiStoreCommand : IRequest<IResult>
    {
        public int OrPartiStoreId { get; set; }

        public class DeleteOrPartiStoreCommandHandler : IRequestHandler<DeleteOrPartiStoreCommand, IResult>
        {
            private readonly IOrPartiStoreRepository _orPartiStoreRepository;
            private readonly IMediator _mediator;

            public DeleteOrPartiStoreCommandHandler(IOrPartiStoreRepository orPartiStoreRepository, IMediator mediator)
            {
                _orPartiStoreRepository = orPartiStoreRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteOrPartiStoreCommand request, CancellationToken cancellationToken)
            {
                var orPartiStoreToDelete = _orPartiStoreRepository.Get(p => p.OrPartiStoreId == request.OrPartiStoreId);

                _orPartiStoreRepository.Delete(orPartiStoreToDelete);
                await _orPartiStoreRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

