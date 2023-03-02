
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
using Business.Handlers.Muziks.ValidationRules;

namespace Business.Handlers.Muziks.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateMuzikCommand : IRequest<IResult>
    {

        public string Photo { get; set; }
        public string Title { get; set; }
        public string Tag { get; set; }
        public string Detay { get; set; }


        public class CreateMuzikCommandHandler : IRequestHandler<CreateMuzikCommand, IResult>
        {
            private readonly IMuzikRepository _muzikRepository;
            private readonly IMediator _mediator;
            public CreateMuzikCommandHandler(IMuzikRepository muzikRepository, IMediator mediator)
            {
                _muzikRepository = muzikRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateMuzikValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateMuzikCommand request, CancellationToken cancellationToken)
            {
                //var isThereMuzikRecord = _muzikRepository.Query().Any(u => u.Photo == request.Photo);

                //if (isThereMuzikRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedMuzik = new Muzik
                {
                    Photo = request.Photo,
                    Title = request.Title,
                    Tag = request.Tag,
                    Detay = request.Detay,

                };

                _muzikRepository.Add(addedMuzik);
                await _muzikRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}