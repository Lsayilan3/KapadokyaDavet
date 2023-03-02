
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
using Business.Handlers.OrKinaas.ValidationRules;


namespace Business.Handlers.OrKinaas.Commands
{


    public class UpdateOrKinaaCommand : IRequest<IResult>
    {
        public int OrKinaaId { get; set; }
        public string Photo { get; set; }
        public string Detay { get; set; }

        public class UpdateOrKinaaCommandHandler : IRequestHandler<UpdateOrKinaaCommand, IResult>
        {
            private readonly IOrKinaaRepository _orKinaaRepository;
            private readonly IMediator _mediator;

            public UpdateOrKinaaCommandHandler(IOrKinaaRepository orKinaaRepository, IMediator mediator)
            {
                _orKinaaRepository = orKinaaRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateOrKinaaValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateOrKinaaCommand request, CancellationToken cancellationToken)
            {
                var isThereOrKinaaRecord = await _orKinaaRepository.GetAsync(u => u.OrKinaaId == request.OrKinaaId);


                isThereOrKinaaRecord.Photo = request.Photo;
                isThereOrKinaaRecord.Detay = request.Detay;


                _orKinaaRepository.Update(isThereOrKinaaRecord);
                await _orKinaaRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

