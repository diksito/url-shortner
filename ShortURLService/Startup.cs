using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ShortURLService.Startup))]
namespace ShortURLService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
