using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Bekk.Pact.Provider.Config;
using Bekk.Pact.Provider.Contracts;
using Bekk.Pact.Provider.Web;
using Bekk.Pact.Provider.Web.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;

namespace Provider.WebApi.Test
{
    public class PactTests
    {
        private readonly ITestOutputHelper _output;

        public PactTests(ITestOutputHelper output) => this._output = output;

        [Theory]
        [MemberData(nameof(RunPacts))]
        public void VerifyAllPacts(ITestResult result)
        {
            Assert.True(result.Success, result.ToString());
            _output.WriteLine(result.ToString());
        }

        public static IEnumerable<object[]> RunPacts()
        {
            var runner = new PactRunner<WebApplication.Startup>(
                Configuration
                    .With
                    .PublishPath(
                        @"/home/rramos/git/Bekk.Pact.Demo/Consumer.Console.Test/bin/Debug/netcoreapp3.1/pacts/my_provider_id/Consumidor/"),
                new ProviderStateSetup());

            return runner.Verify("my_provider_id")
                .Result
                .Select(r => new object[] {r});
        }
    }

    public class ProviderStateSetup : IProviderStateSetup
    {
        public IEnumerable<Claim> GetClaims(string providerState) => default;

        public Action<IServiceCollection> ConfigureServices(string providerState) => default;
    }
}