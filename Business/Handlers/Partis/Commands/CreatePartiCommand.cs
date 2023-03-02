
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
using Business.Handlers.Partis.ValidationRules;

namespace Business.Handlers.Partis.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreatePartiCommand : IRequest<IResult>
    {

        public string Photo { get; set; }
        public string Title { get; set; }
        public string Tag { get; set; }
        public string Price { get; set; }
        public string DiscountPrice { get; set; }


        public class CreatePartiCommandHandler : IRequestHandler<CreatePartiCommand, IResult>
        {
            private readonly IPartiRepository _partiRepository;
            private readonly IMediator _mediator;
            public CreatePartiCommandHandler(IPartiRepository partiRepository, IMediator mediator)
            {
                _partiRepository = partiRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreatePartiValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreatePartiCommand request, CancellationToken cancellationToken)
            {
                //var isTherePartiRecord = _partiRepository.Query().Any(u => u.Photo == request.Photo);

                //if (isTherePartiRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedParti = new Parti
                {
                    Photo = request.Photo,
                    Title = request.Title,
                    Tag = request.Tag,
                    Price = request.Price,
                    DiscountPrice = request.DiscountPrice,

                };

                _partiRepository.Add(addedParti);
                await _partiRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}