#r "nuget: Nake.Meta, 3.0.0-beta-02"
#r "nuget: Nake.Utility, 3.0.0-beta-02"

using Nake;
using System;

const string DefaultConfig = "Release";

var ScriptDirectory = "%NakeScriptDirectory%";
var RandoCliPath = $"{ScriptDirectory}/../MMR.CLI";
var RandoUiPath = $"{ScriptDirectory}/../MMR.UI";
var RomPath = $"{ScriptDirectory}/input/Rom.z64";

/// Build Asm blob using docker-compose.
[Nake] async Task RunDockerCompose() => await "docker-compose up --abort-on-container-exit";

/// Build MMR.CLI.
[Nake] async Task BuildRandoCli(string config, bool incremental = false)
{
    var projectPath = Path.GetFullPath($"{RandoCliPath}/MMR.CLI.csproj");
    await $"dotnet build '{projectPath}' -c {config} {(incremental ? "" : "--no-incremental")}";
}

/// Run MMR.CLI to generate a patched ROM.
[Nake] async Task RunRandoCli(string config, bool useUiConfig = true)
{
    var cliDllPath = Path.GetFullPath($"{RandoCliPath}/bin/{config}/netcoreapp3.1/MMR.CLI.dll");
    var settingsPath = Path.GetFullPath($"{RandoUiPath}/bin/{config}/settings.json");
    await $"dotnet '{cliDllPath}' -input '{RomPath}' {(useUiConfig ? $"-settings '{settingsPath}'" : "")}";
}

/// Build Asm blob and MMR.CLI, and run MMR.CLI to generate a patched ROM.
[Nake] async Task BuildAllAndRun(string config)
{
    WriteHeaderText("Building Asm blob...");
    await RunDockerCompose();

    WriteHeaderText("Building MMR.CLI...");
    await BuildRandoCli(config);

    WriteHeaderText("Running MMR.CLI to generate randomized ROM...");
    await RunRandoCli(config);

    WriteHeaderText("Completed all tasks!", false);
}

/// See: BuildAllAndRun.
[Nake] async Task Default() => await BuildAllAndRun(DefaultConfig);

/// Write header message to console.
void WriteHeaderText(string message, bool finalNewline = true) =>
    Console.WriteLine($"\n:::\n::: {message}\n:::{(finalNewline ? "\n" : "")}");
