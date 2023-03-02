
using Business.Handlers.OrSokakLezzetis.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.OrSokakLezzetis.Queries.GetOrSokakLezzetiQuery;
using Entities.Concrete;
using static Business.Handlers.OrSokakLezzetis.Queries.GetOrSokakLezzetisQuery;
using static Business.Handlers.OrSokakLezzetis.Commands.CreateOrSokakLezzetiCommand;
using Business.Handlers.OrSokakLezzetis.Commands;
using Business.Constants;
using static Business.Handlers.OrSokakLezzetis.Commands.UpdateOrSokakLezzetiCommand;
using static Business.Handlers.OrSokakLezzetis.Commands.DeleteOrSokakLezzetiCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class OrSokakLezzetiHandlerTests
    {
        Mock<IOrSokakLezzetiRepository> _orSokakLezzetiRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _orSokakLezzetiRepository = new Mock<IOrSokakLezzetiRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task OrSokakLezzeti_GetQuery_Success()
        {
            //Arrange
            var query = new GetOrSokakLezzetiQuery();

            _orSokakLezzetiRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrSokakLezzeti, bool>>>())).ReturnsAsync(new OrSokakLezzeti()
//propertyler buraya yazılacak
//{																		
//OrSokakLezzetiId = 1,
//OrSokakLezzetiName = "Test"
//}
);

            var handler = new GetOrSokakLezzetiQueryHandler(_orSokakLezzetiRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.OrSokakLezzetiId.Should().Be(1);

        }

        [Test]
        public async Task OrSokakLezzeti_GetQueries_Success()
        {
            //Arrange
            var query = new GetOrSokakLezzetisQuery();

            _orSokakLezzetiRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<OrSokakLezzeti, bool>>>()))
                        .ReturnsAsync(new List<OrSokakLezzeti> { new OrSokakLezzeti() { /*TODO:propertyler buraya yazılacak OrSokakLezzetiId = 1, OrSokakLezzetiName = "test"*/ } });

            var handler = new GetOrSokakLezzetisQueryHandler(_orSokakLezzetiRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<OrSokakLezzeti>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task OrSokakLezzeti_CreateCommand_Success()
        {
            OrSokakLezzeti rt = null;
            //Arrange
            var command = new CreateOrSokakLezzetiCommand();
            //propertyler buraya yazılacak
            //command.OrSokakLezzetiName = "deneme";

            _orSokakLezzetiRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrSokakLezzeti, bool>>>()))
                        .ReturnsAsync(rt);

            _orSokakLezzetiRepository.Setup(x => x.Add(It.IsAny<OrSokakLezzeti>())).Returns(new OrSokakLezzeti());

            var handler = new CreateOrSokakLezzetiCommandHandler(_orSokakLezzetiRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orSokakLezzetiRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task OrSokakLezzeti_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateOrSokakLezzetiCommand();
            //propertyler buraya yazılacak 
            //command.OrSokakLezzetiName = "test";

            _orSokakLezzetiRepository.Setup(x => x.Query())
                                           .Returns(new List<OrSokakLezzeti> { new OrSokakLezzeti() { /*TODO:propertyler buraya yazılacak OrSokakLezzetiId = 1, OrSokakLezzetiName = "test"*/ } }.AsQueryable());

            _orSokakLezzetiRepository.Setup(x => x.Add(It.IsAny<OrSokakLezzeti>())).Returns(new OrSokakLezzeti());

            var handler = new CreateOrSokakLezzetiCommandHandler(_orSokakLezzetiRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task OrSokakLezzeti_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateOrSokakLezzetiCommand();
            //command.OrSokakLezzetiName = "test";

            _orSokakLezzetiRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrSokakLezzeti, bool>>>()))
                        .ReturnsAsync(new OrSokakLezzeti() { /*TODO:propertyler buraya yazılacak OrSokakLezzetiId = 1, OrSokakLezzetiName = "deneme"*/ });

            _orSokakLezzetiRepository.Setup(x => x.Update(It.IsAny<OrSokakLezzeti>())).Returns(new OrSokakLezzeti());

            var handler = new UpdateOrSokakLezzetiCommandHandler(_orSokakLezzetiRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orSokakLezzetiRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task OrSokakLezzeti_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteOrSokakLezzetiCommand();

            _orSokakLezzetiRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrSokakLezzeti, bool>>>()))
                        .ReturnsAsync(new OrSokakLezzeti() { /*TODO:propertyler buraya yazılacak OrSokakLezzetiId = 1, OrSokakLezzetiName = "deneme"*/});

            _orSokakLezzetiRepository.Setup(x => x.Delete(It.IsAny<OrSokakLezzeti>()));

            var handler = new DeleteOrSokakLezzetiCommandHandler(_orSokakLezzetiRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orSokakLezzetiRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

