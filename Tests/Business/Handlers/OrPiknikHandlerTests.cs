
using Business.Handlers.OrPikniks.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.OrPikniks.Queries.GetOrPiknikQuery;
using Entities.Concrete;
using static Business.Handlers.OrPikniks.Queries.GetOrPikniksQuery;
using static Business.Handlers.OrPikniks.Commands.CreateOrPiknikCommand;
using Business.Handlers.OrPikniks.Commands;
using Business.Constants;
using static Business.Handlers.OrPikniks.Commands.UpdateOrPiknikCommand;
using static Business.Handlers.OrPikniks.Commands.DeleteOrPiknikCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class OrPiknikHandlerTests
    {
        Mock<IOrPiknikRepository> _orPiknikRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _orPiknikRepository = new Mock<IOrPiknikRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task OrPiknik_GetQuery_Success()
        {
            //Arrange
            var query = new GetOrPiknikQuery();

            _orPiknikRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrPiknik, bool>>>())).ReturnsAsync(new OrPiknik()
//propertyler buraya yazılacak
//{																		
//OrPiknikId = 1,
//OrPiknikName = "Test"
//}
);

            var handler = new GetOrPiknikQueryHandler(_orPiknikRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.OrPiknikId.Should().Be(1);

        }

        [Test]
        public async Task OrPiknik_GetQueries_Success()
        {
            //Arrange
            var query = new GetOrPikniksQuery();

            _orPiknikRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<OrPiknik, bool>>>()))
                        .ReturnsAsync(new List<OrPiknik> { new OrPiknik() { /*TODO:propertyler buraya yazılacak OrPiknikId = 1, OrPiknikName = "test"*/ } });

            var handler = new GetOrPikniksQueryHandler(_orPiknikRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<OrPiknik>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task OrPiknik_CreateCommand_Success()
        {
            OrPiknik rt = null;
            //Arrange
            var command = new CreateOrPiknikCommand();
            //propertyler buraya yazılacak
            //command.OrPiknikName = "deneme";

            _orPiknikRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrPiknik, bool>>>()))
                        .ReturnsAsync(rt);

            _orPiknikRepository.Setup(x => x.Add(It.IsAny<OrPiknik>())).Returns(new OrPiknik());

            var handler = new CreateOrPiknikCommandHandler(_orPiknikRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orPiknikRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task OrPiknik_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateOrPiknikCommand();
            //propertyler buraya yazılacak 
            //command.OrPiknikName = "test";

            _orPiknikRepository.Setup(x => x.Query())
                                           .Returns(new List<OrPiknik> { new OrPiknik() { /*TODO:propertyler buraya yazılacak OrPiknikId = 1, OrPiknikName = "test"*/ } }.AsQueryable());

            _orPiknikRepository.Setup(x => x.Add(It.IsAny<OrPiknik>())).Returns(new OrPiknik());

            var handler = new CreateOrPiknikCommandHandler(_orPiknikRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task OrPiknik_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateOrPiknikCommand();
            //command.OrPiknikName = "test";

            _orPiknikRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrPiknik, bool>>>()))
                        .ReturnsAsync(new OrPiknik() { /*TODO:propertyler buraya yazılacak OrPiknikId = 1, OrPiknikName = "deneme"*/ });

            _orPiknikRepository.Setup(x => x.Update(It.IsAny<OrPiknik>())).Returns(new OrPiknik());

            var handler = new UpdateOrPiknikCommandHandler(_orPiknikRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orPiknikRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task OrPiknik_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteOrPiknikCommand();

            _orPiknikRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrPiknik, bool>>>()))
                        .ReturnsAsync(new OrPiknik() { /*TODO:propertyler buraya yazılacak OrPiknikId = 1, OrPiknikName = "deneme"*/});

            _orPiknikRepository.Setup(x => x.Delete(It.IsAny<OrPiknik>()));

            var handler = new DeleteOrPiknikCommandHandler(_orPiknikRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orPiknikRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

