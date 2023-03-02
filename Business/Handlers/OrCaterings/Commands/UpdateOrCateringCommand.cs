
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
using Business.Handlers.OrCaterings.ValidationRules;


namespace Business.Handlers.OrCaterings.Commands
{


    public class UpdateOrCateringCommand : IRequest<IResult>
    {
        public int OrCateringId { get; set; }
        public string Photo { get; set; }
        public string Detay { get; set; }

        public class UpdateOrCateringCommandHandler : IRequestHandler<UpdateOrCateringCommand, IResult>
        {
            private readonly IOrCateringRepository _orCateringRepository;
            private readonly IMediator _mediator;

            public UpdateOrCateringCommandHandler(IOrCateringRepository orCateringRepository, IMediator mediator)
            {
                _orCateringRepository = orCateringRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateOrCateringValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateOrCateringCommand request, CancellationToken cancellationToken)
            {
                var isThereOrCateringRecord = await _orCateringRepository.GetAsync(u => u.OrCateringId == request.OrCateringId);


                isThereOrCateringRecord.Photo = request.Photo;
                isThereOrCateringRecord.Detay = request.Detay;


                _orCateringRepository.Update(isThereOrCateringRecord);
                await _orCateringRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

