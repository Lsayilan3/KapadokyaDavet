
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
using Business.Handlers.OrBabies.ValidationRules;


namespace Business.Handlers.OrBabies.Commands
{


    public class UpdateOrBabyCommand : IRequest<IResult>
    {
        public int OrBabyId { get; set; }
        public string Photo { get; set; }
        public string Detay { get; set; }

        public class UpdateOrBabyCommandHandler : IRequestHandler<UpdateOrBabyCommand, IResult>
        {
            private readonly IOrBabyRepository _orBabyRepository;
            private readonly IMediator _mediator;

            public UpdateOrBabyCommandHandler(IOrBabyRepository orBabyRepository, IMediator mediator)
            {
                _orBabyRepository = orBabyRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateOrBabyValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateOrBabyCommand request, CancellationToken cancellationToken)
            {
                var isThereOrBabyRecord = await _orBabyRepository.GetAsync(u => u.OrBabyId == request.OrBabyId);


                isThereOrBabyRecord.Photo = request.Photo;
                isThereOrBabyRecord.Detay = request.Detay;


                _orBabyRepository.Update(isThereOrBabyRecord);
                await _orBabyRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

