
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
using Business.Handlers.Yiyeceks.ValidationRules;


namespace Business.Handlers.Yiyeceks.Commands
{


    public class UpdateYiyecekCommand : IRequest<IResult>
    {
        public int YiyecekId { get; set; }
        public string Photo { get; set; }
        public string Title { get; set; }
        public string Tag { get; set; }
        public string Price { get; set; }
        public string DiscountPrice { get; set; }

        public class UpdateYiyecekCommandHandler : IRequestHandler<UpdateYiyecekCommand, IResult>
        {
            private readonly IYiyecekRepository _yiyecekRepository;
            private readonly IMediator _mediator;

            public UpdateYiyecekCommandHandler(IYiyecekRepository yiyecekRepository, IMediator mediator)
            {
                _yiyecekRepository = yiyecekRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateYiyecekValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateYiyecekCommand request, CancellationToken cancellationToken)
            {
                var isThereYiyecekRecord = await _yiyecekRepository.GetAsync(u => u.YiyecekId == request.YiyecekId);


                isThereYiyecekRecord.Photo = request.Photo;
                isThereYiyecekRecord.Title = request.Title;
                isThereYiyecekRecord.Tag = request.Tag;
                isThereYiyecekRecord.Price = request.Price;
                isThereYiyecekRecord.DiscountPrice = request.DiscountPrice;


                _yiyecekRepository.Update(isThereYiyecekRecord);
                await _yiyecekRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

