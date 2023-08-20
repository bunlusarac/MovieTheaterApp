using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OTPService.Infrastructure.Messages;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OTPService.Application.Communicators;
using OTPService.Application.Messages;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace OTPService.Infrastructure.Communicators;

public class RabbitCommunicator: IRabbitCommunicator, IDisposable
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly IConfiguration _configuration;
    private readonly ILogger<RabbitCommunicator> _logger;
    private readonly IRabbitMessageHandler _messageHandler;

    public RabbitCommunicator(IConfiguration configuration, ILogger<RabbitCommunicator> logger, IRabbitMessageHandler messageHandler)
    {
        _configuration = configuration;
        _logger = logger;
        _messageHandler = messageHandler;

        var rabbitCfg = configuration.GetSection("RabbitMQ");
        
        var factory = new ConnectionFactory
        {
            HostName = rabbitCfg["HostName"],
            Port = int.Parse(rabbitCfg["Port"]),
            UserName = rabbitCfg["UserName"],
            Password = rabbitCfg["Password"],
            VirtualHost = rabbitCfg["VirtualHost"],
        };

        //Protocol version negotiation, authentication.....
        _connection = factory.CreateConnection();
        
        //API
        _channel = _connection.CreateModel();
    }

    public void SendMessageToQueue(string queueName, IRabbitMessage message)
    {
        _channel.QueueDeclare(
            queue: queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var body = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
        _channel.BasicPublish(exchange: string.Empty, routingKey: queueName, basicProperties: null, body: body);
    }

    public void SendMessageToExchange(string exchangeName, IRabbitMessage message)
    {
        _channel.ExchangeDeclare(exchangeName, type: ExchangeType.Fanout);

        var body = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
        
        _channel.BasicPublish(exchange: exchangeName, routingKey: string.Empty, basicProperties: null, body: body);
    }
    
    public IRabbitMessage ReceiveMessageFromQueue(string queueName)
    {
        _channel.QueueDeclare(
            queue: queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var consumer = new EventingBasicConsumer(_channel);
        IRabbitMessage? message = null;

        //Callback for EventingBasicConsumer.Received event
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var messageJson = System.Text.Encoding.UTF8.GetString(body);
            message = JsonConvert.DeserializeObject<IRabbitMessage>(messageJson);
            _logger.Log(LogLevel.Information, messageJson);
            if(message is not null) _messageHandler.Handle(message.Event, messageJson);
        };

        _channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
        
        return message;
    }

    public IRabbitMessage ReceiveMessageFromExchange(string exchangeName)
    {
        _channel.ExchangeDeclare(exchangeName, type: ExchangeType.Fanout);

        //Random queue generation and binding, per consumer
        var queueName = _channel.QueueDeclare().QueueName;
        _channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: string.Empty);
        
        var consumer = new EventingBasicConsumer(_channel);
        IRabbitMessage? message = null;

        //Callback for EventingBasicConsumer.Received event
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var messageJson = System.Text.Encoding.UTF8.GetString(body);
            message = JsonConvert.DeserializeObject<RabbitMessage>(messageJson);
            _logger.Log(LogLevel.Information, messageJson);
            if(message is not null) _messageHandler.Handle(message.Event, messageJson);
        };
        
        _channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
        return message;
    }
    
    public void Dispose()
    {
        _channel.Close();
        _connection.Close();
    }
}