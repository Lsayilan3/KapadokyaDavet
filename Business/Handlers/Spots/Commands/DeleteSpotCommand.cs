
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


namespace Business.Handlers.Spots.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteSpotCommand : IRequest<IResult>
    {
        public int SpotId { get; set; }

        public class DeleteSpotCommandHandler : IRequestHandler<DeleteSpotCommand, IResult>
        {
            private readonly ISpotRepository _spotRepository;
            private readonly IMediator _mediator;

            public DeleteSpotCommandHandler(ISpotRepository spotRepository, IMediator mediator)
            {
                _spotRepository = spotRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteSpotCommand request, CancellationToken cancellationToken)
            {
                var spotToDelete = _spotRepository.Get(p => p.SpotId == request.SpotId);

                _spotRepository.Delete(spotToDelete);
                await _spotRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

