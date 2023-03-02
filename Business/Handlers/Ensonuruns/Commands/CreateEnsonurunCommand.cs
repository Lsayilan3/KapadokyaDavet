
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
using Business.Handlers.Ensonuruns.ValidationRules;

namespace Business.Handlers.Ensonuruns.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateEnsonurunCommand : IRequest<IResult>
    {

        public string Photo { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }


        public class CreateEnsonurunCommandHandler : IRequestHandler<CreateEnsonurunCommand, IResult>
        {
            private readonly IEnsonurunRepository _ensonurunRepository;
            private readonly IMediator _mediator;
            public CreateEnsonurunCommandHandler(IEnsonurunRepository ensonurunRepository, IMediator mediator)
            {
                _ensonurunRepository = ensonurunRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateEnsonurunValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateEnsonurunCommand request, CancellationToken cancellationToken)
            {
                //var isThereEnsonurunRecord = _ensonurunRepository.Query().Any(u => u.Photo == request.Photo);

                //if (isThereEnsonurunRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedEnsonurun = new Ensonurun
                {
                    Photo = request.Photo,
                    Title = request.Title,
                    Url = request.Url,

                };

                _ensonurunRepository.Add(addedEnsonurun);
                await _ensonurunRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}