
using Business.Handlers.Cikolatas.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Cikolatas.Queries.GetCikolataQuery;
using Entities.Concrete;
using static Business.Handlers.Cikolatas.Queries.GetCikolatasQuery;
using static Business.Handlers.Cikolatas.Commands.CreateCikolataCommand;
using Business.Handlers.Cikolatas.Commands;
using Business.Constants;
using static Business.Handlers.Cikolatas.Commands.UpdateCikolataCommand;
using static Business.Handlers.Cikolatas.Commands.DeleteCikolataCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class CikolataHandlerTests
    {
        Mock<ICikolataRepository> _cikolataRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _cikolataRepository = new Mock<ICikolataRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Cikolata_GetQuery_Success()
        {
            //Arrange
            var query = new GetCikolataQuery();

            _cikolataRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Cikolata, bool>>>())).ReturnsAsync(new Cikolata()
//propertyler buraya yazılacak
//{																		
//CikolataId = 1,
//CikolataName = "Test"
//}
);

            var handler = new GetCikolataQueryHandler(_cikolataRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.CikolataId.Should().Be(1);

        }

        [Test]
        public async Task Cikolata_GetQueries_Success()
        {
            //Arrange
            var query = new GetCikolatasQuery();

            _cikolataRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Cikolata, bool>>>()))
                        .ReturnsAsync(new List<Cikolata> { new Cikolata() { /*TODO:propertyler buraya yazılacak CikolataId = 1, CikolataName = "test"*/ } });

            var handler = new GetCikolatasQueryHandler(_cikolataRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Cikolata>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Cikolata_CreateCommand_Success()
        {
            Cikolata rt = null;
            //Arrange
            var command = new CreateCikolataCommand();
            //propertyler buraya yazılacak
            //command.CikolataName = "deneme";

            _cikolataRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Cikolata, bool>>>()))
                        .ReturnsAsync(rt);

            _cikolataRepository.Setup(x => x.Add(It.IsAny<Cikolata>())).Returns(new Cikolata());

            var handler = new CreateCikolataCommandHandler(_cikolataRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _cikolataRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Cikolata_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateCikolataCommand();
            //propertyler buraya yazılacak 
            //command.CikolataName = "test";

            _cikolataRepository.Setup(x => x.Query())
                                           .Returns(new List<Cikolata> { new Cikolata() { /*TODO:propertyler buraya yazılacak CikolataId = 1, CikolataName = "test"*/ } }.AsQueryable());

            _cikolataRepository.Setup(x => x.Add(It.IsAny<Cikolata>())).Returns(new Cikolata());

            var handler = new CreateCikolataCommandHandler(_cikolataRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Cikolata_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateCikolataCommand();
            //command.CikolataName = "test";

            _cikolataRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Cikolata, bool>>>()))
                        .ReturnsAsync(new Cikolata() { /*TODO:propertyler buraya yazılacak CikolataId = 1, CikolataName = "deneme"*/ });

            _cikolataRepository.Setup(x => x.Update(It.IsAny<Cikolata>())).Returns(new Cikolata());

            var handler = new UpdateCikolataCommandHandler(_cikolataRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _cikolataRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Cikolata_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteCikolataCommand();

            _cikolataRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Cikolata, bool>>>()))
                        .ReturnsAsync(new Cikolata() { /*TODO:propertyler buraya yazılacak CikolataId = 1, CikolataName = "deneme"*/});

            _cikolataRepository.Setup(x => x.Delete(It.IsAny<Cikolata>()));

            var handler = new DeleteCikolataCommandHandler(_cikolataRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _cikolataRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

