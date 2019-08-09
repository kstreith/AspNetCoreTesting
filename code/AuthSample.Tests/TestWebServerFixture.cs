using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using AuthSample.Tests.MockAuthentication;

namespace AuthSample.Tests
{
    public class TestWebServerFixture : WebApplicationFactory<Startup>
    {
        public class TestStartup : Startup
        {
            public MockRoles MockRoles { get; }
            public TestStartup(IConfiguration config) : base(config)
            {
                MockRoles = new MockRoles();
            }

            protected override void ConfigureAuthentication(IServiceCollection services)
            {
                services.AddSingleton(MockRoles);
                services.AddAuthentication(MockAuthenticationHandler.AuthenticationScheme)
                    .AddScheme<MockAuthenticationOptions, MockAuthenticationHandler>(MockAuthenticationHandler.AuthenticationScheme, _ => { });
            }
        }

        public TestWebServerFixture()
        {
            var configBuilder = new ConfigurationBuilder()
                .AddInMemoryCollection();
            _startup = new TestStartup(configBuilder.Build());
        }

        public MockRoles MockRoles => _startup?.MockRoles;
        private readonly TestStartup _startup;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services => { services.AddSingleton<IStartup>(_startup); });
        }
    }
}
