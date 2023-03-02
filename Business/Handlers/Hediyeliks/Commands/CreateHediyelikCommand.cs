
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
using Business.Handlers.Hediyeliks.ValidationRules;

namespace Business.Handlers.Hediyeliks.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateHediyelikCommand : IRequest<IResult>
    {

        public string Photo { get; set; }
        public string Title { get; set; }
        public string Tag { get; set; }
        public string Price { get; set; }
        public string DiscountPrice { get; set; }


        public class CreateHediyelikCommandHandler : IRequestHandler<CreateHediyelikCommand, IResult>
        {
            private readonly IHediyelikRepository _hediyelikRepository;
            private readonly IMediator _mediator;
            public CreateHediyelikCommandHandler(IHediyelikRepository hediyelikRepository, IMediator mediator)
            {
                _hediyelikRepository = hediyelikRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateHediyelikValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateHediyelikCommand request, CancellationToken cancellationToken)
            {
                //var isThereHediyelikRecord = _hediyelikRepository.Query().Any(u => u.Photo == request.Photo);

                //if (isThereHediyelikRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedHediyelik = new Hediyelik
                {
                    Photo = request.Photo,
                    Title = request.Title,
                    Tag = request.Tag,
                    Price = request.Price,
                    DiscountPrice = request.DiscountPrice,

                };

                _hediyelikRepository.Add(addedHediyelik);
                await _hediyelikRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}