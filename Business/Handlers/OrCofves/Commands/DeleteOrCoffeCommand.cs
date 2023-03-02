
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


namespace Business.Handlers.OrCofves.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteOrCoffeCommand : IRequest<IResult>
    {
        public int OrCoffeId { get; set; }

        public class DeleteOrCoffeCommandHandler : IRequestHandler<DeleteOrCoffeCommand, IResult>
        {
            private readonly IOrCoffeRepository _orCoffeRepository;
            private readonly IMediator _mediator;

            public DeleteOrCoffeCommandHandler(IOrCoffeRepository orCoffeRepository, IMediator mediator)
            {
                _orCoffeRepository = orCoffeRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteOrCoffeCommand request, CancellationToken cancellationToken)
            {
                var orCoffeToDelete = _orCoffeRepository.Get(p => p.OrCoffeId == request.OrCoffeId);

                _orCoffeRepository.Delete(orCoffeToDelete);
                await _orCoffeRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

