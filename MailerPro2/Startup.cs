using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MailerPro2.Startup))]
namespace MailerPro2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
