
using Business.Handlers.GalleryTwoes.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.GalleryTwoes.Queries.GetGalleryTwoQuery;
using Entities.Concrete;
using static Business.Handlers.GalleryTwoes.Queries.GetGalleryTwoesQuery;
using static Business.Handlers.GalleryTwoes.Commands.CreateGalleryTwoCommand;
using Business.Handlers.GalleryTwoes.Commands;
using Business.Constants;
using static Business.Handlers.GalleryTwoes.Commands.UpdateGalleryTwoCommand;
using static Business.Handlers.GalleryTwoes.Commands.DeleteGalleryTwoCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class GalleryTwoHandlerTests
    {
        Mock<IGalleryTwoRepository> _galleryTwoRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _galleryTwoRepository = new Mock<IGalleryTwoRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task GalleryTwo_GetQuery_Success()
        {
            //Arrange
            var query = new GetGalleryTwoQuery();

            _galleryTwoRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<GalleryTwo, bool>>>())).ReturnsAsync(new GalleryTwo()
//propertyler buraya yazılacak
//{																		
//GalleryTwoId = 1,
//GalleryTwoName = "Test"
//}
);

            var handler = new GetGalleryTwoQueryHandler(_galleryTwoRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.GalleryTwoId.Should().Be(1);

        }

        [Test]
        public async Task GalleryTwo_GetQueries_Success()
        {
            //Arrange
            var query = new GetGalleryTwoesQuery();

            _galleryTwoRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<GalleryTwo, bool>>>()))
                        .ReturnsAsync(new List<GalleryTwo> { new GalleryTwo() { /*TODO:propertyler buraya yazılacak GalleryTwoId = 1, GalleryTwoName = "test"*/ } });

            var handler = new GetGalleryTwoesQueryHandler(_galleryTwoRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<GalleryTwo>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task GalleryTwo_CreateCommand_Success()
        {
            GalleryTwo rt = null;
            //Arrange
            var command = new CreateGalleryTwoCommand();
            //propertyler buraya yazılacak
            //command.GalleryTwoName = "deneme";

            _galleryTwoRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<GalleryTwo, bool>>>()))
                        .ReturnsAsync(rt);

            _galleryTwoRepository.Setup(x => x.Add(It.IsAny<GalleryTwo>())).Returns(new GalleryTwo());

            var handler = new CreateGalleryTwoCommandHandler(_galleryTwoRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _galleryTwoRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task GalleryTwo_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateGalleryTwoCommand();
            //propertyler buraya yazılacak 
            //command.GalleryTwoName = "test";

            _galleryTwoRepository.Setup(x => x.Query())
                                           .Returns(new List<GalleryTwo> { new GalleryTwo() { /*TODO:propertyler buraya yazılacak GalleryTwoId = 1, GalleryTwoName = "test"*/ } }.AsQueryable());

            _galleryTwoRepository.Setup(x => x.Add(It.IsAny<GalleryTwo>())).Returns(new GalleryTwo());

            var handler = new CreateGalleryTwoCommandHandler(_galleryTwoRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task GalleryTwo_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateGalleryTwoCommand();
            //command.GalleryTwoName = "test";

            _galleryTwoRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<GalleryTwo, bool>>>()))
                        .ReturnsAsync(new GalleryTwo() { /*TODO:propertyler buraya yazılacak GalleryTwoId = 1, GalleryTwoName = "deneme"*/ });

            _galleryTwoRepository.Setup(x => x.Update(It.IsAny<GalleryTwo>())).Returns(new GalleryTwo());

            var handler = new UpdateGalleryTwoCommandHandler(_galleryTwoRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _galleryTwoRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task GalleryTwo_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteGalleryTwoCommand();

            _galleryTwoRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<GalleryTwo, bool>>>()))
                        .ReturnsAsync(new GalleryTwo() { /*TODO:propertyler buraya yazılacak GalleryTwoId = 1, GalleryTwoName = "deneme"*/});

            _galleryTwoRepository.Setup(x => x.Delete(It.IsAny<GalleryTwo>()));

            var handler = new DeleteGalleryTwoCommandHandler(_galleryTwoRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _galleryTwoRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

