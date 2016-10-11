using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(webUI.Startup))]
namespace webUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
