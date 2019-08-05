using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace RazorViewComponentSample.Tests
{
    public class TestWebServerFixture : WebApplicationFactory<Startup>
    {
        public class TestStartup : Startup
        {
            public TestStartup(IConfiguration config) : base(config)
            {
            }

            public override void ConfigureMvc(IMvcBuilder mvc)
            {
                var assembly = typeof(RatingViewComponentController).GetTypeInfo().Assembly;
                mvc.AddApplicationPart(assembly);
            }
        }

        public TestWebServerFixture()
        {
            var configBuilder = new ConfigurationBuilder()
                .AddInMemoryCollection();
            _startup = new TestStartup(configBuilder.Build());
        }

        private readonly TestStartup _startup;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services => { services.AddSingleton<IStartup>(_startup); });
        }
    }
}
