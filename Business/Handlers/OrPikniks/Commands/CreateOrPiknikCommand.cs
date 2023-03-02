
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
using Business.Handlers.OrPikniks.ValidationRules;

namespace Business.Handlers.OrPikniks.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateOrPiknikCommand : IRequest<IResult>
    {

        public string Photo { get; set; }
        public string Detay { get; set; }


        public class CreateOrPiknikCommandHandler : IRequestHandler<CreateOrPiknikCommand, IResult>
        {
            private readonly IOrPiknikRepository _orPiknikRepository;
            private readonly IMediator _mediator;
            public CreateOrPiknikCommandHandler(IOrPiknikRepository orPiknikRepository, IMediator mediator)
            {
                _orPiknikRepository = orPiknikRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateOrPiknikValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateOrPiknikCommand request, CancellationToken cancellationToken)
            {
                //var isThereOrPiknikRecord = _orPiknikRepository.Query().Any(u => u.Photo == request.Photo);

                //if (isThereOrPiknikRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedOrPiknik = new OrPiknik
                {
                    Photo = request.Photo,
                    Detay = request.Detay,

                };

                _orPiknikRepository.Add(addedOrPiknik);
                await _orPiknikRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}