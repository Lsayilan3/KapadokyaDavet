
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


namespace Business.Handlers.OrEkipmans.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteOrEkipmanCommand : IRequest<IResult>
    {
        public int OrEkipmanId { get; set; }

        public class DeleteOrEkipmanCommandHandler : IRequestHandler<DeleteOrEkipmanCommand, IResult>
        {
            private readonly IOrEkipmanRepository _orEkipmanRepository;
            private readonly IMediator _mediator;

            public DeleteOrEkipmanCommandHandler(IOrEkipmanRepository orEkipmanRepository, IMediator mediator)
            {
                _orEkipmanRepository = orEkipmanRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteOrEkipmanCommand request, CancellationToken cancellationToken)
            {
                var orEkipmanToDelete = _orEkipmanRepository.Get(p => p.OrEkipmanId == request.OrEkipmanId);

                _orEkipmanRepository.Delete(orEkipmanToDelete);
                await _orEkipmanRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

