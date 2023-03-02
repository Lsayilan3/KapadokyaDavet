
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
using Business.Handlers.OrDuguns.ValidationRules;


namespace Business.Handlers.OrDuguns.Commands
{


    public class UpdateOrDugunCommand : IRequest<IResult>
    {
        public int OrDugunId { get; set; }
        public string Photo { get; set; }
        public string Detay { get; set; }

        public class UpdateOrDugunCommandHandler : IRequestHandler<UpdateOrDugunCommand, IResult>
        {
            private readonly IOrDugunRepository _orDugunRepository;
            private readonly IMediator _mediator;

            public UpdateOrDugunCommandHandler(IOrDugunRepository orDugunRepository, IMediator mediator)
            {
                _orDugunRepository = orDugunRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateOrDugunValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateOrDugunCommand request, CancellationToken cancellationToken)
            {
                var isThereOrDugunRecord = await _orDugunRepository.GetAsync(u => u.OrDugunId == request.OrDugunId);


                isThereOrDugunRecord.Photo = request.Photo;
                isThereOrDugunRecord.Detay = request.Detay;


                _orDugunRepository.Update(isThereOrDugunRecord);
                await _orDugunRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

