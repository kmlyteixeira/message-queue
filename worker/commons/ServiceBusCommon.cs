using Azure.Messaging.ServiceBus;

public class ServiceBusCommon
{
    public static ServiceBusClient CreateClient(string connectionString)
    {
        var clientOptions = new ServiceBusClientOptions()
        {
            TransportType = ServiceBusTransportType.AmqpWebSockets
        };
        
        return new ServiceBusClient(connectionString, clientOptions);
    }
}