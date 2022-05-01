using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TurboPac.Startup))]
namespace TurboPac
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
