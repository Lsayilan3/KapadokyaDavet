
using Business.Constants;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Core.Aspects.Autofac.Validation;
using Business.Handlers.Sliders.ValidationRules;


namespace Business.Handlers.Sliders.Commands
{


    public class UpdateSliderCommand : IRequest<IResult>
    {
        public int SliderId { get; set; }
        public string Photo { get; set; }

        public class UpdateSliderCommandHandler : IRequestHandler<UpdateSliderCommand, IResult>
        {
            private readonly ISliderRepository _sliderRepository;
            private readonly IMediator _mediator;

            public UpdateSliderCommandHandler(ISliderRepository sliderRepository, IMediator mediator)
            {
                _sliderRepository = sliderRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateSliderValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateSliderCommand request, CancellationToken cancellationToken)
            {
                var isThereSliderRecord = await _sliderRepository.GetAsync(u => u.SliderId == request.SliderId);


                isThereSliderRecord.Photo = request.Photo;


                _sliderRepository.Update(isThereSliderRecord);
                await _sliderRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

