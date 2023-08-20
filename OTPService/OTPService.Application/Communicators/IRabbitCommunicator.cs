using OTPService.Application.Messages;

namespace OTPService.Application.Communicators;

public interface IRabbitCommunicator
{
    void SendMessageToQueue(string queueName, IRabbitMessage message);
    void SendMessageToExchange(string exchangeName, IRabbitMessage message);
    IRabbitMessage ReceiveMessageFromQueue(string queueName);
    IRabbitMessage ReceiveMessageFromExchange(string exchangeName);
}