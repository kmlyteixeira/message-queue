using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

[Route("api/[controller]")]
[ApiController]
public class ServiceBusController : ControllerBase
{
    private readonly ServiceBusMessageProcessor _processor;
    private readonly ServiceBusMessageReceiver _receiver;

    public ServiceBusController(
        ServiceBusMessageProcessor processor,
        ServiceBusMessageReceiver receiver)
    {
        _processor = processor;
        _receiver = receiver;
    }

    [HttpPost("start")]
    public async Task<IActionResult> StartProcessor()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("./appsettings.json", optional: true, reloadOnChange: true)
            .Build();

        string connectionString = configuration["Azure:ConnectionString"] ?? "";
        string queueName = configuration["Azure:QueueName"] ?? "";

        await _processor.Start(connectionString, queueName);

        return Ok("Service Bus Message Processor started.");
    }

    [HttpGet("messages")]
    public async Task<List<MessageResponseDto>> GetProcessorStatus()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("./appsettings.json", optional: true, reloadOnChange: true)
            .Build();

        string connectionString = configuration["Azure:ConnectionString"] ?? "";
        string queueName = configuration["Azure:QueueName"] ?? "";

        var messages = await _receiver.PeekMessages(connectionString, queueName);

        return messages;
    }
}
