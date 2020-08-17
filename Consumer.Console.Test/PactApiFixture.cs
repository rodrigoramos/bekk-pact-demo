using System;
using System.IO;
using Bekk.Pact.Consumer.Config;

namespace Consumer.Console.Test
{
    public class PactApiFixture : IDisposable
    {
        public Bekk.Pact.Consumer.Server.Context Context { get; }

        public PactApiFixture()
        {
            var path = Directory.GetCurrentDirectory();
            
            Context = new Bekk.Pact.Consumer.Server.Context(
                    Configuration.With
                        .PublishPath(path)
                        // .BrokerUrl("https://mybroker.pact.dius.com.au")
                        // .PublishPathInTemp("published_pacts")
                        .LogFileInTemp("pact_log.txt")
                        .MockServiceBaseUri("http://localhost:1234"))
                .WithVersion(GetType())
                .ForConsumer("my_consumer_id");
        }

        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}