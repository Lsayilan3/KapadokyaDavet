
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
using Business.Handlers.SpotCategoryies.ValidationRules;


namespace Business.Handlers.SpotCategoryies.Commands
{


    public class UpdateSpotCategoryyCommand : IRequest<IResult>
    {
        public int SpotCategoryyId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public class UpdateSpotCategoryyCommandHandler : IRequestHandler<UpdateSpotCategoryyCommand, IResult>
        {
            private readonly ISpotCategoryyRepository _spotCategoryyRepository;
            private readonly IMediator _mediator;

            public UpdateSpotCategoryyCommandHandler(ISpotCategoryyRepository spotCategoryyRepository, IMediator mediator)
            {
                _spotCategoryyRepository = spotCategoryyRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateSpotCategoryyValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateSpotCategoryyCommand request, CancellationToken cancellationToken)
            {
                var isThereSpotCategoryyRecord = await _spotCategoryyRepository.GetAsync(u => u.SpotCategoryyId == request.SpotCategoryyId);


                isThereSpotCategoryyRecord.CategoryId = request.CategoryId;
                isThereSpotCategoryyRecord.CategoryName = request.CategoryName;


                _spotCategoryyRepository.Update(isThereSpotCategoryyRecord);
                await _spotCategoryyRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

