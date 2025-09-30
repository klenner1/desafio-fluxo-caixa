using Confluent.Kafka;
using Dominio.Comum;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infraestrutura.Publicadores;

public class KafkaPublicador(ProducerConfig config) : IPublicador
{
    private readonly IProducer<Null, string> _producer = new ProducerBuilder<Null, string>(config).Build();

    public async Task Publicar<T>(string topico, T evento, CancellationToken cancellationToken)
    {
        var mensagem = new Message<Null, string> { Value = JsonSerializer.Serialize(evento) };
        await _producer.ProduceAsync(topico, mensagem, cancellationToken);
    }
}


public class RabbitMqPublicador(IOptions<RabbitMqConfiguration> options) : IPublicador
{
    private readonly IOptions<RabbitMqConfiguration> _options = options;

    public async Task Publicar<T>(string topico, T evento, CancellationToken cancellationToken)
    {
        var factory = new ConnectionFactory()
        {
            HostName = _options.Value.HostName,
            Port = _options.Value.Port,
            UserName = _options.Value.UserName,
            Password = _options.Value.Password
        };
        using var connection = await factory.CreateConnectionAsync(cancellationToken);
        using var channel = await connection.CreateChannelAsync(cancellationToken: cancellationToken);
        await channel.ExchangeDeclareAsync(
            exchange: _options.Value.Exchange,
            type: "direct",
            cancellationToken: cancellationToken);

        string message = JsonSerializer.Serialize(evento);
        var body = Encoding.UTF8.GetBytes(message);


        var properties = new BasicProperties
        {
            Persistent = true,
            ContentType = "application/json",
            ContentEncoding = "UTF-8"
        };

        await channel.BasicPublishAsync(
            exchange: topico,
            routingKey: string.Empty,
            mandatory: false,
            basicProperties: properties,
            body: body,
            cancellationToken: cancellationToken);
    }
}


public class RabbitMqConfiguration
{
    public string HostName { get; set; } = null!;
    public string Exchange { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public int Port { get; set; }
}
