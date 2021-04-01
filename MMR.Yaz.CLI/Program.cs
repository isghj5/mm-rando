using MzxYaz = MMR.Randomizer.Utils.Mzxrules.Yaz;
using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace MMR.Yaz.CLI
{
    public static class Program
    {
        static Command WithHandler(this Command command, string name)
        {
            var flags = BindingFlags.NonPublic | BindingFlags.Static;
            var method = typeof(Program).GetMethod(name, flags);

            var handler = CommandHandler.Create(method!);
            command.Handler = handler;
            return command;
        }

        public static async Task<int> Main(params string[] args)
        {
            var cmd = new RootCommand
            {
                new Command("decode")
                {
                    new Argument<string>("input", "Input file path."),
                    new Argument<string>("output", "Output file path."),
                    new Option("--legacy", "Use legacy implementation."),
                }.WithHandler(nameof(HandleDecode)),
                new Command("encode")
                {
                    new Argument<string>("input", "Input file path."),
                    new Argument<string>("output", "Output file path."),
                    new Option("--legacy", "Use legacy implementation."),
                }.WithHandler(nameof(HandleEncode)),
            };
            cmd.Description = "Command-line interface for MMR.Yaz decoder & encoder.";
            return await cmd.InvokeAsync(args);
        }

        static void HandleDecode(string input, string output, bool legacy, IConsole console)
        {
            var inputBytes = File.ReadAllBytes(input);
            byte[] outputBytes = null;

            // Perform decode.
            if (legacy)
            {
                // Legacy decode.
                using (var memoryStream = new MemoryStream(inputBytes))
                {
                    outputBytes = MzxYaz.Decode(memoryStream, inputBytes.Length);
                }
            }
            else
            {
                // New decode.
                outputBytes = Yaz.Decode(inputBytes);
            }

            File.WriteAllBytes(output, outputBytes);
        }

        static void HandleEncode(string input, string output, bool legacy, IConsole console)
        {
            var inputBytes = File.ReadAllBytes(input);
            byte[] outputBytes = null;

            if (legacy)
            {
                // Legacy encode.
                var amount = MzxYaz.Encode(inputBytes, inputBytes.Length, out var result);
                if (amount >= 0)
                {
                    outputBytes = new byte[amount];
                    Buffer.BlockCopy(result, 0, outputBytes, 0, outputBytes.Length);
                }
            }
            else
            {
                // New encode.
                var amount = Yaz.Encode(inputBytes, out var compressed);
                outputBytes = new byte[amount];
                compressed.AsSpan(0, amount).CopyTo(outputBytes);
            }

            File.WriteAllBytes(output, outputBytes);
        }
    }
}
