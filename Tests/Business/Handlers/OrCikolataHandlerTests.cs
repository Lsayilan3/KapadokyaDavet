
using Business.Handlers.OrCikolatas.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.OrCikolatas.Queries.GetOrCikolataQuery;
using Entities.Concrete;
using static Business.Handlers.OrCikolatas.Queries.GetOrCikolatasQuery;
using static Business.Handlers.OrCikolatas.Commands.CreateOrCikolataCommand;
using Business.Handlers.OrCikolatas.Commands;
using Business.Constants;
using static Business.Handlers.OrCikolatas.Commands.UpdateOrCikolataCommand;
using static Business.Handlers.OrCikolatas.Commands.DeleteOrCikolataCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class OrCikolataHandlerTests
    {
        Mock<IOrCikolataRepository> _orCikolataRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _orCikolataRepository = new Mock<IOrCikolataRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task OrCikolata_GetQuery_Success()
        {
            //Arrange
            var query = new GetOrCikolataQuery();

            _orCikolataRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrCikolata, bool>>>())).ReturnsAsync(new OrCikolata()
//propertyler buraya yazılacak
//{																		
//OrCikolataId = 1,
//OrCikolataName = "Test"
//}
);

            var handler = new GetOrCikolataQueryHandler(_orCikolataRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.OrCikolataId.Should().Be(1);

        }

        [Test]
        public async Task OrCikolata_GetQueries_Success()
        {
            //Arrange
            var query = new GetOrCikolatasQuery();

            _orCikolataRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<OrCikolata, bool>>>()))
                        .ReturnsAsync(new List<OrCikolata> { new OrCikolata() { /*TODO:propertyler buraya yazılacak OrCikolataId = 1, OrCikolataName = "test"*/ } });

            var handler = new GetOrCikolatasQueryHandler(_orCikolataRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<OrCikolata>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task OrCikolata_CreateCommand_Success()
        {
            OrCikolata rt = null;
            //Arrange
            var command = new CreateOrCikolataCommand();
            //propertyler buraya yazılacak
            //command.OrCikolataName = "deneme";

            _orCikolataRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrCikolata, bool>>>()))
                        .ReturnsAsync(rt);

            _orCikolataRepository.Setup(x => x.Add(It.IsAny<OrCikolata>())).Returns(new OrCikolata());

            var handler = new CreateOrCikolataCommandHandler(_orCikolataRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orCikolataRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task OrCikolata_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateOrCikolataCommand();
            //propertyler buraya yazılacak 
            //command.OrCikolataName = "test";

            _orCikolataRepository.Setup(x => x.Query())
                                           .Returns(new List<OrCikolata> { new OrCikolata() { /*TODO:propertyler buraya yazılacak OrCikolataId = 1, OrCikolataName = "test"*/ } }.AsQueryable());

            _orCikolataRepository.Setup(x => x.Add(It.IsAny<OrCikolata>())).Returns(new OrCikolata());

            var handler = new CreateOrCikolataCommandHandler(_orCikolataRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task OrCikolata_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateOrCikolataCommand();
            //command.OrCikolataName = "test";

            _orCikolataRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrCikolata, bool>>>()))
                        .ReturnsAsync(new OrCikolata() { /*TODO:propertyler buraya yazılacak OrCikolataId = 1, OrCikolataName = "deneme"*/ });

            _orCikolataRepository.Setup(x => x.Update(It.IsAny<OrCikolata>())).Returns(new OrCikolata());

            var handler = new UpdateOrCikolataCommandHandler(_orCikolataRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orCikolataRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task OrCikolata_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteOrCikolataCommand();

            _orCikolataRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrCikolata, bool>>>()))
                        .ReturnsAsync(new OrCikolata() { /*TODO:propertyler buraya yazılacak OrCikolataId = 1, OrCikolataName = "deneme"*/});

            _orCikolataRepository.Setup(x => x.Delete(It.IsAny<OrCikolata>()));

            var handler = new DeleteOrCikolataCommandHandler(_orCikolataRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orCikolataRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

