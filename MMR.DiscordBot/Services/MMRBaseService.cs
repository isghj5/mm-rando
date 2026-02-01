using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using MMR.Common.Utils;

namespace MMR.DiscordBot.Services
{
    public abstract class MMRBaseService
    {
        private const string MMR_CLI = "MMR_CLI";
        private const string MYSTERY_MAKER = "MYSTERY_MAKER";
        protected string _cliPath;
        protected string _mysteryMakerPath;
        private readonly HttpClient _httpClient;
        private static readonly ThreadQueue _threadQueue = new ThreadQueue();
        private CancellationTokenSource _cancelTokenSource;
        private readonly Random _random = new Random();

        protected abstract string Version { get; }

        private string GetEnvironmentVariableDirectory(string variable)
        {
            var result = Environment.GetEnvironmentVariable(variable);
            if (string.IsNullOrWhiteSpace(result))
            {
                Console.WriteLine($"Warning: Environment Variable '{variable}' is missing.");
            }
            else
            {
                result = Path.Combine(result, Version);
                if (!Directory.Exists(result))
                {
                    Console.WriteLine($"Warning: Directory '{result}' does not exist.");
                }
                else
                {
                    Console.WriteLine($"{variable} path = {result}");
                }
            }
            return result;
        }

        public MMRBaseService()
        {
            _cliPath = GetEnvironmentVariableDirectory(MMR_CLI);
            _mysteryMakerPath = GetEnvironmentVariableDirectory(MYSTERY_MAKER);

            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(120);
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "zoey.zolotova at gmail.com");
        }

        public bool IsReady()
        {
            return !string.IsNullOrWhiteSpace(_cliPath)
                && Directory.Exists(_cliPath)
                && !string.IsNullOrWhiteSpace(_mysteryMakerPath)
                && Directory.Exists(_mysteryMakerPath);
        }

        public (string filename, string patchPath, string hashIconPath, string spoilerLogPath, string mysterySpoilerPath, string version) GetSeedPaths(DateTime seedDate, string version)
        {
            if (string.IsNullOrWhiteSpace(version))
            {
                var randomizerDllPath = Path.Combine(_cliPath, "MMR.Randomizer.dll");
                version = AssemblyName.GetAssemblyName(randomizerDllPath).Version.ToString();
            }
            var filename = FileUtils.MakeFilenameValid($"MMR-{version}-{seedDate:o}");
            var patchPath = Path.Combine(_cliPath, "output", $"{filename}.mmr");
            var hashIconPath = Path.Combine(_cliPath, "output", $"{filename}.png");
            var spoilerLogPath = Path.Combine(_cliPath, "output", $"{filename}_SpoilerLog.txt");
            var mysterySpoilerPath = Path.Combine(_cliPath, "output", $"{filename}_MysterySpoiler.txt");
            return (filename, patchPath, hashIconPath, spoilerLogPath, mysterySpoilerPath, version);
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

        public string GetMysteryRoot(ulong guildId)
        {
            var mysteryRoot = Path.Combine(_cliPath, "mystery-settings");
            if (!Directory.Exists(mysteryRoot))
            {
                Directory.CreateDirectory(mysteryRoot);
            }
            var guildRoot = Path.Combine(mysteryRoot, $"{guildId}");
            if (!Directory.Exists(guildRoot))
            {
                Directory.CreateDirectory(guildRoot);
            }
            return guildRoot;
        }

        public string GetMysteryPath(ulong guildId, string categoryName, bool createIfNotExists)
        {
            var guildRoot = GetMysteryRoot(guildId);
            var categoryRoot = Path.Combine(guildRoot, FileUtils.MakeFilenameValid(categoryName));
            if (createIfNotExists && !Directory.Exists(categoryRoot))
            {
                Directory.CreateDirectory(categoryRoot);
            }
            return categoryRoot;
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

        public string GetDefaultSettingsPath()
        {
            var settingsRoot = Path.Combine(_cliPath, "settings");
            if (!Directory.Exists(settingsRoot))
            {
                Directory.CreateDirectory(settingsRoot);
            }
            return Path.Combine(settingsRoot, "default.json");
        }

        public string GetVersion()
        {
            var randomizerDllPath = Path.Combine(_cliPath, "MMR.Randomizer.dll");
            return AssemblyName.GetAssemblyName(randomizerDllPath).Version.ToString();
        }

        public void Kill()
        {
            if (_cancelTokenSource != null)
            {
                _cancelTokenSource.Cancel();
            }
        }

        public async Task<(string patchPath, string hashIconPath, string spoilerLogPath, string version)> GenerateSeed(DateTime now, string settingsPath, Func<int, Task> notifyOfPosition)
        {
            await _threadQueue.WaitAsync(notifyOfPosition);
            //await Task.Delay(1);
            _cancelTokenSource = new CancellationTokenSource();
            await notifyOfPosition(-1);
            try
            {
                var (filename, patchPath, hashIconPath, spoilerLogPath, mysterySpoilerPath, version) = GetSeedPaths(now, null);
                var attempts = 1; // TODO increase number of attempts and alter seed each attempt
                while (attempts > 0)
                {
                    try
                    {
                        var settingFileName = Path.GetFileNameWithoutExtension(settingsPath);
                        bool success;
                        if (settingFileName.StartsWith("Mystery_"))
                        {
                            success = await GenerateMystery(filename, settingsPath, mysterySpoilerPath, _cancelTokenSource.Token);
                        }
                        else
                        {
                            success = await GenerateSeed(filename, settingsPath, _cancelTokenSource.Token);
                        }
                        if (success)
                        {
                            if (File.Exists(patchPath) && File.Exists(hashIconPath))
                            {
                                return (patchPath, hashIconPath, spoilerLogPath, version);
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
                if (_cancelTokenSource.IsCancellationRequested)
                {
                    throw new Exception("Killed by admin.");
                }
                throw new Exception("Failed to generate seed.");
            }
            finally
            {
                _cancelTokenSource.Dispose();
                _cancelTokenSource = null;
                await _threadQueue.ReleaseAsync();
            }
        }

        private async Task<bool> GenerateSeed(string filename, string settingsPath, CancellationToken cancellationToken)
        {
            var cliDllPath = Path.Combine(_cliPath, "MMR.CLI.dll");
            var output = Path.Combine("output", filename);
            var seed = await GetSeed();
            var processInfo = new ProcessStartInfo("dotnet");
            processInfo.WorkingDirectory = _cliPath;
            processInfo.Arguments = $"{cliDllPath} -output \"{output}.z64\" -seed {seed} -spoiler -outputpatch";
            if (!string.IsNullOrWhiteSpace(settingsPath))
            {
                processInfo.Arguments += $" -settings \"{settingsPath}\"";
            }
            else if (File.Exists(GetDefaultSettingsPath()))
            {
                processInfo.Arguments += $" -settings \"{GetDefaultSettingsPath()}\"";
            }
            processInfo.ErrorDialog = false;
            processInfo.UseShellExecute = false;
            processInfo.RedirectStandardOutput = true;
            processInfo.RedirectStandardError = true;
            processInfo.CreateNoWindow = true;

            var proc = Process.Start(processInfo);
            proc.ErrorDataReceived += (sender, errorLine) => { if (errorLine.Data != null) Trace.WriteLine(errorLine.Data); };
            proc.OutputDataReceived += (sender, outputLine) => { if (outputLine.Data != null) Trace.WriteLine(outputLine.Data); };
            proc.BeginErrorReadLine();
            proc.BeginOutputReadLine();

            while (!proc.WaitForExit(1000))
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    proc.Kill();
                }
            }

            return proc.ExitCode == 0;
        }

        private async Task<bool> GenerateMystery(string filename, string mysteryPath, string mysterySpoilerPath, CancellationToken cancellationToken)
        {
            var processFilename = Path.Combine(_mysteryMakerPath, "MysteryMaker.exe");
            var processInfo = new ProcessStartInfo(processFilename, $"-i {mysteryPath} --settings-only");

            processInfo.WorkingDirectory = _mysteryMakerPath;
            processInfo.ErrorDialog = false;
            processInfo.UseShellExecute = false;
            processInfo.RedirectStandardOutput = true;
            processInfo.RedirectStandardError = true;
            processInfo.CreateNoWindow = true;

            var proc = Process.Start(processInfo);
            proc.ErrorDataReceived += (sender, errorLine) => { if (errorLine.Data != null) Trace.WriteLine(errorLine.Data); };
            proc.OutputDataReceived += (sender, outputLine) => { if (outputLine.Data != null) Trace.WriteLine(outputLine.Data); };
            proc.BeginErrorReadLine();
            proc.BeginOutputReadLine();

            while (!proc.WaitForExit(1000))
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    proc.Kill();
                }
            }

            if (proc.ExitCode != 0)
            {
                return false;
            }

            var expectedMysteryFilename = Path.GetFileNameWithoutExtension(mysteryPath);
            if (expectedMysteryFilename.EndsWith("base"))
            {
                expectedMysteryFilename = expectedMysteryFilename.Remove(expectedMysteryFilename.Length - 4);
            }
            var expectedMysterySpoilerFilename = expectedMysteryFilename + "1_MysterySpoiler.txt";
            expectedMysteryFilename += "1.json";

            var settingFile = Path.Combine(_mysteryMakerPath, "output", expectedMysteryFilename);
            var mysterySpoiler = Path.Combine(_mysteryMakerPath, "output", expectedMysterySpoilerFilename);

            if (!File.Exists(settingFile))
            {
                return false;
            }

            var result = await GenerateSeed(filename, settingFile, cancellationToken);

            File.Delete(settingFile);
            File.Move(mysterySpoiler, mysterySpoilerPath);

            return result;
        }

        private async Task<int> GetSeed()
        {
            int seed;
            try
            {
                var response = await _httpClient.GetStringAsync("https://www.random.org/integers/?num=31&min=0&max=1&col=31&base=2&format=plain&rnd=new");
                seed = Convert.ToInt32(response.Replace("\t", "").Trim(), 2);
            }
            catch (Exception e) when (e is HttpRequestException || e is TaskCanceledException)
            {
                seed = _random.Next();
            }
            return seed;
        }
    }

    public class ThreadQueue
    {
        private List<SemaphoreSlim> semaphores = new List<SemaphoreSlim>();
        SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        public async Task WaitAsync(Func<int, Task> notifyOfPosition)
        {
            await _semaphore.WaitAsync();
            for (var i = semaphores.Count - 1; i >= 0; i--)
            {
                if (semaphores[i].CurrentCount > 0)
                {
                    semaphores.RemoveAt(i);
                }
                else
                {
                    await notifyOfPosition(i);
                    if (i == semaphores.Count - 1)
                    {
                        var newSemaphore = new SemaphoreSlim(0, 1);
                        semaphores.Add(newSemaphore);
                    }
                    var mySemaphore = semaphores[i + 1];
                    _semaphore.Release();
                    await semaphores[i].WaitAsync();
                    await _semaphore.WaitAsync();
                    mySemaphore.Release();
                }
            }
            if (semaphores.Count == 0)
            {
                semaphores.Add(new SemaphoreSlim(0, 1));
            }
            _semaphore.Release();
        }

        public async Task ReleaseAsync()
        {
            await _semaphore.WaitAsync();
            semaphores[0].Release();
            _semaphore.Release();
        }
    }
}
