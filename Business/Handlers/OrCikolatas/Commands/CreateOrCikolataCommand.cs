
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
using Business.Handlers.OrCikolatas.ValidationRules;

namespace Business.Handlers.OrCikolatas.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateOrCikolataCommand : IRequest<IResult>
    {

        public string Photo { get; set; }
        public string Title { get; set; }
        public string Tag { get; set; }
        public string Detay { get; set; }


        public class CreateOrCikolataCommandHandler : IRequestHandler<CreateOrCikolataCommand, IResult>
        {
            private readonly IOrCikolataRepository _orCikolataRepository;
            private readonly IMediator _mediator;
            public CreateOrCikolataCommandHandler(IOrCikolataRepository orCikolataRepository, IMediator mediator)
            {
                _orCikolataRepository = orCikolataRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateOrCikolataValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateOrCikolataCommand request, CancellationToken cancellationToken)
            {
                //var isThereOrCikolataRecord = _orCikolataRepository.Query().Any(u => u.Photo == request.Photo);

                //if (isThereOrCikolataRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedOrCikolata = new OrCikolata
                {
                    Photo = request.Photo,
                    Title = request.Title,
                    Tag = request.Tag,
                    Detay = request.Detay,

                };

                _orCikolataRepository.Add(addedOrCikolata);
                await _orCikolataRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}