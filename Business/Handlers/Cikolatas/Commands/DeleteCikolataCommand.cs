
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


namespace Business.Handlers.Cikolatas.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteCikolataCommand : IRequest<IResult>
    {
        public int CikolataId { get; set; }

        public class DeleteCikolataCommandHandler : IRequestHandler<DeleteCikolataCommand, IResult>
        {
            private readonly ICikolataRepository _cikolataRepository;
            private readonly IMediator _mediator;

            public DeleteCikolataCommandHandler(ICikolataRepository cikolataRepository, IMediator mediator)
            {
                _cikolataRepository = cikolataRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteCikolataCommand request, CancellationToken cancellationToken)
            {
                var cikolataToDelete = _cikolataRepository.Get(p => p.CikolataId == request.CikolataId);

                _cikolataRepository.Delete(cikolataToDelete);
                await _cikolataRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

