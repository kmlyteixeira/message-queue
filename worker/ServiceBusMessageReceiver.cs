using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;

public class ServiceBusMessageReceiver
{
    public async Task<List<MessageResponseDto>> PeekMessages(string connectionString, string queueName)
    {
        var client = ServiceBusCommon.CreateClient(connectionString);
        var receiver = client.CreateReceiver(queueName);

        List<MessageResponseDto> messages = new List<MessageResponseDto>();


        try
        {
            var queuedMessages = await receiver.PeekMessagesAsync(50);
            if (queuedMessages == null) return messages;
            
            foreach (ServiceBusReceivedMessage message in queuedMessages)
            {
                var body = JsonConvert.DeserializeObject<MessageBodyDto>(message.Body.ToString());
                string id = message.MessageId;
                DateTimeOffset enqueuedTime = message.EnqueuedTime;
                string state = message.State.ToString();
                
                messages.Add(new MessageResponseDto
                {
                    Id = id,
                    EnqueuedAt = enqueuedTime,
                    State = state,
                    BodySize = message.Body.ToString().Length.ToString(),
                    Message = new MessageBodyDto
                    {
                        Title = body.Title,
                        Url = body.Url,
                        ImageUrl = body.ImageUrl,
                        Summary = body.Summary
                    }
                });
            }
        }
        finally
        {
            await receiver.DisposeAsync();
            await client.DisposeAsync();
        }

        return messages;
    }
}