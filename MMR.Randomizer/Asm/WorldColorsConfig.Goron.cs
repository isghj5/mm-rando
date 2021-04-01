using MMR.Randomizer.Extensions;
using MMR.Randomizer.Utils;
using SixLabors.ImageSharp.ColorSpaces;
using SixLabors.ImageSharp.ColorSpaces.Conversion;
using System;
using System.Drawing;

namespace MMR.Randomizer.Asm
{
    public partial class WorldColorsConfig
    {
        public readonly struct GoronColorOptions
        {
            /// <summary>
            /// Main color.
            /// </summary>
            public Color Main { get; }

            /// <summary>
            /// Extra color 1.
            /// </summary>
            public Color Extra1 { get; }

            /// <summary>
            /// Extra color 2.
            /// </summary>
            public Color Extra2 { get; }

            /// <summary>
            /// Whether or not to mix the previous environment color's HSV value with the current color's value.
            /// </summary>
            public bool MixEnvValue { get; }

            public GoronColorOptions(Color main, Color extra1, Color extra2, bool mixEnvValue = true)
            {
                Main = main;
                Extra1 = extra1;
                Extra2 = extra2;
                MixEnvValue = mixEnvValue;
            }

            public static GoronColorOptions From(Color main, Color extra, bool mixEnvValue = true)
            {
                return new GoronColorOptions(main, extra.Brighten(0.3f), extra.Darken(0.3f), mixEnvValue);
            }
        }

        /// <summary>
        /// Patch Goron object data to write new energy colors.
        /// </summary>
        /// <param name="data">Object data.</param>
        void PatchGoronEnergyColors(Span<byte> data)
        {
            // Patch SetPrimColor instruction for Goron punch energy color.
            var punchDListOffset = 0x11AB8;
            Colors.GoronPunchEnergyPrim.ToBytesRGB(0xFF).CopyTo(data.Slice(punchDListOffset + 0x1C));

            // Patch SetEnvColor color values for Goron punch in code file: RDRAM: 0x801BFDE0, Offset: 0x11A320.
            var punchEnvColorAddr = 0xB3C000 + 0x11A320;
            ReadWriteUtils.WriteToROM(punchEnvColorAddr, Colors.GoronPunchEnergyEnv2.ToBytesRGB(0));

            // Patch SetPrimColor instruction for interior of Goron roll energy color.
            var innerOffset = 0x127CC;
            var innerPrim = Colors.GoronRollInnerEnergyPrim.ToBytesRGB(0xFF);
            innerPrim.CopyTo(data.Slice(innerOffset));

            // Patch color table used to generate DLists for exterior of Goron roll energy effect.
            var outerOffset = 0x14660;
            var outerPrim1 = Colors.GoronRollOuterEnergyPrim1.ToBytesRGB(0xFF);
            var outerPrim2 = Colors.GoronRollOuterEnergyPrim2.ToBytesRGB(0xFF);
            var outerEnv1 = Colors.GoronRollOuterEnergyEnv1.ToBytesRGB(0xFF);
            var outerEnv2 = Colors.GoronRollOuterEnergyEnv2.ToBytesRGB(0xFF);
            outerPrim1.CopyTo(data.Slice(outerOffset + 0));
            outerPrim2.CopyTo(data.Slice(outerOffset + 5));
            outerEnv1.CopyTo(data.Slice(outerOffset + 0xC));
            outerEnv2.CopyTo(data.Slice(outerOffset + 0x10));
        }

        /// <summary>
        /// Set Goron energy colors from the given options.
        /// </summary>
        /// <param name="options">Color options.</param>
        void SetGoronEnergyColors(GoronColorOptions options)
        {
            Colors.GoronPunchEnergyPrim = GetPunchPrim(options.Main);
            Colors.GoronPunchEnergyEnv1 = options.Extra1;
            Colors.GoronPunchEnergyEnv2 = options.Extra1;

            var inner = Mix(options.Main, Colors.GoronRollInnerEnergyEnv);
            Colors.GoronRollInnerEnergyEnv = inner.Item1;
            Colors.GoronRollInnerEnergyPrim = inner.Item2;

            var outer1 = Mix(options.Extra1, Colors.GoronRollOuterEnergyEnv1);
            Colors.GoronRollOuterEnergyEnv1 = outer1.Item1;
            Colors.GoronRollOuterEnergyPrim1 = outer1.Item2;

            var outer2 = Mix(options.Extra2, Colors.GoronRollOuterEnergyEnv2);
            Colors.GoronRollOuterEnergyEnv2 = outer2.Item1;
            Colors.GoronRollOuterEnergyPrim2 = outer2.Item2;

            static Color GetPunchPrim(Color color)
            {
                var converter = new ColorSpaceConverter();
                return converter.TranslateHsv(color, static hsv => new Hsv(hsv.H, (hsv.S / 5f) * 4f, hsv.V));
            }

            (Color env, Color prim) Mix(Color color, Color env)
            {
                // Convert color values to HSV.
                var converter = new ColorSpaceConverter();
                var curHsv = converter.ToHsv(color);
                var envHsv = converter.ToHsv(env);
                // Calculate delta of Value.
                var envHsvValDelta = ((curHsv.V + envHsv.V) / 2f) - curHsv.V;
                // Multiply by Saturation to limit deviation of Value for greyscale-approaching colors (Saturation approaching 0).
                var envHsvValue = options.MixEnvValue ? curHsv.V + (envHsvValDelta * curHsv.S) : curHsv.V;
                // Build new colors from HSV.
                var envResultHsv = new Hsv(curHsv.H, curHsv.S, envHsvValue);
                var primResultHsv = new Hsv(curHsv.H, curHsv.S, curHsv.V);
                // Convert back into RGB.
                var envResult = converter.ToColor(envResultHsv);
                var primResult = converter.ToColor(primResultHsv);
                return (envResult, primResult);
            }
        }
    }
}
