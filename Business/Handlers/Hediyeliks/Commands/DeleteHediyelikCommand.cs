
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


namespace Business.Handlers.Hediyeliks.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteHediyelikCommand : IRequest<IResult>
    {
        public int HediyelikId { get; set; }

        public class DeleteHediyelikCommandHandler : IRequestHandler<DeleteHediyelikCommand, IResult>
        {
            private readonly IHediyelikRepository _hediyelikRepository;
            private readonly IMediator _mediator;

            public DeleteHediyelikCommandHandler(IHediyelikRepository hediyelikRepository, IMediator mediator)
            {
                _hediyelikRepository = hediyelikRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteHediyelikCommand request, CancellationToken cancellationToken)
            {
                var hediyelikToDelete = _hediyelikRepository.Get(p => p.HediyelikId == request.HediyelikId);

                _hediyelikRepository.Delete(hediyelikToDelete);
                await _hediyelikRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

