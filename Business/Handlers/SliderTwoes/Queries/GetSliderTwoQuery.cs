
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.SliderTwoes.Queries
{
    public class GetSliderTwoQuery : IRequest<IDataResult<SliderTwo>>
    {
        public int SliderTwoId { get; set; }

        public class GetSliderTwoQueryHandler : IRequestHandler<GetSliderTwoQuery, IDataResult<SliderTwo>>
        {
            private readonly ISliderTwoRepository _sliderTwoRepository;
            private readonly IMediator _mediator;

            public GetSliderTwoQueryHandler(ISliderTwoRepository sliderTwoRepository, IMediator mediator)
            {
                _sliderTwoRepository = sliderTwoRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<SliderTwo>> Handle(GetSliderTwoQuery request, CancellationToken cancellationToken)
            {
                var sliderTwo = await _sliderTwoRepository.GetAsync(p => p.SliderTwoId == request.SliderTwoId);
                return new SuccessDataResult<SliderTwo>(sliderTwo);
            }
        }
    }
}
