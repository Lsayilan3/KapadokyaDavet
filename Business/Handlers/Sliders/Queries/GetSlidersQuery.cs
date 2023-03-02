
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

namespace Business.Handlers.Sliders.Queries
{

    public class GetSlidersQuery : IRequest<IDataResult<IEnumerable<Slider>>>
    {
        public class GetSlidersQueryHandler : IRequestHandler<GetSlidersQuery, IDataResult<IEnumerable<Slider>>>
        {
            private readonly ISliderRepository _sliderRepository;
            private readonly IMediator _mediator;

            public GetSlidersQueryHandler(ISliderRepository sliderRepository, IMediator mediator)
            {
                _sliderRepository = sliderRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Slider>>> Handle(GetSlidersQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Slider>>(await _sliderRepository.GetListAsync());
            }
        }
    }
}