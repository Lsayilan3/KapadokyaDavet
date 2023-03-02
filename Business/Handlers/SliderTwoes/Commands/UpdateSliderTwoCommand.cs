
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
using Business.Handlers.SliderTwoes.ValidationRules;


namespace Business.Handlers.SliderTwoes.Commands
{


    public class UpdateSliderTwoCommand : IRequest<IResult>
    {
        public int SliderTwoId { get; set; }
        public string Title { get; set; }
        public string Detay { get; set; }
        public int Price { get; set; }
        public int DiscountPrice { get; set; }
        public string Photo { get; set; }

        public class UpdateSliderTwoCommandHandler : IRequestHandler<UpdateSliderTwoCommand, IResult>
        {
            private readonly ISliderTwoRepository _sliderTwoRepository;
            private readonly IMediator _mediator;

            public UpdateSliderTwoCommandHandler(ISliderTwoRepository sliderTwoRepository, IMediator mediator)
            {
                _sliderTwoRepository = sliderTwoRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateSliderTwoValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateSliderTwoCommand request, CancellationToken cancellationToken)
            {
                var isThereSliderTwoRecord = await _sliderTwoRepository.GetAsync(u => u.SliderTwoId == request.SliderTwoId);


                isThereSliderTwoRecord.Title = request.Title;
                isThereSliderTwoRecord.Detay = request.Detay;
                isThereSliderTwoRecord.Price = request.Price;
                isThereSliderTwoRecord.DiscountPrice = request.DiscountPrice;
                isThereSliderTwoRecord.Photo = request.Photo;


                _sliderTwoRepository.Update(isThereSliderTwoRecord);
                await _sliderTwoRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

