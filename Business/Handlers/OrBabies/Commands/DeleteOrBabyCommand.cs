
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


namespace Business.Handlers.OrBabies.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteOrBabyCommand : IRequest<IResult>
    {
        public int OrBabyId { get; set; }

        public class DeleteOrBabyCommandHandler : IRequestHandler<DeleteOrBabyCommand, IResult>
        {
            private readonly IOrBabyRepository _orBabyRepository;
            private readonly IMediator _mediator;

            public DeleteOrBabyCommandHandler(IOrBabyRepository orBabyRepository, IMediator mediator)
            {
                _orBabyRepository = orBabyRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteOrBabyCommand request, CancellationToken cancellationToken)
            {
                var orBabyToDelete = _orBabyRepository.Get(p => p.OrBabyId == request.OrBabyId);

                _orBabyRepository.Delete(orBabyToDelete);
                await _orBabyRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

