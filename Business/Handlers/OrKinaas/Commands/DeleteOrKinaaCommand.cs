
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


namespace Business.Handlers.OrKinaas.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteOrKinaaCommand : IRequest<IResult>
    {
        public int OrKinaaId { get; set; }

        public class DeleteOrKinaaCommandHandler : IRequestHandler<DeleteOrKinaaCommand, IResult>
        {
            private readonly IOrKinaaRepository _orKinaaRepository;
            private readonly IMediator _mediator;

            public DeleteOrKinaaCommandHandler(IOrKinaaRepository orKinaaRepository, IMediator mediator)
            {
                _orKinaaRepository = orKinaaRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteOrKinaaCommand request, CancellationToken cancellationToken)
            {
                var orKinaaToDelete = _orKinaaRepository.Get(p => p.OrKinaaId == request.OrKinaaId);

                _orKinaaRepository.Delete(orKinaaToDelete);
                await _orKinaaRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

