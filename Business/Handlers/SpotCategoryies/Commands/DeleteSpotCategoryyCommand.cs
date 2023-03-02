
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


namespace Business.Handlers.SpotCategoryies.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteSpotCategoryyCommand : IRequest<IResult>
    {
        public int SpotCategoryyId { get; set; }

        public class DeleteSpotCategoryyCommandHandler : IRequestHandler<DeleteSpotCategoryyCommand, IResult>
        {
            private readonly ISpotCategoryyRepository _spotCategoryyRepository;
            private readonly IMediator _mediator;

            public DeleteSpotCategoryyCommandHandler(ISpotCategoryyRepository spotCategoryyRepository, IMediator mediator)
            {
                _spotCategoryyRepository = spotCategoryyRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteSpotCategoryyCommand request, CancellationToken cancellationToken)
            {
                var spotCategoryyToDelete = _spotCategoryyRepository.Get(p => p.SpotCategoryyId == request.SpotCategoryyId);

                _spotCategoryyRepository.Delete(spotCategoryyToDelete);
                await _spotCategoryyRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

