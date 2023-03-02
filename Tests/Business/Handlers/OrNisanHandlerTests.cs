
using Business.Handlers.OrNisans.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.OrNisans.Queries.GetOrNisanQuery;
using Entities.Concrete;
using static Business.Handlers.OrNisans.Queries.GetOrNisansQuery;
using static Business.Handlers.OrNisans.Commands.CreateOrNisanCommand;
using Business.Handlers.OrNisans.Commands;
using Business.Constants;
using static Business.Handlers.OrNisans.Commands.UpdateOrNisanCommand;
using static Business.Handlers.OrNisans.Commands.DeleteOrNisanCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class OrNisanHandlerTests
    {
        Mock<IOrNisanRepository> _orNisanRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _orNisanRepository = new Mock<IOrNisanRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task OrNisan_GetQuery_Success()
        {
            //Arrange
            var query = new GetOrNisanQuery();

            _orNisanRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrNisan, bool>>>())).ReturnsAsync(new OrNisan()
//propertyler buraya yazılacak
//{																		
//OrNisanId = 1,
//OrNisanName = "Test"
//}
);

            var handler = new GetOrNisanQueryHandler(_orNisanRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.OrNisanId.Should().Be(1);

        }

        [Test]
        public async Task OrNisan_GetQueries_Success()
        {
            //Arrange
            var query = new GetOrNisansQuery();

            _orNisanRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<OrNisan, bool>>>()))
                        .ReturnsAsync(new List<OrNisan> { new OrNisan() { /*TODO:propertyler buraya yazılacak OrNisanId = 1, OrNisanName = "test"*/ } });

            var handler = new GetOrNisansQueryHandler(_orNisanRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<OrNisan>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task OrNisan_CreateCommand_Success()
        {
            OrNisan rt = null;
            //Arrange
            var command = new CreateOrNisanCommand();
            //propertyler buraya yazılacak
            //command.OrNisanName = "deneme";

            _orNisanRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrNisan, bool>>>()))
                        .ReturnsAsync(rt);

            _orNisanRepository.Setup(x => x.Add(It.IsAny<OrNisan>())).Returns(new OrNisan());

            var handler = new CreateOrNisanCommandHandler(_orNisanRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orNisanRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task OrNisan_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateOrNisanCommand();
            //propertyler buraya yazılacak 
            //command.OrNisanName = "test";

            _orNisanRepository.Setup(x => x.Query())
                                           .Returns(new List<OrNisan> { new OrNisan() { /*TODO:propertyler buraya yazılacak OrNisanId = 1, OrNisanName = "test"*/ } }.AsQueryable());

            _orNisanRepository.Setup(x => x.Add(It.IsAny<OrNisan>())).Returns(new OrNisan());

            var handler = new CreateOrNisanCommandHandler(_orNisanRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task OrNisan_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateOrNisanCommand();
            //command.OrNisanName = "test";

            _orNisanRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrNisan, bool>>>()))
                        .ReturnsAsync(new OrNisan() { /*TODO:propertyler buraya yazılacak OrNisanId = 1, OrNisanName = "deneme"*/ });

            _orNisanRepository.Setup(x => x.Update(It.IsAny<OrNisan>())).Returns(new OrNisan());

            var handler = new UpdateOrNisanCommandHandler(_orNisanRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orNisanRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task OrNisan_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteOrNisanCommand();

            _orNisanRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrNisan, bool>>>()))
                        .ReturnsAsync(new OrNisan() { /*TODO:propertyler buraya yazılacak OrNisanId = 1, OrNisanName = "deneme"*/});

            _orNisanRepository.Setup(x => x.Delete(It.IsAny<OrNisan>()));

            var handler = new DeleteOrNisanCommandHandler(_orNisanRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orNisanRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

