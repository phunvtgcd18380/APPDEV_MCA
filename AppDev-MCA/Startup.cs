using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AppDev_MCA.Startup))]
namespace AppDev_MCA
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
