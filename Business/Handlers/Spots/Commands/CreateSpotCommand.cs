
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
using Business.Handlers.Spots.ValidationRules;

namespace Business.Handlers.Spots.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateSpotCommand : IRequest<IResult>
    {

        public int CategoryId { get; set; }
        public string Photo { get; set; }
        public string Title { get; set; }
        public string Tag { get; set; }
        public string Price { get; set; }
        public string DiscountPrice { get; set; }


        public class CreateSpotCommandHandler : IRequestHandler<CreateSpotCommand, IResult>
        {
            private readonly ISpotRepository _spotRepository;
            private readonly IMediator _mediator;
            public CreateSpotCommandHandler(ISpotRepository spotRepository, IMediator mediator)
            {
                _spotRepository = spotRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateSpotValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateSpotCommand request, CancellationToken cancellationToken)
            {
                //var isThereSpotRecord = _spotRepository.Query().Any(u => u.CategoryId == request.CategoryId);

                //if (isThereSpotRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedSpot = new Spot
                {
                    CategoryId = request.CategoryId,
                    Photo = request.Photo,
                    Title = request.Title,
                    Tag = request.Tag,
                    Price = request.Price,
                    DiscountPrice = request.DiscountPrice,

                };

                _spotRepository.Add(addedSpot);
                await _spotRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}