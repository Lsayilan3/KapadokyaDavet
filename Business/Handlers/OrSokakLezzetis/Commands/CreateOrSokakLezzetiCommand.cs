
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
using Business.Handlers.OrSokakLezzetis.ValidationRules;

namespace Business.Handlers.OrSokakLezzetis.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateOrSokakLezzetiCommand : IRequest<IResult>
    {

        public string Photo { get; set; }
        public string Detay { get; set; }


        public class CreateOrSokakLezzetiCommandHandler : IRequestHandler<CreateOrSokakLezzetiCommand, IResult>
        {
            private readonly IOrSokakLezzetiRepository _orSokakLezzetiRepository;
            private readonly IMediator _mediator;
            public CreateOrSokakLezzetiCommandHandler(IOrSokakLezzetiRepository orSokakLezzetiRepository, IMediator mediator)
            {
                _orSokakLezzetiRepository = orSokakLezzetiRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateOrSokakLezzetiValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateOrSokakLezzetiCommand request, CancellationToken cancellationToken)
            {
                //var isThereOrSokakLezzetiRecord = _orSokakLezzetiRepository.Query().Any(u => u.Photo == request.Photo);

                //if (isThereOrSokakLezzetiRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedOrSokakLezzeti = new OrSokakLezzeti
                {
                    Photo = request.Photo,
                    Detay = request.Detay,

                };

                _orSokakLezzetiRepository.Add(addedOrSokakLezzeti);
                await _orSokakLezzetiRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}