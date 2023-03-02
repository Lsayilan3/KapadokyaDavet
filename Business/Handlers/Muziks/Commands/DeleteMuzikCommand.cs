
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


namespace Business.Handlers.Muziks.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteMuzikCommand : IRequest<IResult>
    {
        public int MuzikId { get; set; }

        public class DeleteMuzikCommandHandler : IRequestHandler<DeleteMuzikCommand, IResult>
        {
            private readonly IMuzikRepository _muzikRepository;
            private readonly IMediator _mediator;

            public DeleteMuzikCommandHandler(IMuzikRepository muzikRepository, IMediator mediator)
            {
                _muzikRepository = muzikRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteMuzikCommand request, CancellationToken cancellationToken)
            {
                var muzikToDelete = _muzikRepository.Get(p => p.MuzikId == request.MuzikId);

                _muzikRepository.Delete(muzikToDelete);
                await _muzikRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

