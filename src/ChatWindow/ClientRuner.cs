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
                Console.WriteLine($"trying to create chat Id {chatId}");
                var result = client.CreateSupportRequest(chatId, $"title{random.Next(1, 1000000)}",
                    $"message_{random.Next(1, 1000000)}").Result;
                if (result)
                {
                    chatIds.Add(chatId);
                    Console.WriteLine($"chat Id {chatId} created");
                }
                else
                {
                    Console.WriteLine($"support refused for {chatId}", ConsoleColor.Red);
                    Console.WriteLine("Press any key to continue.....", ConsoleColor.White);
                    Console.ReadLine();
                }

                var sleepDuration = random.Next(2000, 15000);
                Console.WriteLine($"create support sleep for {sleepDuration} ms");
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
                    var result = client.PollSupportRequest(chatId).Result;
                    //logger.LogWarning($"chat Id {chatId} status is ==> {result.Status}");

                    var sleepDuration = random.Next(1000, 3000);
                    //logger.LogWarning($"poll sleep for {sleepDuration} ms");
                    Task.Delay(TimeSpan.FromSeconds(sleepDuration));
                }
                Task.Delay(TimeSpan.FromSeconds(1000));
                //Console.WriteLine("Press any key to poll.....");
                //Console.ReadLine();
            }
            catch (Exception ex)
            {
                logger.LogCritical($"exception SupportPolling: {ex}");
            }
        }
    }
}