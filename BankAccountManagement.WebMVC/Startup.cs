using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BankAccountManagement.WebMVC.Startup))]
namespace BankAccountManagement.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
