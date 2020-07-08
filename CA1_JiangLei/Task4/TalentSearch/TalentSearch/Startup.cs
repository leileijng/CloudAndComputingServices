using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TalentSearch.Startup))]
namespace TalentSearch
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
