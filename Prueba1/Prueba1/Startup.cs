using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Prueba1.Startup))]
namespace Prueba1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
