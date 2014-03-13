using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Icm.MediaLibrary.Web.Startup))]

namespace Icm.MediaLibrary.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
