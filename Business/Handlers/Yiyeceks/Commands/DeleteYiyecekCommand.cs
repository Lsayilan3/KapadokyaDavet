
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


namespace Business.Handlers.Yiyeceks.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteYiyecekCommand : IRequest<IResult>
    {
        public int YiyecekId { get; set; }

        public class DeleteYiyecekCommandHandler : IRequestHandler<DeleteYiyecekCommand, IResult>
        {
            private readonly IYiyecekRepository _yiyecekRepository;
            private readonly IMediator _mediator;

            public DeleteYiyecekCommandHandler(IYiyecekRepository yiyecekRepository, IMediator mediator)
            {
                _yiyecekRepository = yiyecekRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteYiyecekCommand request, CancellationToken cancellationToken)
            {
                var yiyecekToDelete = _yiyecekRepository.Get(p => p.YiyecekId == request.YiyecekId);

                _yiyecekRepository.Delete(yiyecekToDelete);
                await _yiyecekRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

