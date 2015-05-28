using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FE.Startup))]
namespace FE
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
