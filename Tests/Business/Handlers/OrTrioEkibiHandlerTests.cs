
using Business.Handlers.OrTrioEkibis.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.OrTrioEkibis.Queries.GetOrTrioEkibiQuery;
using Entities.Concrete;
using static Business.Handlers.OrTrioEkibis.Queries.GetOrTrioEkibisQuery;
using static Business.Handlers.OrTrioEkibis.Commands.CreateOrTrioEkibiCommand;
using Business.Handlers.OrTrioEkibis.Commands;
using Business.Constants;
using static Business.Handlers.OrTrioEkibis.Commands.UpdateOrTrioEkibiCommand;
using static Business.Handlers.OrTrioEkibis.Commands.DeleteOrTrioEkibiCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class OrTrioEkibiHandlerTests
    {
        Mock<IOrTrioEkibiRepository> _orTrioEkibiRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _orTrioEkibiRepository = new Mock<IOrTrioEkibiRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task OrTrioEkibi_GetQuery_Success()
        {
            //Arrange
            var query = new GetOrTrioEkibiQuery();

            _orTrioEkibiRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrTrioEkibi, bool>>>())).ReturnsAsync(new OrTrioEkibi()
//propertyler buraya yazılacak
//{																		
//OrTrioEkibiId = 1,
//OrTrioEkibiName = "Test"
//}
);

            var handler = new GetOrTrioEkibiQueryHandler(_orTrioEkibiRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.OrTrioEkibiId.Should().Be(1);

        }

        [Test]
        public async Task OrTrioEkibi_GetQueries_Success()
        {
            //Arrange
            var query = new GetOrTrioEkibisQuery();

            _orTrioEkibiRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<OrTrioEkibi, bool>>>()))
                        .ReturnsAsync(new List<OrTrioEkibi> { new OrTrioEkibi() { /*TODO:propertyler buraya yazılacak OrTrioEkibiId = 1, OrTrioEkibiName = "test"*/ } });

            var handler = new GetOrTrioEkibisQueryHandler(_orTrioEkibiRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<OrTrioEkibi>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task OrTrioEkibi_CreateCommand_Success()
        {
            OrTrioEkibi rt = null;
            //Arrange
            var command = new CreateOrTrioEkibiCommand();
            //propertyler buraya yazılacak
            //command.OrTrioEkibiName = "deneme";

            _orTrioEkibiRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrTrioEkibi, bool>>>()))
                        .ReturnsAsync(rt);

            _orTrioEkibiRepository.Setup(x => x.Add(It.IsAny<OrTrioEkibi>())).Returns(new OrTrioEkibi());

            var handler = new CreateOrTrioEkibiCommandHandler(_orTrioEkibiRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orTrioEkibiRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task OrTrioEkibi_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateOrTrioEkibiCommand();
            //propertyler buraya yazılacak 
            //command.OrTrioEkibiName = "test";

            _orTrioEkibiRepository.Setup(x => x.Query())
                                           .Returns(new List<OrTrioEkibi> { new OrTrioEkibi() { /*TODO:propertyler buraya yazılacak OrTrioEkibiId = 1, OrTrioEkibiName = "test"*/ } }.AsQueryable());

            _orTrioEkibiRepository.Setup(x => x.Add(It.IsAny<OrTrioEkibi>())).Returns(new OrTrioEkibi());

            var handler = new CreateOrTrioEkibiCommandHandler(_orTrioEkibiRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task OrTrioEkibi_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateOrTrioEkibiCommand();
            //command.OrTrioEkibiName = "test";

            _orTrioEkibiRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrTrioEkibi, bool>>>()))
                        .ReturnsAsync(new OrTrioEkibi() { /*TODO:propertyler buraya yazılacak OrTrioEkibiId = 1, OrTrioEkibiName = "deneme"*/ });

            _orTrioEkibiRepository.Setup(x => x.Update(It.IsAny<OrTrioEkibi>())).Returns(new OrTrioEkibi());

            var handler = new UpdateOrTrioEkibiCommandHandler(_orTrioEkibiRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orTrioEkibiRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task OrTrioEkibi_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteOrTrioEkibiCommand();

            _orTrioEkibiRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrTrioEkibi, bool>>>()))
                        .ReturnsAsync(new OrTrioEkibi() { /*TODO:propertyler buraya yazılacak OrTrioEkibiId = 1, OrTrioEkibiName = "deneme"*/});

            _orTrioEkibiRepository.Setup(x => x.Delete(It.IsAny<OrTrioEkibi>()));

            var handler = new DeleteOrTrioEkibiCommandHandler(_orTrioEkibiRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orTrioEkibiRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

