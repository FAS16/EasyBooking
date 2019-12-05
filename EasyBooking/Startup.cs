using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EasyBooking.Startup))]
namespace EasyBooking
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
