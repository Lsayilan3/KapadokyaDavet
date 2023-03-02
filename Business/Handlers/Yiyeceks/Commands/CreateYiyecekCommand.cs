
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
using Business.Handlers.Yiyeceks.ValidationRules;

namespace Business.Handlers.Yiyeceks.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateYiyecekCommand : IRequest<IResult>
    {

        public string Photo { get; set; }
        public string Title { get; set; }
        public string Tag { get; set; }
        public string Price { get; set; }
        public string DiscountPrice { get; set; }


        public class CreateYiyecekCommandHandler : IRequestHandler<CreateYiyecekCommand, IResult>
        {
            private readonly IYiyecekRepository _yiyecekRepository;
            private readonly IMediator _mediator;
            public CreateYiyecekCommandHandler(IYiyecekRepository yiyecekRepository, IMediator mediator)
            {
                _yiyecekRepository = yiyecekRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateYiyecekValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateYiyecekCommand request, CancellationToken cancellationToken)
            {
                //var isThereYiyecekRecord = _yiyecekRepository.Query().Any(u => u.Photo == request.Photo);

                //if (isThereYiyecekRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedYiyecek = new Yiyecek
                {
                    Photo = request.Photo,
                    Title = request.Title,
                    Tag = request.Tag,
                    Price = request.Price,
                    DiscountPrice = request.DiscountPrice,

                };

                _yiyecekRepository.Add(addedYiyecek);
                await _yiyecekRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}