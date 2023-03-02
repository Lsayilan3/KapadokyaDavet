
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Yiyeceks.Queries
{
    public class GetYiyecekQuery : IRequest<IDataResult<Yiyecek>>
    {
        public int YiyecekId { get; set; }

        public class GetYiyecekQueryHandler : IRequestHandler<GetYiyecekQuery, IDataResult<Yiyecek>>
        {
            private readonly IYiyecekRepository _yiyecekRepository;
            private readonly IMediator _mediator;

            public GetYiyecekQueryHandler(IYiyecekRepository yiyecekRepository, IMediator mediator)
            {
                _yiyecekRepository = yiyecekRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Yiyecek>> Handle(GetYiyecekQuery request, CancellationToken cancellationToken)
            {
                var yiyecek = await _yiyecekRepository.GetAsync(p => p.YiyecekId == request.YiyecekId);
                return new SuccessDataResult<Yiyecek>(yiyecek);
            }
        }
    }
}
