
using Business.BusinessAspects;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Caching;

namespace Business.Handlers.SliderTwoes.Queries
{

    public class GetSliderTwoesQuery : IRequest<IDataResult<IEnumerable<SliderTwo>>>
    {
        public class GetSliderTwoesQueryHandler : IRequestHandler<GetSliderTwoesQuery, IDataResult<IEnumerable<SliderTwo>>>
        {
            private readonly ISliderTwoRepository _sliderTwoRepository;
            private readonly IMediator _mediator;

            public GetSliderTwoesQueryHandler(ISliderTwoRepository sliderTwoRepository, IMediator mediator)
            {
                _sliderTwoRepository = sliderTwoRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<SliderTwo>>> Handle(GetSliderTwoesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<SliderTwo>>(await _sliderTwoRepository.GetListAsync());
            }
        }
    }
}