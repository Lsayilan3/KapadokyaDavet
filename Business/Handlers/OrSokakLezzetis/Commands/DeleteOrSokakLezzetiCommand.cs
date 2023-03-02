
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


namespace Business.Handlers.OrSokakLezzetis.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteOrSokakLezzetiCommand : IRequest<IResult>
    {
        public int OrSokakLezzetiId { get; set; }

        public class DeleteOrSokakLezzetiCommandHandler : IRequestHandler<DeleteOrSokakLezzetiCommand, IResult>
        {
            private readonly IOrSokakLezzetiRepository _orSokakLezzetiRepository;
            private readonly IMediator _mediator;

            public DeleteOrSokakLezzetiCommandHandler(IOrSokakLezzetiRepository orSokakLezzetiRepository, IMediator mediator)
            {
                _orSokakLezzetiRepository = orSokakLezzetiRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteOrSokakLezzetiCommand request, CancellationToken cancellationToken)
            {
                var orSokakLezzetiToDelete = _orSokakLezzetiRepository.Get(p => p.OrSokakLezzetiId == request.OrSokakLezzetiId);

                _orSokakLezzetiRepository.Delete(orSokakLezzetiToDelete);
                await _orSokakLezzetiRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

