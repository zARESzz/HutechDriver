using HutechDriver.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Owin;
using Owin;
using System.Threading;


[assembly: OwinStartupAttribute(typeof(HutechDriver.Startup))]
namespace HutechDriver
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<TripCleanup>();
            services.AddHostedService<TripCleanupServices>();
            services.AddSignalR();
        }
    }
}
