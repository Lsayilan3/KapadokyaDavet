
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
using Business.Handlers.GalleryTwoes.ValidationRules;

namespace Business.Handlers.GalleryTwoes.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateGalleryTwoCommand : IRequest<IResult>
    {

        public string Photo { get; set; }


        public class CreateGalleryTwoCommandHandler : IRequestHandler<CreateGalleryTwoCommand, IResult>
        {
            private readonly IGalleryTwoRepository _galleryTwoRepository;
            private readonly IMediator _mediator;
            public CreateGalleryTwoCommandHandler(IGalleryTwoRepository galleryTwoRepository, IMediator mediator)
            {
                _galleryTwoRepository = galleryTwoRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateGalleryTwoValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateGalleryTwoCommand request, CancellationToken cancellationToken)
            {
                //var isThereGalleryTwoRecord = _galleryTwoRepository.Query().Any(u => u.Photo == request.Photo);

                //if (isThereGalleryTwoRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedGalleryTwo = new GalleryTwo
                {
                    Photo = request.Photo,

                };

                _galleryTwoRepository.Add(addedGalleryTwo);
                await _galleryTwoRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}