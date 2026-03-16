using RabbitMQ.Client;
using RabbitMQ.Client.Events;
namespace CAPGEMINI_CROPDEAL.Services;
public class RabbitMQService
{
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitMQService()
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost"
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(
            queue: "crop_notifications",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );
    }

    public IModel GetChannel()
    {
        return _channel;
    }
}