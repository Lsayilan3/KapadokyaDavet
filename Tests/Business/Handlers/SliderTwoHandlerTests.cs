
using Business.Handlers.SliderTwoes.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.SliderTwoes.Queries.GetSliderTwoQuery;
using Entities.Concrete;
using static Business.Handlers.SliderTwoes.Queries.GetSliderTwoesQuery;
using static Business.Handlers.SliderTwoes.Commands.CreateSliderTwoCommand;
using Business.Handlers.SliderTwoes.Commands;
using Business.Constants;
using static Business.Handlers.SliderTwoes.Commands.UpdateSliderTwoCommand;
using static Business.Handlers.SliderTwoes.Commands.DeleteSliderTwoCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class SliderTwoHandlerTests
    {
        Mock<ISliderTwoRepository> _sliderTwoRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _sliderTwoRepository = new Mock<ISliderTwoRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task SliderTwo_GetQuery_Success()
        {
            //Arrange
            var query = new GetSliderTwoQuery();

            _sliderTwoRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<SliderTwo, bool>>>())).ReturnsAsync(new SliderTwo()
//propertyler buraya yazılacak
//{																		
//SliderTwoId = 1,
//SliderTwoName = "Test"
//}
);

            var handler = new GetSliderTwoQueryHandler(_sliderTwoRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.SliderTwoId.Should().Be(1);

        }

        [Test]
        public async Task SliderTwo_GetQueries_Success()
        {
            //Arrange
            var query = new GetSliderTwoesQuery();

            _sliderTwoRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<SliderTwo, bool>>>()))
                        .ReturnsAsync(new List<SliderTwo> { new SliderTwo() { /*TODO:propertyler buraya yazılacak SliderTwoId = 1, SliderTwoName = "test"*/ } });

            var handler = new GetSliderTwoesQueryHandler(_sliderTwoRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<SliderTwo>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task SliderTwo_CreateCommand_Success()
        {
            SliderTwo rt = null;
            //Arrange
            var command = new CreateSliderTwoCommand();
            //propertyler buraya yazılacak
            //command.SliderTwoName = "deneme";

            _sliderTwoRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<SliderTwo, bool>>>()))
                        .ReturnsAsync(rt);

            _sliderTwoRepository.Setup(x => x.Add(It.IsAny<SliderTwo>())).Returns(new SliderTwo());

            var handler = new CreateSliderTwoCommandHandler(_sliderTwoRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _sliderTwoRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task SliderTwo_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateSliderTwoCommand();
            //propertyler buraya yazılacak 
            //command.SliderTwoName = "test";

            _sliderTwoRepository.Setup(x => x.Query())
                                           .Returns(new List<SliderTwo> { new SliderTwo() { /*TODO:propertyler buraya yazılacak SliderTwoId = 1, SliderTwoName = "test"*/ } }.AsQueryable());

            _sliderTwoRepository.Setup(x => x.Add(It.IsAny<SliderTwo>())).Returns(new SliderTwo());

            var handler = new CreateSliderTwoCommandHandler(_sliderTwoRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task SliderTwo_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateSliderTwoCommand();
            //command.SliderTwoName = "test";

            _sliderTwoRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<SliderTwo, bool>>>()))
                        .ReturnsAsync(new SliderTwo() { /*TODO:propertyler buraya yazılacak SliderTwoId = 1, SliderTwoName = "deneme"*/ });

            _sliderTwoRepository.Setup(x => x.Update(It.IsAny<SliderTwo>())).Returns(new SliderTwo());

            var handler = new UpdateSliderTwoCommandHandler(_sliderTwoRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _sliderTwoRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task SliderTwo_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteSliderTwoCommand();

            _sliderTwoRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<SliderTwo, bool>>>()))
                        .ReturnsAsync(new SliderTwo() { /*TODO:propertyler buraya yazılacak SliderTwoId = 1, SliderTwoName = "deneme"*/});

            _sliderTwoRepository.Setup(x => x.Delete(It.IsAny<SliderTwo>()));

            var handler = new DeleteSliderTwoCommandHandler(_sliderTwoRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _sliderTwoRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

