
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
using Business.Handlers.Hediyeliks.ValidationRules;


namespace Business.Handlers.Hediyeliks.Commands
{


    public class UpdateHediyelikCommand : IRequest<IResult>
    {
        public int HediyelikId { get; set; }
        public string Photo { get; set; }
        public string Title { get; set; }
        public string Tag { get; set; }
        public string Price { get; set; }
        public string DiscountPrice { get; set; }

        public class UpdateHediyelikCommandHandler : IRequestHandler<UpdateHediyelikCommand, IResult>
        {
            private readonly IHediyelikRepository _hediyelikRepository;
            private readonly IMediator _mediator;

            public UpdateHediyelikCommandHandler(IHediyelikRepository hediyelikRepository, IMediator mediator)
            {
                _hediyelikRepository = hediyelikRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateHediyelikValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateHediyelikCommand request, CancellationToken cancellationToken)
            {
                var isThereHediyelikRecord = await _hediyelikRepository.GetAsync(u => u.HediyelikId == request.HediyelikId);


                isThereHediyelikRecord.Photo = request.Photo;
                isThereHediyelikRecord.Title = request.Title;
                isThereHediyelikRecord.Tag = request.Tag;
                isThereHediyelikRecord.Price = request.Price;
                isThereHediyelikRecord.DiscountPrice = request.DiscountPrice;


                _hediyelikRepository.Update(isThereHediyelikRecord);
                await _hediyelikRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

