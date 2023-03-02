
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
using Business.Handlers.OrDuguns.ValidationRules;

namespace Business.Handlers.OrDuguns.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateOrDugunCommand : IRequest<IResult>
    {

        public string Photo { get; set; }
        public string Detay { get; set; }


        public class CreateOrDugunCommandHandler : IRequestHandler<CreateOrDugunCommand, IResult>
        {
            private readonly IOrDugunRepository _orDugunRepository;
            private readonly IMediator _mediator;
            public CreateOrDugunCommandHandler(IOrDugunRepository orDugunRepository, IMediator mediator)
            {
                _orDugunRepository = orDugunRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateOrDugunValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateOrDugunCommand request, CancellationToken cancellationToken)
            {
                //var isThereOrDugunRecord = _orDugunRepository.Query().Any(u => u.Photo == request.Photo);

                //if (isThereOrDugunRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedOrDugun = new OrDugun
                {
                    Photo = request.Photo,
                    Detay = request.Detay,

                };

                _orDugunRepository.Add(addedOrDugun);
                await _orDugunRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}