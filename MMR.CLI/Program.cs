﻿using MMR.Randomizer.Models.Settings;
using MMR.Randomizer.Models;
using MMR.Randomizer;
using MMR.Randomizer.GameObjects;
using System.Collections.Generic;
using System;
using MMR.Randomizer.Utils;
using System.Linq;
using MMR.Randomizer.Extensions;
using MMR.Common.Utils;
using System.IO;
using System.Linq.Expressions;
using MMR.Randomizer.Models.Colors;
using MMR.Randomizer.Constants;

namespace MMR.CLI
{
    partial class Program
    {
        static int Main(string[] args)
        {
            Dictionary<string, List<string>> argsDictionary;
            if (args.Length == 1 && File.Exists(args[0]) && Path.GetExtension(args[0]) == ".mmr")
            {
                argsDictionary = new Dictionary<string, List<string>>
                {
                    { "-inputpatch", args.ToList() }
                };
            }
            else
            {
                argsDictionary = DictionaryHelper.FromProgramArguments(args);
            }
            if (argsDictionary.ContainsKey("-help"))
            {
                Console.WriteLine("All arguments are optional.");
                var helpTexts = new Dictionary<string, string>
                {
                    { "-help", "See this help text." },
                    { "-settings <path>", "Path to a settings JSON file. Only the GameplaySettings will be loaded. Other settings will be loaded from your default settings.json file." },
                    { "-outputpatch", "Output a .mmr patch file." },
                    { "-inputpatch <path>", "Path to a .mmr patch file to apply." },
                    { "-spoiler", "Output a .txt spoiler log." },
                    { "-html", "Output a .html item tracker." },
                    { "-rom", "Output a .z64 ROM file." },
                    { "-seed", "Set the seed for the randomizer." },
                    { "-output <path>", "Path to output the output ROM. Other outputs will be based on the filename in this path. If omitted, will output to \"output/{timestamp}.z64\"" },
                    { "-input <path>", "Path to the input Majora's Mask (U) ROM. If omitted, will try to use \"input.z64\"." },
                    { "-save", "Save the settings to the default settings.json file." },
                    { "-maxImportanceWait", "Set the maximum amount of time (in seconds) that the randomizer should wait for item importance verification before skipping it." },
                };
                foreach (var kvp in helpTexts)
                {
                    Console.WriteLine("{0, -17} {1}", kvp.Key, kvp.Value);
                }
                Console.WriteLine("settings.json details:");
                Console.WriteLine(GetSettingPath(cfg => cfg.GameplaySettings) + ":");
                Console.WriteLine(GetEnumSettingDescription(cfg => cfg.GameplaySettings.LogicMode));
                Console.WriteLine(GetArrayValueDescription(nameof(GameplaySettings.ItemCategoriesRandomized), Enum.GetValues<ItemCategory>().Where(c => c > 0).Select(c => c.ToString())));
                Console.WriteLine(GetArrayValueDescription(nameof(GameplaySettings.LocationCategoriesRandomized), Enum.GetValues<LocationCategory>().Where(c => c > 0).Select(c => c.ToString())));
                Console.WriteLine(GetArrayValueDescription(nameof(GameplaySettings.ClassicCategoriesRandomized), Enum.GetValues<ClassicCategory>().Where(c => c > 0).Select(c => c.ToString())));
                Console.WriteLine(GetEnumSettingDescription(cfg => cfg.GameplaySettings.DamageMode));
                Console.WriteLine(GetEnumSettingDescription(cfg => cfg.GameplaySettings.DamageEffect));
                Console.WriteLine(GetEnumSettingDescription(cfg => cfg.GameplaySettings.MovementMode));
                Console.WriteLine(GetEnumSettingDescription(cfg => cfg.GameplaySettings.FloorType));
                Console.WriteLine(GetEnumSettingDescription(cfg => cfg.GameplaySettings.ClockSpeed));
                Console.WriteLine(GetEnumSettingDescription(cfg => cfg.GameplaySettings.BlastMaskCooldown));
                Console.WriteLine(GetEnumSettingDescription(cfg => cfg.GameplaySettings.GossipHintStyle));
                Console.WriteLine(GetEnumSettingDescription(cfg => cfg.GameplaySettings.SmallKeyMode));
                Console.WriteLine(GetEnumSettingDescription(cfg => cfg.GameplaySettings.BossKeyMode));
                Console.WriteLine(GetEnumSettingDescription(cfg => cfg.GameplaySettings.StrayFairyMode));
                Console.WriteLine(GetEnumSettingDescription(cfg => cfg.GameplaySettings.PriceMode));
                Console.WriteLine(GetEnumSettingDescription(cfg => cfg.GameplaySettings.DungeonNavigationMode));
                Console.WriteLine(GetSettingDescription(nameof(GameplaySettings.EnabledTricks), "Array of trick IDs."));
                Console.WriteLine(GetSettingPath(cfg => cfg.GameplaySettings.ShortenCutsceneSettings) + ":");
                Console.WriteLine(GetEnumSettingDescription(cfg => cfg.GameplaySettings.ShortenCutsceneSettings.General));
                Console.WriteLine(GetEnumSettingDescription(cfg => cfg.GameplaySettings.ShortenCutsceneSettings.BossIntros));
                Console.WriteLine(GetSettingPath(cfg => cfg.CosmeticSettings) + ":");
                Console.WriteLine(GetEnumSettingDescription(cfg => cfg.CosmeticSettings.TatlColorSchema));
                Console.WriteLine(GetEnumSettingDescription(cfg => cfg.CosmeticSettings.Music));
                Console.WriteLine(GetEnumSettingDescription(cfg => cfg.CosmeticSettings.DisableCombatMusic));
                Console.WriteLine(GetArrayValueDescription(nameof(CosmeticSettings.Instruments), Enum.GetNames<Instrument>()));
                Console.WriteLine(GetArrayValueDescription(nameof(CosmeticSettings.HeartsSelection), ColorSelectionManager.Hearts.GetItems().Select(csi => csi.Name)));
                Console.WriteLine(GetArrayValueDescription(nameof(CosmeticSettings.MagicSelection), ColorSelectionManager.MagicMeter.GetItems().Select(csi => csi.Name)));
                Console.WriteLine(GetSettingPath(cfg => cfg.CosmeticSettings.DPad.Pad) + ":");
                Console.WriteLine(GetEnumArraySettingDescription(cfg => cfg.CosmeticSettings.DPad.Pad.Values) + " Array length of 4.");
                return 0;
            }
            var configuration = LoadSettings();
            if (configuration == null)
            {
                Console.WriteLine("Default settings file not found. Generating...");
                configuration = new Configuration
                {
                    CosmeticSettings = new CosmeticSettings(),
                    GameplaySettings = new GameplaySettings
                    {
                        ShortenCutsceneSettings = new ShortenCutsceneSettings(),
                    },
                    OutputSettings = new OutputSettings()
                    {
                        InputROMFilename = "input.z64",
                    },
                };
                SaveSettings(configuration);
                Console.WriteLine($"Generated {Path.ChangeExtension(DEFAULT_SETTINGS_FILENAME, SETTINGS_EXTENSION)}. Edit it to set your settings.");
            }
            var settingsPath = argsDictionary.GetValueOrDefault("-settings")?.FirstOrDefault();
            if (settingsPath != null)
            {
                var loadedConfiguration = LoadSettings(settingsPath);
                if (loadedConfiguration == null)
                {
                    Console.WriteLine($"File not found \"{settingsPath}\".");
                    return -1;
                }
                if (loadedConfiguration.GameplaySettings == null)
                {
                    Console.WriteLine($"Error loading GameplaySettings from \"{settingsPath}\".");
                    return -1;
                }
                configuration.GameplaySettings = loadedConfiguration.GameplaySettings;
                Console.WriteLine($"Loaded GameplaySettings from \"{settingsPath}\".");
            }

            if (configuration.GameplaySettings.ItemCategoriesRandomized != null || configuration.GameplaySettings.LocationCategoriesRandomized != null || configuration.GameplaySettings.ClassicCategoriesRandomized != null)
            {
                var items = new List<Item>();
                if (configuration.GameplaySettings.ItemCategoriesRandomized != null)
                {
                    items.AddRange(ItemUtils.ItemsByItemCategory().Where(kvp => configuration.GameplaySettings.ItemCategoriesRandomized.Contains(kvp.Key)).SelectMany(kvp => kvp.Value));
                    configuration.GameplaySettings.ItemCategoriesRandomized = null;
                }
                if (configuration.GameplaySettings.LocationCategoriesRandomized != null)
                {
                    items.AddRange(ItemUtils.ItemsByLocationCategory().Where(kvp => configuration.GameplaySettings.LocationCategoriesRandomized.Contains(kvp.Key)).SelectMany(kvp => kvp.Value));
                    configuration.GameplaySettings.LocationCategoriesRandomized = null;
                }
                if (configuration.GameplaySettings.ClassicCategoriesRandomized != null)
                {
                    items.AddRange(ItemUtils.ItemsByClassicCategory().Where(kvp => configuration.GameplaySettings.ClassicCategoriesRandomized.Contains(kvp.Key)).SelectMany(kvp => kvp.Value));
                    configuration.GameplaySettings.ClassicCategoriesRandomized = null;
                }
                configuration.GameplaySettings.CustomItemList.Clear();
                foreach (var item in items)
                {
                    configuration.GameplaySettings.CustomItemList.Add(item);
                }
            }
            else
            {
                configuration.GameplaySettings.CustomItemList = ConvertItemString(ItemUtils.AllLocations().ToList(), configuration.GameplaySettings.CustomItemListString).ToHashSet();
            }

            configuration.GameplaySettings.CustomStartingItemList = ConvertItemString(ItemUtils.StartingItems().Where(item => !item.Name().Contains("Heart")).ToList(), configuration.GameplaySettings.CustomStartingItemListString);
            configuration.GameplaySettings.CustomJunkLocations = ConvertItemString(ItemUtils.AllLocations().ToList(), configuration.GameplaySettings.CustomJunkLocationsString);

            configuration.OutputSettings.InputPatchFilename = argsDictionary.GetValueOrDefault("-inputpatch")?.SingleOrDefault();
            configuration.OutputSettings.GeneratePatch |= argsDictionary.ContainsKey("-outputpatch");
            configuration.OutputSettings.GenerateSpoilerLog |= argsDictionary.ContainsKey("-spoiler");
            configuration.OutputSettings.GenerateHTMLLog |= argsDictionary.ContainsKey("-html");
            configuration.OutputSettings.GenerateROM |= argsDictionary.ContainsKey("-rom");

            int seed;
            if (argsDictionary.ContainsKey("-seed"))
            {
                seed = int.Parse(argsDictionary["-seed"][0]);
            }
            else
            {
                seed = new Random().Next();
            }


            var outputArg = argsDictionary.GetValueOrDefault("-output");
            if (outputArg != null)
            {
                if (outputArg.Count > 1)
                {
                    throw new ArgumentException("Invalid argument.", "-output");
                }
                configuration.OutputSettings.OutputROMFilename = outputArg.SingleOrDefault();
            }
            else if (!string.IsNullOrWhiteSpace(configuration.OutputSettings.InputPatchFilename))
            {
                configuration.OutputSettings.OutputROMFilename = Path.ChangeExtension(configuration.OutputSettings.InputPatchFilename, "z64");
            }
            configuration.OutputSettings.OutputROMFilename ??= "output/";
            if (!Path.IsPathRooted(configuration.OutputSettings.OutputROMFilename))
            {
                configuration.OutputSettings.OutputROMFilename = Path.Combine(Directory.GetCurrentDirectory(), configuration.OutputSettings.OutputROMFilename);
            }
            var directory = Path.GetDirectoryName(configuration.OutputSettings.OutputROMFilename);
            var filename = Path.GetFileName(configuration.OutputSettings.OutputROMFilename);
            Directory.CreateDirectory(directory);
            if (string.IsNullOrWhiteSpace(filename))
            {
                filename = FileUtils.MakeFilenameValid($"MMR-{typeof(Randomizer.Randomizer).Assembly.GetName().Version}-{DateTime.UtcNow:o}") + ".z64";
            }
            else if (Path.GetExtension(filename) != ".z64")
            {
                filename = Path.ChangeExtension(filename, "z64");
            }
            configuration.OutputSettings.OutputROMFilename = Path.Combine(directory, filename);

            var inputArg = argsDictionary.GetValueOrDefault("-input");
            if (inputArg != null)
            {
                if (inputArg.Count > 1)
                {
                    throw new ArgumentException("Invalid argument.", "-input");
                }
                configuration.OutputSettings.InputROMFilename = inputArg.SingleOrDefault();
            }
            configuration.OutputSettings.InputROMFilename ??= "input.z64";

            if (argsDictionary.ContainsKey("-save"))
            {
                SaveSettings(configuration);
            }

            var validationResult = configuration.GameplaySettings.Validate() ?? configuration.OutputSettings.Validate();
            if (validationResult != null)
            {
                Console.WriteLine(validationResult);
                return -1;
            }

            var maxImportanceWaitArg = argsDictionary.GetValueOrDefault("-maxImportanceWait");
            int? maxImportanceWait = null;
            if (maxImportanceWaitArg != null)
            {
                if (maxImportanceWaitArg.Count > 1)
                {
                    throw new ArgumentException("Invalid argument.", "-maxImportanceWaitArg");
                }
                maxImportanceWait = int.Parse(maxImportanceWaitArg.SingleOrDefault());
                if (maxImportanceWait < 5)
                {
                    maxImportanceWait = 5; // Threads don't cancel properly if you cancel the token too quickly.
                }
            }

            try
            {
                string result;
                using (var progressBar = new ProgressBar())
                {
                    //var progressReporter = new TextWriterProgressReporter(Console.Out);
                    var progressReporter = new ProgressBarProgressReporter(progressBar, maxImportanceWait);
                    result = ConfigurationProcessor.Process(configuration, seed, progressReporter);
                }
                if (result != null)
                {
                    Console.Error.WriteLine(result);
                }
                else
                {
                    Console.WriteLine($"Generation complete! Output to: {directory}");
                }
                return result == null ? 0 : -1;
            }
            catch (Exception e)
            {
                Console.Error.Write(e.Message);
                Console.Error.Write(e.StackTrace);
                return -1;
            }
        }

        private static List<int> ConvertIntString(string c)
        {
            var result = new List<int>();
            if (string.IsNullOrWhiteSpace(c))
            {
                return result;
            }
            try
            {
                result.Clear();
                string[] v = c.Split('-');
                int[] vi = new int[13];
                if (v.Length != vi.Length)
                {
                    return null;
                }
                for (int i = 0; i < 13; i++)
                {
                    if (v[12 - i] != "")
                    {
                        vi[i] = Convert.ToInt32(v[12 - i], 16);
                    }
                }
                for (int i = 0; i < 32 * 13; i++)
                {
                    int j = i / 32;
                    int k = i % 32;
                    if (((vi[j] >> k) & 1) > 0)
                    {
                        if (i >= ItemUtils.AllLocations().Count())
                        {
                            throw new IndexOutOfRangeException();
                        }
                        result.Add(i);
                    }
                }
            }
            catch
            {
                return null;
            }
            return result;
        }

        private static List<Item> ConvertItemString(List<Item> items, string c)
        {
            var sectionCount = (int)Math.Ceiling(items.Count / 32.0);
            var result = new List<Item>();
            if (string.IsNullOrWhiteSpace(c))
            {
                return result;
            }
            try
            {
                string[] v = c.Split('-');
                int[] vi = new int[sectionCount];
                if (v.Length != vi.Length)
                {
                    return null;
                }
                for (int i = 0; i < sectionCount; i++)
                {
                    if (v[sectionCount - 1 - i] != "")
                    {
                        vi[i] = Convert.ToInt32(v[sectionCount - 1 - i], 16);
                    }
                }
                for (int i = 0; i < 32 * sectionCount; i++)
                {
                    int j = i / 32;
                    int k = i % 32;
                    if (((vi[j] >> k) & 1) > 0)
                    {
                        if (i >= items.Count)
                        {
                            throw new IndexOutOfRangeException();
                        }
                        result.Add(items[i]);
                    }
                }
            }
            catch
            {
                return null;
            }
            return result;
        }

        private const string DEFAULT_SETTINGS_FILENAME = "settings";
        private const string SETTINGS_EXTENSION = "json";
        private static void SaveSettings(Configuration configuration, string filename = null)
        {
            var path = Path.ChangeExtension(filename ?? Path.Combine(Values.MainDirectory, DEFAULT_SETTINGS_FILENAME), SETTINGS_EXTENSION);
            string logicFilePath = null;
            //if (filename != null)
            //{
            //    logicFilePath = configuration.GameplaySettings.UserLogicFileName;
            //    configuration.GameplaySettings.UserLogicFileName = null;
            //    if (configuration.GameplaySettings.LogicMode == LogicMode.UserLogic && logicFilePath != null && File.Exists(logicFilePath))
            //    {
            //        using (StreamReader Req = new StreamReader(File.OpenRead(logicFilePath)))
            //        {
            //            configuration.GameplaySettings.Logic = Req.ReadToEnd();
            //            if (configuration.GameplaySettings.Logic.StartsWith("{"))
            //            {
            //                var logicConfiguration = Configuration.FromJson(configuration.GameplaySettings.Logic);
            //                configuration.GameplaySettings.Logic = logicConfiguration.GameplaySettings.Logic;
            //            }
            //        }
            //    }
            //    configurationToSave = new Configuration
            //    {
            //        GameplaySettings = configuration.GameplaySettings,
            //    };
            //}
            var fileInfo = new FileInfo(path);
            fileInfo.Directory.Create();
            File.WriteAllText(fileInfo.FullName, configuration.ToString());
            //if (logicFilePath != null)
            //{
            //    configuration.GameplaySettings.UserLogicFileName = logicFilePath;
            //    configuration.GameplaySettings.Logic = null;
            //}
        }

        private static Configuration LoadSettings(string filename = null)
        {
            var path = Path.ChangeExtension(filename ?? Path.Combine(Values.MainDirectory, DEFAULT_SETTINGS_FILENAME), SETTINGS_EXTENSION);
            if (File.Exists(path))
            {
                Configuration configuration;
                using (StreamReader Req = new StreamReader(File.OpenRead(path)))
                {
                    configuration = Configuration.FromJson(Req.ReadToEnd());
                }

                if (configuration.GameplaySettings.Logic != null)
                {
                    configuration.GameplaySettings.UserLogicFileName = path;
                    configuration.GameplaySettings.Logic = null;
                }
                if (!File.Exists(configuration.GameplaySettings.UserLogicFileName))
                {
                    configuration.GameplaySettings.UserLogicFileName = string.Empty;
                }
                return configuration;
            }
            return null;
        }

        private static string GetEnumSettingDescription<T>(Expression<Func<Configuration, T>> propertySelector) where T : struct
        {
            return GetSettingDescription(((MemberExpression)propertySelector.Body).Member.Name, string.Join('|', Enum.GetNames(typeof(T))));
        }

        private static string GetEnumArraySettingDescription<T>(Expression<Func<Configuration, T[]>> propertySelector) where T : struct
        {
            return GetSettingDescription(((MemberExpression)propertySelector.Body).Member.Name, $"[{string.Join('|', Enum.GetNames(typeof(T)))}]");
        }

        private static string GetArrayValueDescription(string name, IEnumerable<string> values)
        {
            return GetSettingDescription(name, string.Join('|', values));
        }

        private static string GetSettingPath<T>(Expression<Func<Configuration, T>> expression)
        {
            return string.Join('.', expression.Body.ToString().Split('.').Skip(1));
        }

        private static string GetSettingDescription(string name, string description)
        {
            return $"{name,-17} {description}";
        }
    }
}
