using Microsoft.Extensions.DependencyInjection;
using MMR.Common.Interfaces;
using MMR.Randomizer.Utils;

namespace MMR.Randomizer
{
    public static class Module
    {
        public static void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton<IObjectUtils, ObjUtils>();
            services.AddSingleton<ISceneUtils, SceneUtils>();
            services.AddScoped<Builder>();
            services.AddTransient<ConfigurationProcessor>();
            services.AddScoped<ConfigurationStore>();
            services.AddScoped<RandomizedResultStore>();
        }
    }
}
