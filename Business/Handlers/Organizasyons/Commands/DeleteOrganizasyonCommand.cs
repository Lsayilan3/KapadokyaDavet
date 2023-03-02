
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


namespace Business.Handlers.Organizasyons.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteOrganizasyonCommand : IRequest<IResult>
    {
        public int OrganizasyonId { get; set; }

        public class DeleteOrganizasyonCommandHandler : IRequestHandler<DeleteOrganizasyonCommand, IResult>
        {
            private readonly IOrganizasyonRepository _organizasyonRepository;
            private readonly IMediator _mediator;

            public DeleteOrganizasyonCommandHandler(IOrganizasyonRepository organizasyonRepository, IMediator mediator)
            {
                _organizasyonRepository = organizasyonRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteOrganizasyonCommand request, CancellationToken cancellationToken)
            {
                var organizasyonToDelete = _organizasyonRepository.Get(p => p.OrganizasyonId == request.OrganizasyonId);

                _organizasyonRepository.Delete(organizasyonToDelete);
                await _organizasyonRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

