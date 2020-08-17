using System;
using System.Net;
using System.Threading.Tasks;
using Bekk.Pact.Consumer.Builders;
using Bekk.Pact.Consumer.Extensions;
using FluentAssertions;
using Xunit;

namespace Consumer.Console.Test
{
    public class UnitTest1 : IClassFixture<PactApiFixture>
    {
        private UserServiceClient client;
        const string ProviderName = "my_provider_id";
        
        public UnitTest1(PactApiFixture apiFixture)
        {
            client = new UserServiceClient("http://localhost:1234");
        }

        [Fact]
        public async Task DeveObterAListaDeUsuários()
        {
            const int id = 1;

            using (await PactBuilder.Build()
                .ForProvider(ProviderName)
                .ForConsumer("Consumidor")
                .Given($"There is an employee with id {id} and one time entry")
                .WhenRequesting($"/User")
                .WithHeader("Accept", "application/json")
                .ThenRespondsWith(HttpStatusCode.OK)
                .WithJsonBody(new[]
                    {
                        new {Name = "Rodrigo"}
                    }
                )
                .InPact())
            {
                var users = await client.GetUsers();

                users
                    .Should()
                    .BeEquivalentTo(new UserApiResponse
                    {
                        Name = "Rodrigo"
                    });
            }
        }
    }
}