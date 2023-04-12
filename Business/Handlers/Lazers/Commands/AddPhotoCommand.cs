
using Business.Constants;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Core.Aspects.Autofac.Validation;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Business.Handlers.Ensonuruns.Queries;
using Business.Handlers.GalleryTwoes.Queries;
using Business.Handlers.Hediyeliks.Queries;
using Business.Handlers.Lazers.Queries;

namespace Business.Handlers.Lazers.Commands
{


    public class AddPhotoCommad : IRequest<IResult>
    {
        public int LazerId { get; set; }

        public IFormFile File { get; set; }

        public class AddPhotoCommadHandler : IRequestHandler<AddPhotoCommad, IResult>
        {
            private readonly IMediator _mediator;
            IHostingEnvironment _hostingEnvironment;
            public AddPhotoCommadHandler(IMediator mediator, IHostingEnvironment hostingEnvironment)
            {
                _mediator = mediator;
                _hostingEnvironment = hostingEnvironment;
            }


            //[SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(AddPhotoCommad request, CancellationToken cancellationToken)
            {
                var result = await _mediator.Send(new GetLazerQuery { LazerId = request.LazerId });
                if (request.File.Length > 0)
                {
                    string folderPath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads/laser");
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    string filePath = Path.Combine(folderPath, request.File.FileName);

                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await request.File.CopyToAsync(fileStream);
                    }
                    result.Data.Photo = "/uploads/laser/" + request.File.FileName;
                    /*myClass.Photo = "/uploads/" + file.FileName; */
                    var upResult = await _mediator.Send(new UpdateLazerCommand()
                    {
                        LazerId = result.Data.LazerId,
                        Title = result.Data.Title,
                        Tag = result.Data.Tag,
                        Price = result.Data.Price,
                        DiscountPrice = result.Data.DiscountPrice,
                        Photo = result.Data.Photo,
                    });
                }
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

