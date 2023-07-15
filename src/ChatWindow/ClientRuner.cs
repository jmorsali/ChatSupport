using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ChatWindow;

public class ClientRuner
{
    private readonly List<Guid> chatIds;
    public ClientRuner()
    {
        chatIds = new List<Guid>();
    }
    public Task[] StartAsync(ServiceProvider serviceProvider)
    {
        var createTask = Task.Factory.StartNew(() => SupportCreator(serviceProvider));
        var pollTask = Task.Factory.StartNew(() => SupportPolling(serviceProvider));
        return new[] { createTask, pollTask };
    }

    private void SupportCreator(ServiceProvider serviceProvider)
    {
        ILogger logger = serviceProvider.GetService<ILogger<Program>>();
        var client = serviceProvider.GetService<IChatApiClient>();
        var random = new Random();
        while (true)
        {
            try
            {
                var chatId = Guid.NewGuid();
                logger.LogInformation($"trying to create chat Id {chatId}");
                var result = client.CreateSupportRequest(chatId, $"title{random.Next(1, 1000000)}",
                    $"message_{random.Next(1, 1000000)}").Result;
                if (result)
                {
                    chatIds.Add(chatId);
                    logger.LogInformation($"chat Id {chatId} created");
                }
                else
                {
                    logger.LogInformation($"no agent available for {chatId}");
                }

                var sleepDuration = random.Next(2000, 15000);
                logger.LogWarning($"create support sleep for {sleepDuration} ms");
                Task.Delay(TimeSpan.FromSeconds(sleepDuration));
                Console.WriteLine("Press any key to send support.....");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                logger.LogCritical($"exception SupportCreator: {ex}");
            }
        }
    }

    private void SupportPolling(ServiceProvider serviceProvider)
    {
        ILogger logger = serviceProvider.GetService<ILogger<Program>>();
        var client = serviceProvider.GetService<IChatApiClient>();
        var random = new Random();
        while (true)
        {
            try
            {
                var followupChatList = chatIds.ToArray();
                foreach (var chatId in followupChatList)
                {
                    var result =  client.PollSupportRequest(chatId).Result;
                    logger.LogInformation($"chat Id {chatId} status is ==> {result.Status}");

                    var sleepDuration = random.Next(1000, 3000);
                    logger.LogWarning($"poll sleep for {sleepDuration} ms");
                    Task.Delay(TimeSpan.FromSeconds(sleepDuration));
                }
                Task.Delay(TimeSpan.FromSeconds(1000));
                Console.WriteLine("Press any key to poll.....");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                logger.LogCritical($"exception SupportPolling: {ex}");
            }
        }
    }
}