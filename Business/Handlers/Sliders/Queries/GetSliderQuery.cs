
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Sliders.Queries
{
    public class GetSliderQuery : IRequest<IDataResult<Slider>>
    {
        public int SliderId { get; set; }

        public class GetSliderQueryHandler : IRequestHandler<GetSliderQuery, IDataResult<Slider>>
        {
            private readonly ISliderRepository _sliderRepository;
            private readonly IMediator _mediator;

            public GetSliderQueryHandler(ISliderRepository sliderRepository, IMediator mediator)
            {
                _sliderRepository = sliderRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Slider>> Handle(GetSliderQuery request, CancellationToken cancellationToken)
            {
                var slider = await _sliderRepository.GetAsync(p => p.SliderId == request.SliderId);
                return new SuccessDataResult<Slider>(slider);
            }
        }
    }
}
