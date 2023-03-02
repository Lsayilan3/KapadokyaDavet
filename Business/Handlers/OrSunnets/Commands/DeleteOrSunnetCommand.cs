
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


namespace Business.Handlers.OrSunnets.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteOrSunnetCommand : IRequest<IResult>
    {
        public int OrSunnetId { get; set; }

        public class DeleteOrSunnetCommandHandler : IRequestHandler<DeleteOrSunnetCommand, IResult>
        {
            private readonly IOrSunnetRepository _orSunnetRepository;
            private readonly IMediator _mediator;

            public DeleteOrSunnetCommandHandler(IOrSunnetRepository orSunnetRepository, IMediator mediator)
            {
                _orSunnetRepository = orSunnetRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteOrSunnetCommand request, CancellationToken cancellationToken)
            {
                var orSunnetToDelete = _orSunnetRepository.Get(p => p.OrSunnetId == request.OrSunnetId);

                _orSunnetRepository.Delete(orSunnetToDelete);
                await _orSunnetRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

