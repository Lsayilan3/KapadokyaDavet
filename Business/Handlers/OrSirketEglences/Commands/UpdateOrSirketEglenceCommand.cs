
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
using Business.Handlers.OrSirketEglences.ValidationRules;


namespace Business.Handlers.OrSirketEglences.Commands
{


    public class UpdateOrSirketEglenceCommand : IRequest<IResult>
    {
        public int OrSirketEglenceId { get; set; }
        public string Photo { get; set; }
        public string Detay { get; set; }

        public class UpdateOrSirketEglenceCommandHandler : IRequestHandler<UpdateOrSirketEglenceCommand, IResult>
        {
            private readonly IOrSirketEglenceRepository _orSirketEglenceRepository;
            private readonly IMediator _mediator;

            public UpdateOrSirketEglenceCommandHandler(IOrSirketEglenceRepository orSirketEglenceRepository, IMediator mediator)
            {
                _orSirketEglenceRepository = orSirketEglenceRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateOrSirketEglenceValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateOrSirketEglenceCommand request, CancellationToken cancellationToken)
            {
                var isThereOrSirketEglenceRecord = await _orSirketEglenceRepository.GetAsync(u => u.OrSirketEglenceId == request.OrSirketEglenceId);


                isThereOrSirketEglenceRecord.Photo = request.Photo;
                isThereOrSirketEglenceRecord.Detay = request.Detay;


                _orSirketEglenceRepository.Update(isThereOrSirketEglenceRecord);
                await _orSirketEglenceRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

