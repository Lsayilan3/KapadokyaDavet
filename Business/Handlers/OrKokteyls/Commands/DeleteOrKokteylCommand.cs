
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


namespace Business.Handlers.OrKokteyls.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteOrKokteylCommand : IRequest<IResult>
    {
        public int OrKokteylId { get; set; }

        public class DeleteOrKokteylCommandHandler : IRequestHandler<DeleteOrKokteylCommand, IResult>
        {
            private readonly IOrKokteylRepository _orKokteylRepository;
            private readonly IMediator _mediator;

            public DeleteOrKokteylCommandHandler(IOrKokteylRepository orKokteylRepository, IMediator mediator)
            {
                _orKokteylRepository = orKokteylRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteOrKokteylCommand request, CancellationToken cancellationToken)
            {
                var orKokteylToDelete = _orKokteylRepository.Get(p => p.OrKokteylId == request.OrKokteylId);

                _orKokteylRepository.Delete(orKokteylToDelete);
                await _orKokteylRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

