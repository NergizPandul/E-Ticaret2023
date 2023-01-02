using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(E_Ticaret2023.Startup))]
namespace E_Ticaret2023
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
