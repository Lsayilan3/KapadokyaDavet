
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


namespace Business.Handlers.OrCikolatas.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteOrCikolataCommand : IRequest<IResult>
    {
        public int OrCikolataId { get; set; }

        public class DeleteOrCikolataCommandHandler : IRequestHandler<DeleteOrCikolataCommand, IResult>
        {
            private readonly IOrCikolataRepository _orCikolataRepository;
            private readonly IMediator _mediator;

            public DeleteOrCikolataCommandHandler(IOrCikolataRepository orCikolataRepository, IMediator mediator)
            {
                _orCikolataRepository = orCikolataRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteOrCikolataCommand request, CancellationToken cancellationToken)
            {
                var orCikolataToDelete = _orCikolataRepository.Get(p => p.OrCikolataId == request.OrCikolataId);

                _orCikolataRepository.Delete(orCikolataToDelete);
                await _orCikolataRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

