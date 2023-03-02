
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
using Business.Handlers.Lazers.ValidationRules;

namespace Business.Handlers.Lazers.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateLazerCommand : IRequest<IResult>
    {

        public string Photo { get; set; }
        public string Title { get; set; }
        public string Tag { get; set; }
        public string Price { get; set; }
        public string DiscountPrice { get; set; }


        public class CreateLazerCommandHandler : IRequestHandler<CreateLazerCommand, IResult>
        {
            private readonly ILazerRepository _lazerRepository;
            private readonly IMediator _mediator;
            public CreateLazerCommandHandler(ILazerRepository lazerRepository, IMediator mediator)
            {
                _lazerRepository = lazerRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateLazerValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateLazerCommand request, CancellationToken cancellationToken)
            {
                //var isThereLazerRecord = _lazerRepository.Query().Any(u => u.Photo == request.Photo);

                //if (isThereLazerRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedLazer = new Lazer
                {
                    Photo = request.Photo,
                    Title = request.Title,
                    Tag = request.Tag,
                    Price = request.Price,
                    DiscountPrice = request.DiscountPrice,

                };

                _lazerRepository.Add(addedLazer);
                await _lazerRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}