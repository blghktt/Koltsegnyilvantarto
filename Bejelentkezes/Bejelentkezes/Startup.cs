using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Bejelentkezes.Startup))]
namespace Bejelentkezes
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
