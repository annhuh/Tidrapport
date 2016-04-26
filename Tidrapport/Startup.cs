using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Tidrapport.Startup))]
namespace Tidrapport
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
