
using Business.Handlers.OrCofves.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.OrCofves.Queries.GetOrCoffeQuery;
using Entities.Concrete;
using static Business.Handlers.OrCofves.Queries.GetOrCofvesQuery;
using static Business.Handlers.OrCofves.Commands.CreateOrCoffeCommand;
using Business.Handlers.OrCofves.Commands;
using Business.Constants;
using static Business.Handlers.OrCofves.Commands.UpdateOrCoffeCommand;
using static Business.Handlers.OrCofves.Commands.DeleteOrCoffeCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class OrCoffeHandlerTests
    {
        Mock<IOrCoffeRepository> _orCoffeRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _orCoffeRepository = new Mock<IOrCoffeRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task OrCoffe_GetQuery_Success()
        {
            //Arrange
            var query = new GetOrCoffeQuery();

            _orCoffeRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrCoffe, bool>>>())).ReturnsAsync(new OrCoffe()
//propertyler buraya yazılacak
//{																		
//OrCoffeId = 1,
//OrCoffeName = "Test"
//}
);

            var handler = new GetOrCoffeQueryHandler(_orCoffeRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.OrCoffeId.Should().Be(1);

        }

        [Test]
        public async Task OrCoffe_GetQueries_Success()
        {
            //Arrange
            var query = new GetOrCofvesQuery();

            _orCoffeRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<OrCoffe, bool>>>()))
                        .ReturnsAsync(new List<OrCoffe> { new OrCoffe() { /*TODO:propertyler buraya yazılacak OrCoffeId = 1, OrCoffeName = "test"*/ } });

            var handler = new GetOrCofvesQueryHandler(_orCoffeRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<OrCoffe>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task OrCoffe_CreateCommand_Success()
        {
            OrCoffe rt = null;
            //Arrange
            var command = new CreateOrCoffeCommand();
            //propertyler buraya yazılacak
            //command.OrCoffeName = "deneme";

            _orCoffeRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrCoffe, bool>>>()))
                        .ReturnsAsync(rt);

            _orCoffeRepository.Setup(x => x.Add(It.IsAny<OrCoffe>())).Returns(new OrCoffe());

            var handler = new CreateOrCoffeCommandHandler(_orCoffeRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orCoffeRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task OrCoffe_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateOrCoffeCommand();
            //propertyler buraya yazılacak 
            //command.OrCoffeName = "test";

            _orCoffeRepository.Setup(x => x.Query())
                                           .Returns(new List<OrCoffe> { new OrCoffe() { /*TODO:propertyler buraya yazılacak OrCoffeId = 1, OrCoffeName = "test"*/ } }.AsQueryable());

            _orCoffeRepository.Setup(x => x.Add(It.IsAny<OrCoffe>())).Returns(new OrCoffe());

            var handler = new CreateOrCoffeCommandHandler(_orCoffeRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task OrCoffe_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateOrCoffeCommand();
            //command.OrCoffeName = "test";

            _orCoffeRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrCoffe, bool>>>()))
                        .ReturnsAsync(new OrCoffe() { /*TODO:propertyler buraya yazılacak OrCoffeId = 1, OrCoffeName = "deneme"*/ });

            _orCoffeRepository.Setup(x => x.Update(It.IsAny<OrCoffe>())).Returns(new OrCoffe());

            var handler = new UpdateOrCoffeCommandHandler(_orCoffeRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orCoffeRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task OrCoffe_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteOrCoffeCommand();

            _orCoffeRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrCoffe, bool>>>()))
                        .ReturnsAsync(new OrCoffe() { /*TODO:propertyler buraya yazılacak OrCoffeId = 1, OrCoffeName = "deneme"*/});

            _orCoffeRepository.Setup(x => x.Delete(It.IsAny<OrCoffe>()));

            var handler = new DeleteOrCoffeCommandHandler(_orCoffeRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orCoffeRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

