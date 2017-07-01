using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DSW_Beta_V2.Startup))]
namespace DSW_Beta_V2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
