using Microsoft.Extensions.DependencyInjection;
using MMR.Common.Interfaces;

namespace MMR.Enemizer
{
    public static class Module
    {
        public static void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton<IEnemies, Enemies>();
        }
    }
}
