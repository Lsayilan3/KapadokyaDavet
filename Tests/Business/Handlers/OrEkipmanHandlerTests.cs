
using Business.Handlers.OrEkipmans.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.OrEkipmans.Queries.GetOrEkipmanQuery;
using Entities.Concrete;
using static Business.Handlers.OrEkipmans.Queries.GetOrEkipmansQuery;
using static Business.Handlers.OrEkipmans.Commands.CreateOrEkipmanCommand;
using Business.Handlers.OrEkipmans.Commands;
using Business.Constants;
using static Business.Handlers.OrEkipmans.Commands.UpdateOrEkipmanCommand;
using static Business.Handlers.OrEkipmans.Commands.DeleteOrEkipmanCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class OrEkipmanHandlerTests
    {
        Mock<IOrEkipmanRepository> _orEkipmanRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _orEkipmanRepository = new Mock<IOrEkipmanRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task OrEkipman_GetQuery_Success()
        {
            //Arrange
            var query = new GetOrEkipmanQuery();

            _orEkipmanRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrEkipman, bool>>>())).ReturnsAsync(new OrEkipman()
//propertyler buraya yazılacak
//{																		
//OrEkipmanId = 1,
//OrEkipmanName = "Test"
//}
);

            var handler = new GetOrEkipmanQueryHandler(_orEkipmanRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.OrEkipmanId.Should().Be(1);

        }

        [Test]
        public async Task OrEkipman_GetQueries_Success()
        {
            //Arrange
            var query = new GetOrEkipmansQuery();

            _orEkipmanRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<OrEkipman, bool>>>()))
                        .ReturnsAsync(new List<OrEkipman> { new OrEkipman() { /*TODO:propertyler buraya yazılacak OrEkipmanId = 1, OrEkipmanName = "test"*/ } });

            var handler = new GetOrEkipmansQueryHandler(_orEkipmanRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<OrEkipman>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task OrEkipman_CreateCommand_Success()
        {
            OrEkipman rt = null;
            //Arrange
            var command = new CreateOrEkipmanCommand();
            //propertyler buraya yazılacak
            //command.OrEkipmanName = "deneme";

            _orEkipmanRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrEkipman, bool>>>()))
                        .ReturnsAsync(rt);

            _orEkipmanRepository.Setup(x => x.Add(It.IsAny<OrEkipman>())).Returns(new OrEkipman());

            var handler = new CreateOrEkipmanCommandHandler(_orEkipmanRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orEkipmanRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task OrEkipman_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateOrEkipmanCommand();
            //propertyler buraya yazılacak 
            //command.OrEkipmanName = "test";

            _orEkipmanRepository.Setup(x => x.Query())
                                           .Returns(new List<OrEkipman> { new OrEkipman() { /*TODO:propertyler buraya yazılacak OrEkipmanId = 1, OrEkipmanName = "test"*/ } }.AsQueryable());

            _orEkipmanRepository.Setup(x => x.Add(It.IsAny<OrEkipman>())).Returns(new OrEkipman());

            var handler = new CreateOrEkipmanCommandHandler(_orEkipmanRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task OrEkipman_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateOrEkipmanCommand();
            //command.OrEkipmanName = "test";

            _orEkipmanRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrEkipman, bool>>>()))
                        .ReturnsAsync(new OrEkipman() { /*TODO:propertyler buraya yazılacak OrEkipmanId = 1, OrEkipmanName = "deneme"*/ });

            _orEkipmanRepository.Setup(x => x.Update(It.IsAny<OrEkipman>())).Returns(new OrEkipman());

            var handler = new UpdateOrEkipmanCommandHandler(_orEkipmanRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orEkipmanRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task OrEkipman_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteOrEkipmanCommand();

            _orEkipmanRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrEkipman, bool>>>()))
                        .ReturnsAsync(new OrEkipman() { /*TODO:propertyler buraya yazılacak OrEkipmanId = 1, OrEkipmanName = "deneme"*/});

            _orEkipmanRepository.Setup(x => x.Delete(It.IsAny<OrEkipman>()));

            var handler = new DeleteOrEkipmanCommandHandler(_orEkipmanRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orEkipmanRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

