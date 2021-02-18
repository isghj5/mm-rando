using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MMR.Common.Utils;

namespace MMR.DiscordBot.Services
{
    public class MMRService
    {
        private const string MMR_CLI = "MMR_CLI";
        private readonly string _cliPath;
        private readonly HttpClient _httpClient;
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        private readonly Random _random = new Random();

        public MMRService()
        {
            _cliPath = Environment.GetEnvironmentVariable(MMR_CLI);
            if (string.IsNullOrWhiteSpace(_cliPath))
            {
                throw new Exception($"Environment Variable '{MMR_CLI}' is missing.");
            }
            if (!Directory.Exists(_cliPath))
            {
                throw new Exception($"'{_cliPath}' is not a valid MMR.CLI path.");
            }

            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(10);
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "zoey.zolotova at gmail.com");
        }

        public string GetSpoilerLogPath(DateTime seedDate)
        {
            var requestedLog = FileUtils.MakeFilenameValid(seedDate.ToString("o"));
            return Path.Combine(_cliPath, "output", $"{requestedLog}_SpoilerLog.txt");
        }

        public string GetSettingsPath(ulong guildId, string settingName)
        {
            var settingsRoot = Path.Combine(_cliPath, "settings");
            if (!Directory.Exists(settingsRoot))
            {
                Directory.CreateDirectory(settingsRoot);
            }
            var guildRoot = Path.Combine(settingsRoot, $"{guildId}");
            if (!Directory.Exists(guildRoot))
            {
                Directory.CreateDirectory(guildRoot);
            }
            return Path.Combine(guildRoot, $"{FileUtils.MakeFilenameValid(settingName)}.json");
        }

        public IEnumerable<string> GetSettingsPaths(ulong guildId)
        {
            var settingsRoot = Path.Combine(_cliPath, "settings");
            if (!Directory.Exists(settingsRoot))
            {
                Directory.CreateDirectory(settingsRoot);
            }
            var guildRoot = Path.Combine(settingsRoot, $"{guildId}");
            if (!Directory.Exists(guildRoot))
            {
                Directory.CreateDirectory(guildRoot);
            }

            return Directory.EnumerateFiles(guildRoot);
        }

        public async Task<(string patchPath, string hashIconPath, string spoilerLogPath)> GenerateSeed(DateTime now, string settingsPath)
        {
            await Task.Delay(1);
            var filename = FileUtils.MakeFilenameValid(now.ToString("o"));
            var attempts = 1; // TODO increase number of attempts and alter seed each attempt
            while (attempts > 0)
            {
                try
                {
                    var success = await GenerateSeed(filename, settingsPath);
                    if (success)
                    {
                        var patchPath = Path.Combine(_cliPath, "output", $"{filename}.mmr");
                        var hashIconPath = Path.ChangeExtension(patchPath, "png");
                        var spoilerLogPath = GetSpoilerLogPath(now);
                        if (File.Exists(patchPath) && File.Exists(hashIconPath))
                        {
                            return (patchPath, hashIconPath, spoilerLogPath);
                        }
                        else
                        {
                            success = false;
                        }
                    }
                }
                catch
                {
                    if (attempts == 1)
                    {
                        throw;
                    }
                }
                attempts--;
            }
            throw new Exception("Failed to generate seed after 5 attempts.");
        }

        private async Task<bool> GenerateSeed(string filename, string settingsPath)
        {
            var output = Path.Combine("output", filename);
            var seed = await GetSeed();
            var processInfo = new ProcessStartInfo("dotnet");
            processInfo.WorkingDirectory = _cliPath;
            processInfo.Arguments = $"{Path.Combine(_cliPath, @"MMR.CLI.dll")} -output \"{output}.z64\" -seed {seed} -spoiler -patch";
            if (!string.IsNullOrWhiteSpace(settingsPath))
            {
                processInfo.Arguments += $" -settings \"{settingsPath}\"";
            }
            processInfo.ErrorDialog = false;
            processInfo.UseShellExecute = false;
            processInfo.RedirectStandardOutput = true;
            processInfo.RedirectStandardError = true;

            var proc = Process.Start(processInfo);
            proc.ErrorDataReceived += (sender, errorLine) => { if (errorLine.Data != null) Trace.WriteLine(errorLine.Data); };
            proc.OutputDataReceived += (sender, outputLine) => { if (outputLine.Data != null) Trace.WriteLine(outputLine.Data); };
            proc.BeginErrorReadLine();
            proc.BeginOutputReadLine();

            proc.WaitForExit();
            return proc.ExitCode == 0;
        }

        private async Task<int> GetSeed()
        {
            await _semaphore.WaitAsync();
            int seed;
            try
            {
                var response = await _httpClient.GetStringAsync("https://www.random.org/integers/?num=1&min=-1000000000&max=1000000000&col=1&base=10&format=plain&rnd=new");
                seed = int.Parse(response) + 1000000000;
            }
            catch (HttpRequestException e)
            {
                seed = _random.Next();
            }
            finally
            {
                _semaphore.Release();
            }
            return seed;
        }
    }
}
