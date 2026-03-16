using CAPGEMINI_CROPDEAL.Events;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
namespace CAPGEMINI_CROPDEAL.Services;
public class CropEventPublisher
{
    private readonly RabbitMQService _rabbitService;

    public CropEventPublisher(RabbitMQService rabbitService)
    {
        _rabbitService = rabbitService;
    }

    public void PublishCrop(CropPublishedEvent cropEvent)
    {
        var channel = _rabbitService.GetChannel();

        var message = JsonSerializer.Serialize(cropEvent);
        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(
            exchange: "",
            routingKey: "crop_notifications",
            basicProperties: null,
            body: body
        );
    }
}