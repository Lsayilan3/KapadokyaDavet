
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


namespace Business.Handlers.OrCaterings.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteOrCateringCommand : IRequest<IResult>
    {
        public int OrCateringId { get; set; }

        public class DeleteOrCateringCommandHandler : IRequestHandler<DeleteOrCateringCommand, IResult>
        {
            private readonly IOrCateringRepository _orCateringRepository;
            private readonly IMediator _mediator;

            public DeleteOrCateringCommandHandler(IOrCateringRepository orCateringRepository, IMediator mediator)
            {
                _orCateringRepository = orCateringRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteOrCateringCommand request, CancellationToken cancellationToken)
            {
                var orCateringToDelete = _orCateringRepository.Get(p => p.OrCateringId == request.OrCateringId);

                _orCateringRepository.Delete(orCateringToDelete);
                await _orCateringRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

