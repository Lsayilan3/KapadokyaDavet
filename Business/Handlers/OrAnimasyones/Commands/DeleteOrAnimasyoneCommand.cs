
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


namespace Business.Handlers.OrAnimasyones.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteOrAnimasyoneCommand : IRequest<IResult>
    {
        public int OrAnimasyoneId { get; set; }

        public class DeleteOrAnimasyoneCommandHandler : IRequestHandler<DeleteOrAnimasyoneCommand, IResult>
        {
            private readonly IOrAnimasyoneRepository _orAnimasyoneRepository;
            private readonly IMediator _mediator;

            public DeleteOrAnimasyoneCommandHandler(IOrAnimasyoneRepository orAnimasyoneRepository, IMediator mediator)
            {
                _orAnimasyoneRepository = orAnimasyoneRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteOrAnimasyoneCommand request, CancellationToken cancellationToken)
            {
                var orAnimasyoneToDelete = _orAnimasyoneRepository.Get(p => p.OrAnimasyoneId == request.OrAnimasyoneId);

                _orAnimasyoneRepository.Delete(orAnimasyoneToDelete);
                await _orAnimasyoneRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

