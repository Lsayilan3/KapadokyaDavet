
using Business.Handlers.Spots.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Spots.Queries.GetSpotQuery;
using Entities.Concrete;
using static Business.Handlers.Spots.Queries.GetSpotsQuery;
using static Business.Handlers.Spots.Commands.CreateSpotCommand;
using Business.Handlers.Spots.Commands;
using Business.Constants;
using static Business.Handlers.Spots.Commands.UpdateSpotCommand;
using static Business.Handlers.Spots.Commands.DeleteSpotCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class SpotHandlerTests
    {
        Mock<ISpotRepository> _spotRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _spotRepository = new Mock<ISpotRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Spot_GetQuery_Success()
        {
            //Arrange
            var query = new GetSpotQuery();

            _spotRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Spot, bool>>>())).ReturnsAsync(new Spot()
//propertyler buraya yazılacak
//{																		
//SpotId = 1,
//SpotName = "Test"
//}
);

            var handler = new GetSpotQueryHandler(_spotRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.SpotId.Should().Be(1);

        }

        [Test]
        public async Task Spot_GetQueries_Success()
        {
            //Arrange
            var query = new GetSpotsQuery();

            _spotRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Spot, bool>>>()))
                        .ReturnsAsync(new List<Spot> { new Spot() { /*TODO:propertyler buraya yazılacak SpotId = 1, SpotName = "test"*/ } });

            var handler = new GetSpotsQueryHandler(_spotRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Spot>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Spot_CreateCommand_Success()
        {
            Spot rt = null;
            //Arrange
            var command = new CreateSpotCommand();
            //propertyler buraya yazılacak
            //command.SpotName = "deneme";

            _spotRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Spot, bool>>>()))
                        .ReturnsAsync(rt);

            _spotRepository.Setup(x => x.Add(It.IsAny<Spot>())).Returns(new Spot());

            var handler = new CreateSpotCommandHandler(_spotRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _spotRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Spot_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateSpotCommand();
            //propertyler buraya yazılacak 
            //command.SpotName = "test";

            _spotRepository.Setup(x => x.Query())
                                           .Returns(new List<Spot> { new Spot() { /*TODO:propertyler buraya yazılacak SpotId = 1, SpotName = "test"*/ } }.AsQueryable());

            _spotRepository.Setup(x => x.Add(It.IsAny<Spot>())).Returns(new Spot());

            var handler = new CreateSpotCommandHandler(_spotRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Spot_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateSpotCommand();
            //command.SpotName = "test";

            _spotRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Spot, bool>>>()))
                        .ReturnsAsync(new Spot() { /*TODO:propertyler buraya yazılacak SpotId = 1, SpotName = "deneme"*/ });

            _spotRepository.Setup(x => x.Update(It.IsAny<Spot>())).Returns(new Spot());

            var handler = new UpdateSpotCommandHandler(_spotRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _spotRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Spot_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteSpotCommand();

            _spotRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Spot, bool>>>()))
                        .ReturnsAsync(new Spot() { /*TODO:propertyler buraya yazılacak SpotId = 1, SpotName = "deneme"*/});

            _spotRepository.Setup(x => x.Delete(It.IsAny<Spot>()));

            var handler = new DeleteSpotCommandHandler(_spotRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _spotRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

