
using Business.Handlers.Partis.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Partis.Queries.GetPartiQuery;
using Entities.Concrete;
using static Business.Handlers.Partis.Queries.GetPartisQuery;
using static Business.Handlers.Partis.Commands.CreatePartiCommand;
using Business.Handlers.Partis.Commands;
using Business.Constants;
using static Business.Handlers.Partis.Commands.UpdatePartiCommand;
using static Business.Handlers.Partis.Commands.DeletePartiCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class PartiHandlerTests
    {
        Mock<IPartiRepository> _partiRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _partiRepository = new Mock<IPartiRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Parti_GetQuery_Success()
        {
            //Arrange
            var query = new GetPartiQuery();

            _partiRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Parti, bool>>>())).ReturnsAsync(new Parti()
//propertyler buraya yazılacak
//{																		
//PartiId = 1,
//PartiName = "Test"
//}
);

            var handler = new GetPartiQueryHandler(_partiRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.PartiId.Should().Be(1);

        }

        [Test]
        public async Task Parti_GetQueries_Success()
        {
            //Arrange
            var query = new GetPartisQuery();

            _partiRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Parti, bool>>>()))
                        .ReturnsAsync(new List<Parti> { new Parti() { /*TODO:propertyler buraya yazılacak PartiId = 1, PartiName = "test"*/ } });

            var handler = new GetPartisQueryHandler(_partiRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Parti>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Parti_CreateCommand_Success()
        {
            Parti rt = null;
            //Arrange
            var command = new CreatePartiCommand();
            //propertyler buraya yazılacak
            //command.PartiName = "deneme";

            _partiRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Parti, bool>>>()))
                        .ReturnsAsync(rt);

            _partiRepository.Setup(x => x.Add(It.IsAny<Parti>())).Returns(new Parti());

            var handler = new CreatePartiCommandHandler(_partiRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _partiRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Parti_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreatePartiCommand();
            //propertyler buraya yazılacak 
            //command.PartiName = "test";

            _partiRepository.Setup(x => x.Query())
                                           .Returns(new List<Parti> { new Parti() { /*TODO:propertyler buraya yazılacak PartiId = 1, PartiName = "test"*/ } }.AsQueryable());

            _partiRepository.Setup(x => x.Add(It.IsAny<Parti>())).Returns(new Parti());

            var handler = new CreatePartiCommandHandler(_partiRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Parti_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdatePartiCommand();
            //command.PartiName = "test";

            _partiRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Parti, bool>>>()))
                        .ReturnsAsync(new Parti() { /*TODO:propertyler buraya yazılacak PartiId = 1, PartiName = "deneme"*/ });

            _partiRepository.Setup(x => x.Update(It.IsAny<Parti>())).Returns(new Parti());

            var handler = new UpdatePartiCommandHandler(_partiRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _partiRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Parti_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeletePartiCommand();

            _partiRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Parti, bool>>>()))
                        .ReturnsAsync(new Parti() { /*TODO:propertyler buraya yazılacak PartiId = 1, PartiName = "deneme"*/});

            _partiRepository.Setup(x => x.Delete(It.IsAny<Parti>()));

            var handler = new DeletePartiCommandHandler(_partiRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _partiRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

