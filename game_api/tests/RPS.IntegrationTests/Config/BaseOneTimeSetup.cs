using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;
using RPS.API;
using RPS.DataAccess.Persistence;

namespace RPS.IntegrationTests.Config;

[SetUpFixture]
public class BaseOneTimeSetup
{
    protected HttpClient Client;
    protected IHost Host;

    [OneTimeSetUp]
    public async Task RunBeforeAnyTestsAsync()
    {
        Host = await GetNewHostAsync();

        Client = await GetNewClient(Host);
    }

    protected static async Task<IHost> GetNewHostAsync()
    {
        var hostBuilder = new HostBuilder()
            .ConfigureWebHost(webHost =>
            {
                webHost.UseTestServer();
                webHost.UseStartup<Startup>();
                webHost.ConfigureAppConfiguration((_, configBuilder) =>
                {
                    configBuilder.AddInMemoryCollection(
                        new Dictionary<string, string>
                        {
                            ["Database:UseInMemoryDatabase"] = "true"
                        });
                });
            });

        var host = await hostBuilder.StartAsync();

        var context = host.Services.GetRequiredService<DatabaseContext>();

        await context.SaveChangesAsync();

        return host;
    }

    private static async Task<HttpClient> GetNewClient(IHost host)
    {
        return host.GetTestClient(); ;
    }

    [OneTimeTearDown]
    public void RunAfterAnyTests() { }
}
