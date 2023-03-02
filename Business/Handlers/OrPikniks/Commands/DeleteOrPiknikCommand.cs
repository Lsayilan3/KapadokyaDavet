
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


namespace Business.Handlers.OrPikniks.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteOrPiknikCommand : IRequest<IResult>
    {
        public int OrPiknikId { get; set; }

        public class DeleteOrPiknikCommandHandler : IRequestHandler<DeleteOrPiknikCommand, IResult>
        {
            private readonly IOrPiknikRepository _orPiknikRepository;
            private readonly IMediator _mediator;

            public DeleteOrPiknikCommandHandler(IOrPiknikRepository orPiknikRepository, IMediator mediator)
            {
                _orPiknikRepository = orPiknikRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteOrPiknikCommand request, CancellationToken cancellationToken)
            {
                var orPiknikToDelete = _orPiknikRepository.Get(p => p.OrPiknikId == request.OrPiknikId);

                _orPiknikRepository.Delete(orPiknikToDelete);
                await _orPiknikRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

