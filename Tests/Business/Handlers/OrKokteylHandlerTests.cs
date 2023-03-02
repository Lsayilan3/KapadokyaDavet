
using Business.Handlers.OrKokteyls.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.OrKokteyls.Queries.GetOrKokteylQuery;
using Entities.Concrete;
using static Business.Handlers.OrKokteyls.Queries.GetOrKokteylsQuery;
using static Business.Handlers.OrKokteyls.Commands.CreateOrKokteylCommand;
using Business.Handlers.OrKokteyls.Commands;
using Business.Constants;
using static Business.Handlers.OrKokteyls.Commands.UpdateOrKokteylCommand;
using static Business.Handlers.OrKokteyls.Commands.DeleteOrKokteylCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class OrKokteylHandlerTests
    {
        Mock<IOrKokteylRepository> _orKokteylRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _orKokteylRepository = new Mock<IOrKokteylRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task OrKokteyl_GetQuery_Success()
        {
            //Arrange
            var query = new GetOrKokteylQuery();

            _orKokteylRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrKokteyl, bool>>>())).ReturnsAsync(new OrKokteyl()
//propertyler buraya yazılacak
//{																		
//OrKokteylId = 1,
//OrKokteylName = "Test"
//}
);

            var handler = new GetOrKokteylQueryHandler(_orKokteylRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.OrKokteylId.Should().Be(1);

        }

        [Test]
        public async Task OrKokteyl_GetQueries_Success()
        {
            //Arrange
            var query = new GetOrKokteylsQuery();

            _orKokteylRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<OrKokteyl, bool>>>()))
                        .ReturnsAsync(new List<OrKokteyl> { new OrKokteyl() { /*TODO:propertyler buraya yazılacak OrKokteylId = 1, OrKokteylName = "test"*/ } });

            var handler = new GetOrKokteylsQueryHandler(_orKokteylRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<OrKokteyl>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task OrKokteyl_CreateCommand_Success()
        {
            OrKokteyl rt = null;
            //Arrange
            var command = new CreateOrKokteylCommand();
            //propertyler buraya yazılacak
            //command.OrKokteylName = "deneme";

            _orKokteylRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrKokteyl, bool>>>()))
                        .ReturnsAsync(rt);

            _orKokteylRepository.Setup(x => x.Add(It.IsAny<OrKokteyl>())).Returns(new OrKokteyl());

            var handler = new CreateOrKokteylCommandHandler(_orKokteylRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orKokteylRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task OrKokteyl_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateOrKokteylCommand();
            //propertyler buraya yazılacak 
            //command.OrKokteylName = "test";

            _orKokteylRepository.Setup(x => x.Query())
                                           .Returns(new List<OrKokteyl> { new OrKokteyl() { /*TODO:propertyler buraya yazılacak OrKokteylId = 1, OrKokteylName = "test"*/ } }.AsQueryable());

            _orKokteylRepository.Setup(x => x.Add(It.IsAny<OrKokteyl>())).Returns(new OrKokteyl());

            var handler = new CreateOrKokteylCommandHandler(_orKokteylRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task OrKokteyl_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateOrKokteylCommand();
            //command.OrKokteylName = "test";

            _orKokteylRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrKokteyl, bool>>>()))
                        .ReturnsAsync(new OrKokteyl() { /*TODO:propertyler buraya yazılacak OrKokteylId = 1, OrKokteylName = "deneme"*/ });

            _orKokteylRepository.Setup(x => x.Update(It.IsAny<OrKokteyl>())).Returns(new OrKokteyl());

            var handler = new UpdateOrKokteylCommandHandler(_orKokteylRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orKokteylRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task OrKokteyl_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteOrKokteylCommand();

            _orKokteylRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrKokteyl, bool>>>()))
                        .ReturnsAsync(new OrKokteyl() { /*TODO:propertyler buraya yazılacak OrKokteylId = 1, OrKokteylName = "deneme"*/});

            _orKokteylRepository.Setup(x => x.Delete(It.IsAny<OrKokteyl>()));

            var handler = new DeleteOrKokteylCommandHandler(_orKokteylRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orKokteylRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

