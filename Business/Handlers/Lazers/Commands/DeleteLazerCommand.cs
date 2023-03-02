
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


namespace Business.Handlers.Lazers.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteLazerCommand : IRequest<IResult>
    {
        public int LazerId { get; set; }

        public class DeleteLazerCommandHandler : IRequestHandler<DeleteLazerCommand, IResult>
        {
            private readonly ILazerRepository _lazerRepository;
            private readonly IMediator _mediator;

            public DeleteLazerCommandHandler(ILazerRepository lazerRepository, IMediator mediator)
            {
                _lazerRepository = lazerRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteLazerCommand request, CancellationToken cancellationToken)
            {
                var lazerToDelete = _lazerRepository.Get(p => p.LazerId == request.LazerId);

                _lazerRepository.Delete(lazerToDelete);
                await _lazerRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

