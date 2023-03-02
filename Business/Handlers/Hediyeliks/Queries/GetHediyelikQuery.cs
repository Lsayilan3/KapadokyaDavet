
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Hediyeliks.Queries
{
    public class GetHediyelikQuery : IRequest<IDataResult<Hediyelik>>
    {
        public int HediyelikId { get; set; }

        public class GetHediyelikQueryHandler : IRequestHandler<GetHediyelikQuery, IDataResult<Hediyelik>>
        {
            private readonly IHediyelikRepository _hediyelikRepository;
            private readonly IMediator _mediator;

            public GetHediyelikQueryHandler(IHediyelikRepository hediyelikRepository, IMediator mediator)
            {
                _hediyelikRepository = hediyelikRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Hediyelik>> Handle(GetHediyelikQuery request, CancellationToken cancellationToken)
            {
                var hediyelik = await _hediyelikRepository.GetAsync(p => p.HediyelikId == request.HediyelikId);
                return new SuccessDataResult<Hediyelik>(hediyelik);
            }
        }
    }
}
