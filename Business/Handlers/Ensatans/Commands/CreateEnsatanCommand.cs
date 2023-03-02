
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
using Business.Handlers.Ensatans.ValidationRules;

namespace Business.Handlers.Ensatans.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateEnsatanCommand : IRequest<IResult>
    {

        public string Photo { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }


        public class CreateEnsatanCommandHandler : IRequestHandler<CreateEnsatanCommand, IResult>
        {
            private readonly IEnsatanRepository _ensatanRepository;
            private readonly IMediator _mediator;
            public CreateEnsatanCommandHandler(IEnsatanRepository ensatanRepository, IMediator mediator)
            {
                _ensatanRepository = ensatanRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateEnsatanValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateEnsatanCommand request, CancellationToken cancellationToken)
            {
                //var isThereEnsatanRecord = _ensatanRepository.Query().Any(u => u.Photo == request.Photo);

                //if (isThereEnsatanRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedEnsatan = new Ensatan
                {
                    Photo = request.Photo,
                    Title = request.Title,
                    Url = request.Url,

                };

                _ensatanRepository.Add(addedEnsatan);
                await _ensatanRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}