
using Business.Handlers.OrKinaas.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.OrKinaas.Queries.GetOrKinaaQuery;
using Entities.Concrete;
using static Business.Handlers.OrKinaas.Queries.GetOrKinaasQuery;
using static Business.Handlers.OrKinaas.Commands.CreateOrKinaaCommand;
using Business.Handlers.OrKinaas.Commands;
using Business.Constants;
using static Business.Handlers.OrKinaas.Commands.UpdateOrKinaaCommand;
using static Business.Handlers.OrKinaas.Commands.DeleteOrKinaaCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class OrKinaaHandlerTests
    {
        Mock<IOrKinaaRepository> _orKinaaRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _orKinaaRepository = new Mock<IOrKinaaRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task OrKinaa_GetQuery_Success()
        {
            //Arrange
            var query = new GetOrKinaaQuery();

            _orKinaaRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrKinaa, bool>>>())).ReturnsAsync(new OrKinaa()
//propertyler buraya yazılacak
//{																		
//OrKinaaId = 1,
//OrKinaaName = "Test"
//}
);

            var handler = new GetOrKinaaQueryHandler(_orKinaaRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.OrKinaaId.Should().Be(1);

        }

        [Test]
        public async Task OrKinaa_GetQueries_Success()
        {
            //Arrange
            var query = new GetOrKinaasQuery();

            _orKinaaRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<OrKinaa, bool>>>()))
                        .ReturnsAsync(new List<OrKinaa> { new OrKinaa() { /*TODO:propertyler buraya yazılacak OrKinaaId = 1, OrKinaaName = "test"*/ } });

            var handler = new GetOrKinaasQueryHandler(_orKinaaRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<OrKinaa>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task OrKinaa_CreateCommand_Success()
        {
            OrKinaa rt = null;
            //Arrange
            var command = new CreateOrKinaaCommand();
            //propertyler buraya yazılacak
            //command.OrKinaaName = "deneme";

            _orKinaaRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrKinaa, bool>>>()))
                        .ReturnsAsync(rt);

            _orKinaaRepository.Setup(x => x.Add(It.IsAny<OrKinaa>())).Returns(new OrKinaa());

            var handler = new CreateOrKinaaCommandHandler(_orKinaaRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orKinaaRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task OrKinaa_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateOrKinaaCommand();
            //propertyler buraya yazılacak 
            //command.OrKinaaName = "test";

            _orKinaaRepository.Setup(x => x.Query())
                                           .Returns(new List<OrKinaa> { new OrKinaa() { /*TODO:propertyler buraya yazılacak OrKinaaId = 1, OrKinaaName = "test"*/ } }.AsQueryable());

            _orKinaaRepository.Setup(x => x.Add(It.IsAny<OrKinaa>())).Returns(new OrKinaa());

            var handler = new CreateOrKinaaCommandHandler(_orKinaaRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task OrKinaa_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateOrKinaaCommand();
            //command.OrKinaaName = "test";

            _orKinaaRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrKinaa, bool>>>()))
                        .ReturnsAsync(new OrKinaa() { /*TODO:propertyler buraya yazılacak OrKinaaId = 1, OrKinaaName = "deneme"*/ });

            _orKinaaRepository.Setup(x => x.Update(It.IsAny<OrKinaa>())).Returns(new OrKinaa());

            var handler = new UpdateOrKinaaCommandHandler(_orKinaaRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orKinaaRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task OrKinaa_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteOrKinaaCommand();

            _orKinaaRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrKinaa, bool>>>()))
                        .ReturnsAsync(new OrKinaa() { /*TODO:propertyler buraya yazılacak OrKinaaId = 1, OrKinaaName = "deneme"*/});

            _orKinaaRepository.Setup(x => x.Delete(It.IsAny<OrKinaa>()));

            var handler = new DeleteOrKinaaCommandHandler(_orKinaaRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orKinaaRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

