using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HutechDriver.Startup))]
namespace HutechDriver
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
