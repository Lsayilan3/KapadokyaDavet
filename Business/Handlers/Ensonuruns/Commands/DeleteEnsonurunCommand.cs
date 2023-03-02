
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


namespace Business.Handlers.Ensonuruns.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteEnsonurunCommand : IRequest<IResult>
    {
        public int EnsonurunId { get; set; }

        public class DeleteEnsonurunCommandHandler : IRequestHandler<DeleteEnsonurunCommand, IResult>
        {
            private readonly IEnsonurunRepository _ensonurunRepository;
            private readonly IMediator _mediator;

            public DeleteEnsonurunCommandHandler(IEnsonurunRepository ensonurunRepository, IMediator mediator)
            {
                _ensonurunRepository = ensonurunRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteEnsonurunCommand request, CancellationToken cancellationToken)
            {
                var ensonurunToDelete = _ensonurunRepository.Get(p => p.EnsonurunId == request.EnsonurunId);

                _ensonurunRepository.Delete(ensonurunToDelete);
                await _ensonurunRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

