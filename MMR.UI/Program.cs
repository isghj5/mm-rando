using System;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using MMR.UI.Forms;

namespace MMR.UI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var services = new ServiceCollection();

            ConfigureServices(services);

            using (var serviceProvider = services.BuildServiceProvider())
            {
                var mainForm = serviceProvider.GetRequiredService<MainForm>();
                Application.Run(mainForm);
            }
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            Enemizer.Module.ConfigureServices(services);
            Randomizer.Module.ConfigureServices(services);
            services.AddScoped<MainForm>();
        }
    }
}
