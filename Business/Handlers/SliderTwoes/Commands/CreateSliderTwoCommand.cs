
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Business.Handlers.SliderTwoes.ValidationRules;

namespace Business.Handlers.SliderTwoes.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateSliderTwoCommand : IRequest<IResult>
    {

        public string Title { get; set; }
        public string Detay { get; set; }
        public int Price { get; set; }
        public int DiscountPrice { get; set; }
        public string Photo { get; set; }


        public class CreateSliderTwoCommandHandler : IRequestHandler<CreateSliderTwoCommand, IResult>
        {
            private readonly ISliderTwoRepository _sliderTwoRepository;
            private readonly IMediator _mediator;
            public CreateSliderTwoCommandHandler(ISliderTwoRepository sliderTwoRepository, IMediator mediator)
            {
                _sliderTwoRepository = sliderTwoRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateSliderTwoValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateSliderTwoCommand request, CancellationToken cancellationToken)
            {
                //var isThereSliderTwoRecord = _sliderTwoRepository.Query().Any(u => u.Title == request.Title);

                //if (isThereSliderTwoRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedSliderTwo = new SliderTwo
                {
                    Title = request.Title,
                    Detay = request.Detay,
                    Price = request.Price,
                    DiscountPrice = request.DiscountPrice,
                    Photo = request.Photo,

                };

                _sliderTwoRepository.Add(addedSliderTwo);
                await _sliderTwoRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}