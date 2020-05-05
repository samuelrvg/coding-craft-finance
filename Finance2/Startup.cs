using Finance;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Finance2.Startup))]
namespace Finance2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            new CommonStartup().ConfigureAuth(app);
        }
    }
}