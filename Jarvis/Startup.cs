using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Jarvis.Startup))]
namespace Jarvis
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
