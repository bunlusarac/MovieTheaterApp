using LoyaltyService.Infrastructure.Messages;

namespace LoyaltyService.Infrastructure.Communicators;

public interface IRabbitCommunicator
{
    void SendMessageToQueue(string queueName, RabbitMessage message);
    void SendMessageToExchange(string exchangeName, RabbitMessage message);
    RabbitMessage ReceiveMessageFromQueue(string queueName);
    RabbitMessage ReceiveMessageFromExchange(string exchangeName);
}