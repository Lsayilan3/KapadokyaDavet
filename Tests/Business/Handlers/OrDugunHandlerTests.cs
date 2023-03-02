
using Business.Handlers.OrDuguns.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.OrDuguns.Queries.GetOrDugunQuery;
using Entities.Concrete;
using static Business.Handlers.OrDuguns.Queries.GetOrDugunsQuery;
using static Business.Handlers.OrDuguns.Commands.CreateOrDugunCommand;
using Business.Handlers.OrDuguns.Commands;
using Business.Constants;
using static Business.Handlers.OrDuguns.Commands.UpdateOrDugunCommand;
using static Business.Handlers.OrDuguns.Commands.DeleteOrDugunCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class OrDugunHandlerTests
    {
        Mock<IOrDugunRepository> _orDugunRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _orDugunRepository = new Mock<IOrDugunRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task OrDugun_GetQuery_Success()
        {
            //Arrange
            var query = new GetOrDugunQuery();

            _orDugunRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrDugun, bool>>>())).ReturnsAsync(new OrDugun()
//propertyler buraya yazılacak
//{																		
//OrDugunId = 1,
//OrDugunName = "Test"
//}
);

            var handler = new GetOrDugunQueryHandler(_orDugunRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.OrDugunId.Should().Be(1);

        }

        [Test]
        public async Task OrDugun_GetQueries_Success()
        {
            //Arrange
            var query = new GetOrDugunsQuery();

            _orDugunRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<OrDugun, bool>>>()))
                        .ReturnsAsync(new List<OrDugun> { new OrDugun() { /*TODO:propertyler buraya yazılacak OrDugunId = 1, OrDugunName = "test"*/ } });

            var handler = new GetOrDugunsQueryHandler(_orDugunRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<OrDugun>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task OrDugun_CreateCommand_Success()
        {
            OrDugun rt = null;
            //Arrange
            var command = new CreateOrDugunCommand();
            //propertyler buraya yazılacak
            //command.OrDugunName = "deneme";

            _orDugunRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrDugun, bool>>>()))
                        .ReturnsAsync(rt);

            _orDugunRepository.Setup(x => x.Add(It.IsAny<OrDugun>())).Returns(new OrDugun());

            var handler = new CreateOrDugunCommandHandler(_orDugunRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orDugunRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task OrDugun_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateOrDugunCommand();
            //propertyler buraya yazılacak 
            //command.OrDugunName = "test";

            _orDugunRepository.Setup(x => x.Query())
                                           .Returns(new List<OrDugun> { new OrDugun() { /*TODO:propertyler buraya yazılacak OrDugunId = 1, OrDugunName = "test"*/ } }.AsQueryable());

            _orDugunRepository.Setup(x => x.Add(It.IsAny<OrDugun>())).Returns(new OrDugun());

            var handler = new CreateOrDugunCommandHandler(_orDugunRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task OrDugun_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateOrDugunCommand();
            //command.OrDugunName = "test";

            _orDugunRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrDugun, bool>>>()))
                        .ReturnsAsync(new OrDugun() { /*TODO:propertyler buraya yazılacak OrDugunId = 1, OrDugunName = "deneme"*/ });

            _orDugunRepository.Setup(x => x.Update(It.IsAny<OrDugun>())).Returns(new OrDugun());

            var handler = new UpdateOrDugunCommandHandler(_orDugunRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orDugunRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task OrDugun_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteOrDugunCommand();

            _orDugunRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrDugun, bool>>>()))
                        .ReturnsAsync(new OrDugun() { /*TODO:propertyler buraya yazılacak OrDugunId = 1, OrDugunName = "deneme"*/});

            _orDugunRepository.Setup(x => x.Delete(It.IsAny<OrDugun>()));

            var handler = new DeleteOrDugunCommandHandler(_orDugunRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orDugunRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

