
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
using Business.Handlers.OrSunnets.ValidationRules;


namespace Business.Handlers.OrSunnets.Commands
{


    public class UpdateOrSunnetCommand : IRequest<IResult>
    {
        public int OrSunnetId { get; set; }
        public string Photo { get; set; }
        public string Detay { get; set; }

        public class UpdateOrSunnetCommandHandler : IRequestHandler<UpdateOrSunnetCommand, IResult>
        {
            private readonly IOrSunnetRepository _orSunnetRepository;
            private readonly IMediator _mediator;

            public UpdateOrSunnetCommandHandler(IOrSunnetRepository orSunnetRepository, IMediator mediator)
            {
                _orSunnetRepository = orSunnetRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateOrSunnetValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateOrSunnetCommand request, CancellationToken cancellationToken)
            {
                var isThereOrSunnetRecord = await _orSunnetRepository.GetAsync(u => u.OrSunnetId == request.OrSunnetId);


                isThereOrSunnetRecord.Photo = request.Photo;
                isThereOrSunnetRecord.Detay = request.Detay;


                _orSunnetRepository.Update(isThereOrSunnetRecord);
                await _orSunnetRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

