
using Business.Handlers.OrSunnets.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.OrSunnets.Queries.GetOrSunnetQuery;
using Entities.Concrete;
using static Business.Handlers.OrSunnets.Queries.GetOrSunnetsQuery;
using static Business.Handlers.OrSunnets.Commands.CreateOrSunnetCommand;
using Business.Handlers.OrSunnets.Commands;
using Business.Constants;
using static Business.Handlers.OrSunnets.Commands.UpdateOrSunnetCommand;
using static Business.Handlers.OrSunnets.Commands.DeleteOrSunnetCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class OrSunnetHandlerTests
    {
        Mock<IOrSunnetRepository> _orSunnetRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _orSunnetRepository = new Mock<IOrSunnetRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task OrSunnet_GetQuery_Success()
        {
            //Arrange
            var query = new GetOrSunnetQuery();

            _orSunnetRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrSunnet, bool>>>())).ReturnsAsync(new OrSunnet()
//propertyler buraya yazılacak
//{																		
//OrSunnetId = 1,
//OrSunnetName = "Test"
//}
);

            var handler = new GetOrSunnetQueryHandler(_orSunnetRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.OrSunnetId.Should().Be(1);

        }

        [Test]
        public async Task OrSunnet_GetQueries_Success()
        {
            //Arrange
            var query = new GetOrSunnetsQuery();

            _orSunnetRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<OrSunnet, bool>>>()))
                        .ReturnsAsync(new List<OrSunnet> { new OrSunnet() { /*TODO:propertyler buraya yazılacak OrSunnetId = 1, OrSunnetName = "test"*/ } });

            var handler = new GetOrSunnetsQueryHandler(_orSunnetRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<OrSunnet>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task OrSunnet_CreateCommand_Success()
        {
            OrSunnet rt = null;
            //Arrange
            var command = new CreateOrSunnetCommand();
            //propertyler buraya yazılacak
            //command.OrSunnetName = "deneme";

            _orSunnetRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrSunnet, bool>>>()))
                        .ReturnsAsync(rt);

            _orSunnetRepository.Setup(x => x.Add(It.IsAny<OrSunnet>())).Returns(new OrSunnet());

            var handler = new CreateOrSunnetCommandHandler(_orSunnetRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orSunnetRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task OrSunnet_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateOrSunnetCommand();
            //propertyler buraya yazılacak 
            //command.OrSunnetName = "test";

            _orSunnetRepository.Setup(x => x.Query())
                                           .Returns(new List<OrSunnet> { new OrSunnet() { /*TODO:propertyler buraya yazılacak OrSunnetId = 1, OrSunnetName = "test"*/ } }.AsQueryable());

            _orSunnetRepository.Setup(x => x.Add(It.IsAny<OrSunnet>())).Returns(new OrSunnet());

            var handler = new CreateOrSunnetCommandHandler(_orSunnetRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task OrSunnet_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateOrSunnetCommand();
            //command.OrSunnetName = "test";

            _orSunnetRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrSunnet, bool>>>()))
                        .ReturnsAsync(new OrSunnet() { /*TODO:propertyler buraya yazılacak OrSunnetId = 1, OrSunnetName = "deneme"*/ });

            _orSunnetRepository.Setup(x => x.Update(It.IsAny<OrSunnet>())).Returns(new OrSunnet());

            var handler = new UpdateOrSunnetCommandHandler(_orSunnetRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orSunnetRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task OrSunnet_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteOrSunnetCommand();

            _orSunnetRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrSunnet, bool>>>()))
                        .ReturnsAsync(new OrSunnet() { /*TODO:propertyler buraya yazılacak OrSunnetId = 1, OrSunnetName = "deneme"*/});

            _orSunnetRepository.Setup(x => x.Delete(It.IsAny<OrSunnet>()));

            var handler = new DeleteOrSunnetCommandHandler(_orSunnetRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orSunnetRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

