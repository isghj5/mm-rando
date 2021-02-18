using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using MMR.DiscordBot.Data;
using Microsoft.Extensions.DependencyInjection;
using MMR.DiscordBot.Services;
using Discord.Commands;
using MMR.DiscordBot.Data.Repositories;

namespace MMR.DiscordBot
{
    class Program
    {
        private const string MMR_DISCORDBOT_TOKEN = "MMR_DiscordBot_Token";

        private readonly string _discordBotToken;

        static int Main(string[] args)
        {
            return new Program().MainAsync().GetAwaiter().GetResult();
        }

        public Program()
        {
            _discordBotToken = Environment.GetEnvironmentVariable(MMR_DISCORDBOT_TOKEN);
        }

        public async Task<int> MainAsync()
        {
            if (string.IsNullOrWhiteSpace(_discordBotToken))
            {
                Console.Error.WriteLine($"Environment Variable '{MMR_DISCORDBOT_TOKEN}' is missing.");
                return -1;
            }

            using (var services = ConfigureServices())
            {
                var client = services.GetRequiredService<DiscordSocketClient>();

                client.Log += LogAsync;
                services.GetRequiredService<CommandService>().Log += LogAsync;

                // Tokens should be considered secret data and never hard-coded.
                // We can read from the environment variable to avoid hardcoding.
                await client.LoginAsync(TokenType.Bot, _discordBotToken);
                await client.StartAsync();

                // Here we initialize the logic required to register our commands.
                await services.GetRequiredService<CommandHandlingService>().InitializeAsync();

                await Task.Delay(Timeout.Infinite);
            }

            // Block the program until it is closed.
            await Task.Delay(-1);
            return 0;
        }

        private Task LogAsync(LogMessage log)
        {
            Console.WriteLine(log.ToString());
            return Task.CompletedTask;
        }

        private ServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton<MMRService>()
                .AddSingleton<DiscordSocketClient>()
                .AddSingleton<CommandService>()
                .AddSingleton<CommandHandlingService>()
                .AddSingleton<HttpClient>()
                .AddSingleton<UserSeedRepository>()
                .AddSingleton<GuildModRepository>()
                .AddSingleton<ConnectionFactory>()
                .BuildServiceProvider();
        }
    }
}
