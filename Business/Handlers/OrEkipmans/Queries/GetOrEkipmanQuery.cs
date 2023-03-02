
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.OrEkipmans.Queries
{
    public class GetOrEkipmanQuery : IRequest<IDataResult<OrEkipman>>
    {
        public int OrEkipmanId { get; set; }

        public class GetOrEkipmanQueryHandler : IRequestHandler<GetOrEkipmanQuery, IDataResult<OrEkipman>>
        {
            private readonly IOrEkipmanRepository _orEkipmanRepository;
            private readonly IMediator _mediator;

            public GetOrEkipmanQueryHandler(IOrEkipmanRepository orEkipmanRepository, IMediator mediator)
            {
                _orEkipmanRepository = orEkipmanRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<OrEkipman>> Handle(GetOrEkipmanQuery request, CancellationToken cancellationToken)
            {
                var orEkipman = await _orEkipmanRepository.GetAsync(p => p.OrEkipmanId == request.OrEkipmanId);
                return new SuccessDataResult<OrEkipman>(orEkipman);
            }
        }
    }
}
