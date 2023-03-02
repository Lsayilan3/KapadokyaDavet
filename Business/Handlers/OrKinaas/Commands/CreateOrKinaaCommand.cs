
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Business.Handlers.OrKinaas.ValidationRules;

namespace Business.Handlers.OrKinaas.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateOrKinaaCommand : IRequest<IResult>
    {

        public string Photo { get; set; }
        public string Detay { get; set; }


        public class CreateOrKinaaCommandHandler : IRequestHandler<CreateOrKinaaCommand, IResult>
        {
            private readonly IOrKinaaRepository _orKinaaRepository;
            private readonly IMediator _mediator;
            public CreateOrKinaaCommandHandler(IOrKinaaRepository orKinaaRepository, IMediator mediator)
            {
                _orKinaaRepository = orKinaaRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateOrKinaaValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateOrKinaaCommand request, CancellationToken cancellationToken)
            {
                //var isThereOrKinaaRecord = _orKinaaRepository.Query().Any(u => u.Photo == request.Photo);

                //if (isThereOrKinaaRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedOrKinaa = new OrKinaa
                {
                    Photo = request.Photo,
                    Detay = request.Detay,

                };

                _orKinaaRepository.Add(addedOrKinaa);
                await _orKinaaRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}