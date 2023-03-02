
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Organizasyons.Queries
{
    public class GetOrganizasyonQuery : IRequest<IDataResult<Organizasyon>>
    {
        public int OrganizasyonId { get; set; }

        public class GetOrganizasyonQueryHandler : IRequestHandler<GetOrganizasyonQuery, IDataResult<Organizasyon>>
        {
            private readonly IOrganizasyonRepository _organizasyonRepository;
            private readonly IMediator _mediator;

            public GetOrganizasyonQueryHandler(IOrganizasyonRepository organizasyonRepository, IMediator mediator)
            {
                _organizasyonRepository = organizasyonRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Organizasyon>> Handle(GetOrganizasyonQuery request, CancellationToken cancellationToken)
            {
                var organizasyon = await _organizasyonRepository.GetAsync(p => p.OrganizasyonId == request.OrganizasyonId);
                return new SuccessDataResult<Organizasyon>(organizasyon);
            }
        }
    }
}
