
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
using Business.Handlers.Cikolatas.ValidationRules;

namespace Business.Handlers.Cikolatas.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateCikolataCommand : IRequest<IResult>
    {

        public string Photo { get; set; }
        public string Title { get; set; }
        public string Tag { get; set; }
        public string Detay { get; set; }


        public class CreateCikolataCommandHandler : IRequestHandler<CreateCikolataCommand, IResult>
        {
            private readonly ICikolataRepository _cikolataRepository;
            private readonly IMediator _mediator;
            public CreateCikolataCommandHandler(ICikolataRepository cikolataRepository, IMediator mediator)
            {
                _cikolataRepository = cikolataRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateCikolataValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateCikolataCommand request, CancellationToken cancellationToken)
            {
                //var isThereCikolataRecord = _cikolataRepository.Query().Any(u => u.Photo == request.Photo);

                //if (isThereCikolataRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedCikolata = new Cikolata
                {
                    Photo = request.Photo,
                    Title = request.Title,
                    Tag = request.Tag,
                    Detay = request.Detay,

                };

                _cikolataRepository.Add(addedCikolata);
                await _cikolataRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}