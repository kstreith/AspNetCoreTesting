using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Linq;
using System.Reflection;

namespace RazorViewComponentSample
{
    public class Startup : IStartup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var mvc = services.AddMvc();
            //var assembly = typeof(Startup).GetTypeInfo().Assembly;
            //mvc.AddApplicationPart(assembly);
            ConfigureMvc(mvc);
            //AddControllersAsServices2(mvc);
            mvc.SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            return services.BuildServiceProvider();
        }

        //public class ServiceBasedControllerActivator2 : IControllerActivator
        //{
        //    public object Create(ControllerContext actionContext)
        //    {
        //        var controllerType = actionContext.ActionDescriptor.ControllerTypeInfo.AsType();

        //        return actionContext.HttpContext.RequestServices.GetRequiredService(controllerType);
        //    }

        //    public virtual void Release(ControllerContext context, object controller)
        //    {
        //    }
        //}
        //public static IMvcBuilder AddControllersAsServices2(IMvcBuilder builder)
        //{
        //    var feature = new ControllerFeature();
        //    builder.PartManager.PopulateFeature(feature);

        //    foreach (var controller in feature.Controllers.Select(c => c.AsType()))
        //    {
        //        builder.Services.TryAddTransient(controller, controller);
        //    }

        //    builder.Services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator2>());

        //    return builder;
        //}

        public virtual void ConfigureMvc(IMvcBuilder mvc)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            var env = app.ApplicationServices.GetService<IHostingEnvironment>();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
