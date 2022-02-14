using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;
using MQTTnet.Client.Subscribing;
using Newtonsoft.Json;
using Serilog;
using Skipass.Workers.Handlers;
using Skipass.Workers.Messages;

namespace Skipass.Workers;

internal sealed class ScannersWorker : IHostedService
{
    private readonly IMqttClientOptions options;
    private readonly IMqttClient client;
    private readonly ILogger logger;
    private readonly IServiceProvider serviceProvider;

    public ScannersWorker(ILogger logger, IServiceProvider serviceProvider)
    {

        this.logger = logger.ForContext<ScannersWorker>();
        this.serviceProvider = serviceProvider;
        this.options = new MqttClientOptionsBuilder()
            .WithClientId("ScannersWorker")
            .WithTcpServer("localhost", 1883)
            // .WithConfiguredTls()
            .WithCredentials("cplotek", "cplotek")
            .Build();
        var factory = new MqttFactory();
        this.client = factory.CreateMqttClient();
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        this.logger.Information("Starting {WorkerName}", this.GetType().Name);
        await client.ConnectAsync(options, cancellationToken);
        await client.SubscribeAsync(new MqttClientSubscribeOptionsBuilder().WithTopicFilter("skipass_scanner/+").Build());

        client.UseApplicationMessageReceivedHandler(async e =>
        {
            try
            {
                var payload = JsonConvert.DeserializeObject<IMessage>(Encoding.UTF8.GetString(e.ApplicationMessage.Payload));
                var services = this.serviceProvider.CreateScope().ServiceProvider;

                IMessage response = payload switch
                {
                    CardHistoryRequest data => await services.GetRequiredService<CardHistoryRequestHandler>().Handle(data),
                    CardValidityRequest data => await services.GetRequiredService<CardValidityRequestHandler>().Handle(data),
                    CardHistoryResponse => NullMessage.Instance,
                    CardValidityResponse => NullMessage.Instance,
                    _ => throw new InvalidOperationException($"{this.GetType().Name} cannot handle {payload?.GetType().Name}"),
                };

                if (response is not NullMessage)
                {
                    var message = new MqttApplicationMessageBuilder()
                        .WithTopic(e.ApplicationMessage.Topic)
                        .WithPayload(JsonConvert.SerializeObject(response))
                        .Build();

                    Task.Run(async () =>
                    {
                        await Task.Delay(500);
                        await client.PublishAsync(message, CancellationToken.None);
                    });
                }
            }
            catch (Exception ex)
            {
                this.logger.Error(ex, "Unknown exception");
            }
        });
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        this.logger.Information("Stopping {WorkerName}", this.GetType().Name);
        await client.DisconnectAsync(new MqttClientDisconnectOptions(), cancellationToken);
        client.Dispose();
    }
}