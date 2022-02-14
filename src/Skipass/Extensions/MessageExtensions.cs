using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using JsonSubTypes;
using MQTTnet.Client.Options;
using Newtonsoft.Json;
using Skipass.Workers.Messages;

namespace Skipass.Extenstions;

static class MessageExtensions
{
    public static JsonConverter CreateMessagesSubtypes()
    {
        return JsonSubtypesConverterBuilder.Of<IMessage>("Discriminator")
            .RegisterSubtype<CardHistoryRequest>(nameof(CardHistoryRequest))
            .RegisterSubtype<CardHistoryResponse>(nameof(CardHistoryResponse))
            .RegisterSubtype<CardValidityRequest>(nameof(CardValidityRequest))
            .RegisterSubtype<CardValidityResponse>(nameof(CardValidityResponse))
            .RegisterSubtype<EntryConfirmation>(nameof(EntryConfirmation))
            .RegisterSubtype<EntryRequest>(nameof(EntryRequest))
            .RegisterSubtype<EntryResponse>(nameof(EntryResponse))
            .SerializeDiscriminatorProperty()
            .Build();

    }

    public static MqttClientOptionsBuilder WithConfiguredTls(this MqttClientOptionsBuilder builder)
    {
        var caCert = X509Certificate.CreateFromCertFile("ca.crt");
        var clientCert = new X509Certificate2("aplikacja.pfx", "pass");

        return builder
            .WithCredentials("cplotek", "cplotek")
            .WithTcpServer("bruh", 10001)
            .WithTls(new MqttClientOptionsBuilderTlsParameters
            {
                UseTls = true,
                SslProtocol = System.Security.Authentication.SslProtocols.Tls12,
                Certificates = new List<X509Certificate>()
                {
                    caCert, clientCert
                },

                IgnoreCertificateRevocationErrors = true
            });
    }
}