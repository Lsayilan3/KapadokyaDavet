
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
using Business.Handlers.OrNisans.ValidationRules;


namespace Business.Handlers.OrNisans.Commands
{


    public class UpdateOrNisanCommand : IRequest<IResult>
    {
        public int OrNisanId { get; set; }
        public string Photo { get; set; }
        public string Detay { get; set; }

        public class UpdateOrNisanCommandHandler : IRequestHandler<UpdateOrNisanCommand, IResult>
        {
            private readonly IOrNisanRepository _orNisanRepository;
            private readonly IMediator _mediator;

            public UpdateOrNisanCommandHandler(IOrNisanRepository orNisanRepository, IMediator mediator)
            {
                _orNisanRepository = orNisanRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateOrNisanValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateOrNisanCommand request, CancellationToken cancellationToken)
            {
                var isThereOrNisanRecord = await _orNisanRepository.GetAsync(u => u.OrNisanId == request.OrNisanId);


                isThereOrNisanRecord.Photo = request.Photo;
                isThereOrNisanRecord.Detay = request.Detay;


                _orNisanRepository.Update(isThereOrNisanRecord);
                await _orNisanRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

