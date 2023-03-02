
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
using Business.Handlers.OrTrioEkibis.ValidationRules;

namespace Business.Handlers.OrTrioEkibis.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateOrTrioEkibiCommand : IRequest<IResult>
    {

        public string Photo { get; set; }
        public string Detay { get; set; }


        public class CreateOrTrioEkibiCommandHandler : IRequestHandler<CreateOrTrioEkibiCommand, IResult>
        {
            private readonly IOrTrioEkibiRepository _orTrioEkibiRepository;
            private readonly IMediator _mediator;
            public CreateOrTrioEkibiCommandHandler(IOrTrioEkibiRepository orTrioEkibiRepository, IMediator mediator)
            {
                _orTrioEkibiRepository = orTrioEkibiRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateOrTrioEkibiValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateOrTrioEkibiCommand request, CancellationToken cancellationToken)
            {
                //var isThereOrTrioEkibiRecord = _orTrioEkibiRepository.Query().Any(u => u.Photo == request.Photo);

                //if (isThereOrTrioEkibiRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedOrTrioEkibi = new OrTrioEkibi
                {
                    Photo = request.Photo,
                    Detay = request.Detay,

                };

                _orTrioEkibiRepository.Add(addedOrTrioEkibi);
                await _orTrioEkibiRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}