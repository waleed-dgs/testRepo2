using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DPNadminPanel.Startup))]
namespace DPNadminPanel
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
