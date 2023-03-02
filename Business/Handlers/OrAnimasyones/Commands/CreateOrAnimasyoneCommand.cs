
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
using Business.Handlers.OrAnimasyones.ValidationRules;

namespace Business.Handlers.OrAnimasyones.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateOrAnimasyoneCommand : IRequest<IResult>
    {

        public string Photo { get; set; }
        public string Detay { get; set; }


        public class CreateOrAnimasyoneCommandHandler : IRequestHandler<CreateOrAnimasyoneCommand, IResult>
        {
            private readonly IOrAnimasyoneRepository _orAnimasyoneRepository;
            private readonly IMediator _mediator;
            public CreateOrAnimasyoneCommandHandler(IOrAnimasyoneRepository orAnimasyoneRepository, IMediator mediator)
            {
                _orAnimasyoneRepository = orAnimasyoneRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateOrAnimasyoneValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateOrAnimasyoneCommand request, CancellationToken cancellationToken)
            {
                //var isThereOrAnimasyoneRecord = _orAnimasyoneRepository.Query().Any(u => u.Photo == request.Photo);

                //if (isThereOrAnimasyoneRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedOrAnimasyone = new OrAnimasyone
                {
                    Photo = request.Photo,
                    Detay = request.Detay,

                };

                _orAnimasyoneRepository.Add(addedOrAnimasyone);
                await _orAnimasyoneRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}