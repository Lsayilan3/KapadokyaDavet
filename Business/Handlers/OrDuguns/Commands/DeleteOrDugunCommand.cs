
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


namespace Business.Handlers.OrDuguns.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteOrDugunCommand : IRequest<IResult>
    {
        public int OrDugunId { get; set; }

        public class DeleteOrDugunCommandHandler : IRequestHandler<DeleteOrDugunCommand, IResult>
        {
            private readonly IOrDugunRepository _orDugunRepository;
            private readonly IMediator _mediator;

            public DeleteOrDugunCommandHandler(IOrDugunRepository orDugunRepository, IMediator mediator)
            {
                _orDugunRepository = orDugunRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteOrDugunCommand request, CancellationToken cancellationToken)
            {
                var orDugunToDelete = _orDugunRepository.Get(p => p.OrDugunId == request.OrDugunId);

                _orDugunRepository.Delete(orDugunToDelete);
                await _orDugunRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

