using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FCVStockTool.Startup))]
namespace FCVStockTool
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
