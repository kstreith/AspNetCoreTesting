using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RazorViewComponentSample.Repository;
using System.Reflection;

namespace RazorViewComponentSample.Tests
{
    public class TestWebServerFixture : WebApplicationFactory<Startup>
    {
        public class TestStartup : Startup
        {
            private readonly IAchievementRepository _repository;

            public TestStartup(IConfiguration config, IAchievementRepository repository) : base(config)
            {
                _repository = repository;
            }

            protected override void AddDependencies(IServiceCollection services)
            {
                services.AddSingleton(_repository);
            }
            protected override void ConfigureMvc(IMvcBuilder mvc)
            {
                var assembly = typeof(AchievementSummaryViewComponentController).GetTypeInfo().Assembly;
                mvc.AddApplicationPart(assembly);
            }
        }

        public TestWebServerFixture()
        {
            MockAchievementRepository = new MockAchievementRepository();
            var configBuilder = new ConfigurationBuilder()
                .AddInMemoryCollection();
            _startup = new TestStartup(configBuilder.Build(), MockAchievementRepository);
        }

        private readonly TestStartup _startup;

        internal MockAchievementRepository MockAchievementRepository { get; set; }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services => { services.AddSingleton<IStartup>(_startup); });
        }
    }
}
