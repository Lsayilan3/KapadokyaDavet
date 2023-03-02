
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Muziks.Queries
{
    public class GetMuzikQuery : IRequest<IDataResult<Muzik>>
    {
        public int MuzikId { get; set; }

        public class GetMuzikQueryHandler : IRequestHandler<GetMuzikQuery, IDataResult<Muzik>>
        {
            private readonly IMuzikRepository _muzikRepository;
            private readonly IMediator _mediator;

            public GetMuzikQueryHandler(IMuzikRepository muzikRepository, IMediator mediator)
            {
                _muzikRepository = muzikRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Muzik>> Handle(GetMuzikQuery request, CancellationToken cancellationToken)
            {
                var muzik = await _muzikRepository.GetAsync(p => p.MuzikId == request.MuzikId);
                return new SuccessDataResult<Muzik>(muzik);
            }
        }
    }
}
