
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


namespace Business.Handlers.OrNisans.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteOrNisanCommand : IRequest<IResult>
    {
        public int OrNisanId { get; set; }

        public class DeleteOrNisanCommandHandler : IRequestHandler<DeleteOrNisanCommand, IResult>
        {
            private readonly IOrNisanRepository _orNisanRepository;
            private readonly IMediator _mediator;

            public DeleteOrNisanCommandHandler(IOrNisanRepository orNisanRepository, IMediator mediator)
            {
                _orNisanRepository = orNisanRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteOrNisanCommand request, CancellationToken cancellationToken)
            {
                var orNisanToDelete = _orNisanRepository.Get(p => p.OrNisanId == request.OrNisanId);

                _orNisanRepository.Delete(orNisanToDelete);
                await _orNisanRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

