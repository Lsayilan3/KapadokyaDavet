
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


namespace Business.Handlers.OrPersonelTeminis.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteOrPersonelTeminiCommand : IRequest<IResult>
    {
        public int OrPersonelTeminiId { get; set; }

        public class DeleteOrPersonelTeminiCommandHandler : IRequestHandler<DeleteOrPersonelTeminiCommand, IResult>
        {
            private readonly IOrPersonelTeminiRepository _orPersonelTeminiRepository;
            private readonly IMediator _mediator;

            public DeleteOrPersonelTeminiCommandHandler(IOrPersonelTeminiRepository orPersonelTeminiRepository, IMediator mediator)
            {
                _orPersonelTeminiRepository = orPersonelTeminiRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteOrPersonelTeminiCommand request, CancellationToken cancellationToken)
            {
                var orPersonelTeminiToDelete = _orPersonelTeminiRepository.Get(p => p.OrPersonelTeminiId == request.OrPersonelTeminiId);

                _orPersonelTeminiRepository.Delete(orPersonelTeminiToDelete);
                await _orPersonelTeminiRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

