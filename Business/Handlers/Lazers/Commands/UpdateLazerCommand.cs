
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
using Business.Handlers.Lazers.ValidationRules;


namespace Business.Handlers.Lazers.Commands
{


    public class UpdateLazerCommand : IRequest<IResult>
    {
        public int LazerId { get; set; }
        public string Photo { get; set; }
        public string Title { get; set; }
        public string Tag { get; set; }
        public string Price { get; set; }
        public string DiscountPrice { get; set; }

        public class UpdateLazerCommandHandler : IRequestHandler<UpdateLazerCommand, IResult>
        {
            private readonly ILazerRepository _lazerRepository;
            private readonly IMediator _mediator;

            public UpdateLazerCommandHandler(ILazerRepository lazerRepository, IMediator mediator)
            {
                _lazerRepository = lazerRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateLazerValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateLazerCommand request, CancellationToken cancellationToken)
            {
                var isThereLazerRecord = await _lazerRepository.GetAsync(u => u.LazerId == request.LazerId);


                isThereLazerRecord.Photo = request.Photo;
                isThereLazerRecord.Title = request.Title;
                isThereLazerRecord.Tag = request.Tag;
                isThereLazerRecord.Price = request.Price;
                isThereLazerRecord.DiscountPrice = request.DiscountPrice;


                _lazerRepository.Update(isThereLazerRecord);
                await _lazerRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

