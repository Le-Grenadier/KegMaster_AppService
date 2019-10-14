using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(KegMasterAppService.Startup))]

namespace KegMasterAppService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}