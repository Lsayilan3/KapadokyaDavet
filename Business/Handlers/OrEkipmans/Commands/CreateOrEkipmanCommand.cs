
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
using Business.Handlers.OrEkipmans.ValidationRules;

namespace Business.Handlers.OrEkipmans.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateOrEkipmanCommand : IRequest<IResult>
    {

        public string Photo { get; set; }
        public string Detay { get; set; }


        public class CreateOrEkipmanCommandHandler : IRequestHandler<CreateOrEkipmanCommand, IResult>
        {
            private readonly IOrEkipmanRepository _orEkipmanRepository;
            private readonly IMediator _mediator;
            public CreateOrEkipmanCommandHandler(IOrEkipmanRepository orEkipmanRepository, IMediator mediator)
            {
                _orEkipmanRepository = orEkipmanRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateOrEkipmanValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateOrEkipmanCommand request, CancellationToken cancellationToken)
            {
                //var isThereOrEkipmanRecord = _orEkipmanRepository.Query().Any(u => u.Photo == request.Photo);

                //if (isThereOrEkipmanRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedOrEkipman = new OrEkipman
                {
                    Photo = request.Photo,
                    Detay = request.Detay,

                };

                _orEkipmanRepository.Add(addedOrEkipman);
                await _orEkipmanRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}