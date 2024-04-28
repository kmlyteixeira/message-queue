using Azure.Messaging.ServiceBus;

public class ServiceBusMessageProcessor
{
    public async Task Start(string connectionString, string queueName)
    {
        var client = ServiceBusCommon.CreateClient(connectionString);
        var processor = client.CreateProcessor(queueName, new ServiceBusProcessorOptions());

        string filePath = Path.Combine(Environment.CurrentDirectory, "messages.txt");

        try
        {
            processor.ProcessMessageAsync += async args =>
            {
                string body = args.Message.Body.ToString();
                Console.WriteLine($"Received: {body}");

                await WriteMessageToFile(filePath, body);

                await args.CompleteMessageAsync(args.Message);
            };

            processor.ProcessErrorAsync += ErrorHandler;
            await processor.StartProcessingAsync();

            Console.WriteLine("Press any key to end the processing");
            Console.Read();

            Console.WriteLine("\nStopping the receiver...");
            await processor.StopProcessingAsync();
            Console.WriteLine("Stopped receiving messages");
        }
        finally
        {
            await processor.DisposeAsync();
            await client.DisposeAsync();
        }
    }

    private static async Task WriteMessageToFile(string filePath, string message)
    {
        try
        {
            using (StreamWriter writer = File.AppendText(filePath))
            {
                await writer.WriteLineAsync(message);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing to file: {ex.Message}");
        }
    }

    private static Task ErrorHandler(ProcessErrorEventArgs args)
    {
        Console.WriteLine(args.Exception.ToString());
        return Task.CompletedTask;
    }
}