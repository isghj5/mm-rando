using MMR.Randomizer.Constants;
using MMR.Randomizer.Models;
using MMR.Randomizer.Models.Settings;
using MMR.Randomizer.Patch;
using MMR.Randomizer.Utils;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace MMR.Randomizer
{
    public static class ConfigurationProcessor
    {
        public static string Process(Configuration configuration, int seed, IProgressReporter progressReporter)
        {
            var randomizer = new Randomizer(configuration.GameplaySettings, seed);
            RandomizedResult randomized = null;
            if (string.IsNullOrWhiteSpace(configuration.OutputSettings.InputPatchFilename))
            {
                try
                {
                    randomized = randomizer.Randomize(progressReporter);

                    if ((configuration.OutputSettings.GenerateSpoilerLog || configuration.OutputSettings.GenerateHTMLLog || configuration.OutputSettings.GenerateSpoilerLogJson || configuration.OutputSettings.GenerateSettingsJson)
                        && configuration.GameplaySettings.LogicMode != LogicMode.Vanilla)
                    {
                        SpoilerUtils.CreateSpoilerLog(randomized, configuration.GameplaySettings, configuration.OutputSettings);
                    }
                }
                catch (RandomizationException ex)
                {
                    string nl = Environment.NewLine;
                    return $"Error randomizing logic: {ex.Message}{nl}{nl}Please try a different seed";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }

            if (configuration.OutputSettings.GenerateROM || configuration.OutputSettings.OutputVC || configuration.OutputSettings.GeneratePatch || configuration.OutputSettings.GenerateCosmeticsPatch)
            {
                if (!RomUtils.ValidateROM(configuration.OutputSettings.InputROMFilename))
                {
                    return "Cannot verify input ROM is Majora's Mask (U).";
                }
                if (configuration.OutputSettings.OutputVC && !Directory.Exists(Values.VCDirectory))
                {
                    return "Error: vc folder is missing and WiiVC wad creation was selected.\n\n"
                        + "If you did not extract the whole randomizer, you must extract the vc folder. If this is a beta release, copy the vc folder from the main release.";
                }

                var startTime = DateTime.Now;
                var builder = new Builder(randomized, configuration.CosmeticSettings);

                try
                {
                    builder.MakeROM(configuration.OutputSettings, progressReporter);
                }
                catch (ROMOverflowException ex)
                {
                    return $"Error: {ex.Message}";
                }
                catch (PatchMagicException)
                {
                    return $"Error applying patch: Not a valid patch file";
                }
                catch (IOException ex)
                {
                    return ex.Message;
                }
                catch (Exception ex)
                {
                    string nl = Environment.NewLine;
                    return $"Error building ROM: {ex.Message}{nl}{nl}Please contact the development team and provide them more information";
                }

                Debug.WriteLine($" seed build time : [{((DateTime.Now).Subtract(startTime).TotalMilliseconds).ToString()} (ms)]");
            }

            //settings.InputPatchFilename = null;

            return null;
            //return "Generation complete!";
        }
    }

    public interface IProgressReporter
    {
        void ReportProgress(int percentProgress, string message, CancellationTokenSource ctsItemImportance = null);
    }
}
