using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SolveOrDrinkIt.Startup))]
namespace SolveOrDrinkIt
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
