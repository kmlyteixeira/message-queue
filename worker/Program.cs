using Microsoft.Extensions.Configuration;

public class Program
{
    static async Task Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("./appsettings.json", optional: true, reloadOnChange: true)
            .Build();

        string connectionString = configuration["Azure:ConnectionString"] ?? "";
        string queueName = configuration["Azure:QueueName"] ?? "";

        var processor = new ServiceBusMessageProcessor();
        await processor.Start(connectionString, queueName);
    }
}
