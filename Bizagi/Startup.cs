using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Bizagi.Startup))]
namespace Bizagi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
