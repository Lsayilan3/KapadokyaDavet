
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


namespace Business.Handlers.OrTrioEkibis.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteOrTrioEkibiCommand : IRequest<IResult>
    {
        public int OrTrioEkibiId { get; set; }

        public class DeleteOrTrioEkibiCommandHandler : IRequestHandler<DeleteOrTrioEkibiCommand, IResult>
        {
            private readonly IOrTrioEkibiRepository _orTrioEkibiRepository;
            private readonly IMediator _mediator;

            public DeleteOrTrioEkibiCommandHandler(IOrTrioEkibiRepository orTrioEkibiRepository, IMediator mediator)
            {
                _orTrioEkibiRepository = orTrioEkibiRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteOrTrioEkibiCommand request, CancellationToken cancellationToken)
            {
                var orTrioEkibiToDelete = _orTrioEkibiRepository.Get(p => p.OrTrioEkibiId == request.OrTrioEkibiId);

                _orTrioEkibiRepository.Delete(orTrioEkibiToDelete);
                await _orTrioEkibiRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

