using AutoMapper;
using Bmb.Core.Domain.Contracts;
using Bmb.Corporate.Customer.MasterData.Domain.Segment.Commands.CreateSegmentCommand.v1;
using Bmb.Corporate.Customer.MasterData.Domain.Segment.Contracts.Repositories.v1;
using Moq;
using Xunit;

namespace Bmb.Corporate.Segmentr.MasterData.Domain.Tests.Unit.Segment.Commands.CreateSegmentCommand.v1;

public class CreateSegmentCommandHandlerTests
{
    private const int SegmentId = 10;
    private readonly CreateSegmentCommandHandler _subject;
    private readonly Customer.MasterData.Domain.Segment.Commands.CreateSegmentCommand.v1.CreateSegmentCommand _command;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ISegmentRepository> _SegmentRepositoryMock;

    public CreateSegmentCommandHandlerTests()
    {
        _command = new Customer.MasterData.Domain.Segment.Commands.CreateSegmentCommand.v1.CreateSegmentCommand
        {
            Name = "julierme carvalho",
            IsActive = true,
            Abbreviations = "jc2",
            UserId = "julierme",
        };
        
        _SegmentRepositoryMock = new Mock<ISegmentRepository>();
        _mapperMock = new Mock<IMapper>();

        _mapperMock.Setup(x =>
                x.Map<Customer.MasterData.Domain.Segment.Entities.v1.Segment>(It.IsAny<Customer.MasterData.Domain.Segment.Commands.CreateSegmentCommand.v1.CreateSegmentCommand>()))
            .Returns(new Customer.MasterData.Domain.Segment.Entities.v1.Segment { });

        _SegmentRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Customer.MasterData.Domain.Segment.Entities.v1.Segment>(), 
                It.IsAny<CancellationToken>())).Returns(Task.FromResult(SegmentId));
        
        _subject = new CreateSegmentCommandHandler(Mock.Of<INotificationContext>(),
            _mapperMock.Object, _SegmentRepositoryMock.Object);
    }

    [Fact(DisplayName = "Should map from command to entity")]
    public async Task Should_map_from_command_to_entity()
    {
        await _subject.Handle(_command, CancellationToken.None);
        
        _mapperMock.Verify(x => x.Map<Customer.MasterData.Domain.Segment.Entities.v1.Segment>(_command), Times.Once);
    }

    [Fact(DisplayName = "Should add entity into repository")]
    public async Task Should_add_entity_into_repository()
    {
        await _subject.Handle(_command, CancellationToken.None);
        
        _SegmentRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Customer.MasterData.Domain.Segment.Entities.v1.Segment>(), 
            It.IsAny<CancellationToken>()), Times.Once);
    }
}