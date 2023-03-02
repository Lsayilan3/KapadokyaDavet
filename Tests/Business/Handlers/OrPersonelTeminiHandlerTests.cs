
using Business.Handlers.OrPersonelTeminis.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.OrPersonelTeminis.Queries.GetOrPersonelTeminiQuery;
using Entities.Concrete;
using static Business.Handlers.OrPersonelTeminis.Queries.GetOrPersonelTeminisQuery;
using static Business.Handlers.OrPersonelTeminis.Commands.CreateOrPersonelTeminiCommand;
using Business.Handlers.OrPersonelTeminis.Commands;
using Business.Constants;
using static Business.Handlers.OrPersonelTeminis.Commands.UpdateOrPersonelTeminiCommand;
using static Business.Handlers.OrPersonelTeminis.Commands.DeleteOrPersonelTeminiCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class OrPersonelTeminiHandlerTests
    {
        Mock<IOrPersonelTeminiRepository> _orPersonelTeminiRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _orPersonelTeminiRepository = new Mock<IOrPersonelTeminiRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task OrPersonelTemini_GetQuery_Success()
        {
            //Arrange
            var query = new GetOrPersonelTeminiQuery();

            _orPersonelTeminiRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrPersonelTemini, bool>>>())).ReturnsAsync(new OrPersonelTemini()
//propertyler buraya yazılacak
//{																		
//OrPersonelTeminiId = 1,
//OrPersonelTeminiName = "Test"
//}
);

            var handler = new GetOrPersonelTeminiQueryHandler(_orPersonelTeminiRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.OrPersonelTeminiId.Should().Be(1);

        }

        [Test]
        public async Task OrPersonelTemini_GetQueries_Success()
        {
            //Arrange
            var query = new GetOrPersonelTeminisQuery();

            _orPersonelTeminiRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<OrPersonelTemini, bool>>>()))
                        .ReturnsAsync(new List<OrPersonelTemini> { new OrPersonelTemini() { /*TODO:propertyler buraya yazılacak OrPersonelTeminiId = 1, OrPersonelTeminiName = "test"*/ } });

            var handler = new GetOrPersonelTeminisQueryHandler(_orPersonelTeminiRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<OrPersonelTemini>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task OrPersonelTemini_CreateCommand_Success()
        {
            OrPersonelTemini rt = null;
            //Arrange
            var command = new CreateOrPersonelTeminiCommand();
            //propertyler buraya yazılacak
            //command.OrPersonelTeminiName = "deneme";

            _orPersonelTeminiRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrPersonelTemini, bool>>>()))
                        .ReturnsAsync(rt);

            _orPersonelTeminiRepository.Setup(x => x.Add(It.IsAny<OrPersonelTemini>())).Returns(new OrPersonelTemini());

            var handler = new CreateOrPersonelTeminiCommandHandler(_orPersonelTeminiRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orPersonelTeminiRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task OrPersonelTemini_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateOrPersonelTeminiCommand();
            //propertyler buraya yazılacak 
            //command.OrPersonelTeminiName = "test";

            _orPersonelTeminiRepository.Setup(x => x.Query())
                                           .Returns(new List<OrPersonelTemini> { new OrPersonelTemini() { /*TODO:propertyler buraya yazılacak OrPersonelTeminiId = 1, OrPersonelTeminiName = "test"*/ } }.AsQueryable());

            _orPersonelTeminiRepository.Setup(x => x.Add(It.IsAny<OrPersonelTemini>())).Returns(new OrPersonelTemini());

            var handler = new CreateOrPersonelTeminiCommandHandler(_orPersonelTeminiRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task OrPersonelTemini_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateOrPersonelTeminiCommand();
            //command.OrPersonelTeminiName = "test";

            _orPersonelTeminiRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrPersonelTemini, bool>>>()))
                        .ReturnsAsync(new OrPersonelTemini() { /*TODO:propertyler buraya yazılacak OrPersonelTeminiId = 1, OrPersonelTeminiName = "deneme"*/ });

            _orPersonelTeminiRepository.Setup(x => x.Update(It.IsAny<OrPersonelTemini>())).Returns(new OrPersonelTemini());

            var handler = new UpdateOrPersonelTeminiCommandHandler(_orPersonelTeminiRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orPersonelTeminiRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task OrPersonelTemini_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteOrPersonelTeminiCommand();

            _orPersonelTeminiRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrPersonelTemini, bool>>>()))
                        .ReturnsAsync(new OrPersonelTemini() { /*TODO:propertyler buraya yazılacak OrPersonelTeminiId = 1, OrPersonelTeminiName = "deneme"*/});

            _orPersonelTeminiRepository.Setup(x => x.Delete(It.IsAny<OrPersonelTemini>()));

            var handler = new DeleteOrPersonelTeminiCommandHandler(_orPersonelTeminiRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orPersonelTeminiRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

