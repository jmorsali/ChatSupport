using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Kernel;
using AutoFixture.Xunit2;
using ChatSessionCoordinator.AgentPool;
using ChatSessionCoordinator.AgentQueue;
using ChatSessionCoordinator.Configurations;
using ChatSessionCoordinator.Coordinator;
using ChatSessionCoordinator.Models.Entities;
using ChatSessionCoordinator.SessionQueue;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace ChatSessionCoordinatorTest;

public class ChatSessionCoordinatorTest
{
    private readonly IFixture _fixture;
    private readonly ISessionCoordinator _sessionCoordinatorService;

    private readonly Mock<IAgentPool> _agentPool;
    private readonly Mock<ISessionQueue> _sessionQueue;

    private const int OVERFLOWCOUNT = 6;
    public ChatSessionCoordinatorTest()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());

        _fixture.Customizations.Add(
            new TypeRelay(
                typeof(IAgentQueue),
                typeof(InMemoryAgentQueue)));

        var options = new Mock<IOptions<SessionCoordinatorConfiguration>>();
        options.Setup(o => o.Value).Returns(new SessionCoordinatorConfiguration { OverFlowCount = OVERFLOWCOUNT });
        _fixture.Inject(options.Object);

        ILogger<ISessionCoordinator> logger = Mock.Get(_fixture.Freeze<ILogger<ISessionCoordinator>>()).Object;


        _agentPool = Mock.Get(_fixture.Freeze<IAgentPool>());
        _sessionQueue = Mock.Get(_fixture.Freeze<ISessionQueue>());
        _sessionCoordinatorService = new SessionCoordinator(_sessionQueue.Object, _agentPool.Object, options.Object, logger);
    }

    [Fact]
    public async void OnProcessChat_WhenGetAvailableAgent_IsNull_MustKickOverflow()
    {
        //Arrange
        ActorChat chat = new ActorChat { ChatId = Guid.NewGuid(), Title = "", MessageBody = "" };
        _agentPool.Setup(x => x.GetAvailableAgent()).Returns((Agent?)null);
        //Act

        await _sessionCoordinatorService.ProcessChatQueue(chat);

        //Assert
        _agentPool.Verify(x => x.KickOverflowTeam(OVERFLOWCOUNT), Times.Once);
    }
}
