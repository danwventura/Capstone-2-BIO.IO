using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Bio.io.Startup))]
namespace Bio.io
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
