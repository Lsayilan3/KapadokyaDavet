
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
using Business.Handlers.Spots.ValidationRules;


namespace Business.Handlers.Spots.Commands
{


    public class UpdateSpotCommand : IRequest<IResult>
    {
        public int SpotId { get; set; }
        public int CategoryId { get; set; }
        public string Photo { get; set; }
        public string Title { get; set; }
        public string Tag { get; set; }
        public string Price { get; set; }
        public string DiscountPrice { get; set; }

        public class UpdateSpotCommandHandler : IRequestHandler<UpdateSpotCommand, IResult>
        {
            private readonly ISpotRepository _spotRepository;
            private readonly IMediator _mediator;

            public UpdateSpotCommandHandler(ISpotRepository spotRepository, IMediator mediator)
            {
                _spotRepository = spotRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateSpotValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateSpotCommand request, CancellationToken cancellationToken)
            {
                var isThereSpotRecord = await _spotRepository.GetAsync(u => u.SpotId == request.SpotId);


                isThereSpotRecord.CategoryId = request.CategoryId;
                isThereSpotRecord.Photo = request.Photo;
                isThereSpotRecord.Title = request.Title;
                isThereSpotRecord.Tag = request.Tag;
                isThereSpotRecord.Price = request.Price;
                isThereSpotRecord.DiscountPrice = request.DiscountPrice;


                _spotRepository.Update(isThereSpotRecord);
                await _spotRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

