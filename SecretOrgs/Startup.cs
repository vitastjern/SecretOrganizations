using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SecretOrgs.Startup))]
namespace SecretOrgs
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
