using MMR.Common.Extensions;
using MMR.Randomizer.Models.Rom;
using MMR.Randomizer.Utils;
using System;
using System.Linq;

namespace MMR.Randomizer.Models.SoundEffects
{
    public static class SoundEffectExtensions
    {
        public const ushort DefaultSoundEffectFlags = 0x0800;

        private static TAttribute GetAttribute<TAttribute>(this SoundEffect value) where TAttribute : Attribute
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            return type.GetField(name)
                .GetCustomAttributes(false)
                .OfType<TAttribute>()
                .SingleOrDefault();
        }

        /// <summary>
        /// Checks if current sound is tagged with provided tag
        /// </summary>
        /// <returns></returns>
        public static bool HasTag(this SoundEffect sound, SoundEffectTag tag)
        {
            return sound.GetAttribute<TagsAttribute>()?.Tags?.Contains(tag) ?? false;
        }

        /// <summary>
        /// Retrieves the list of tags that are valid for this sound's replacement. If the sound has no ReplacableByTags attribute specified, returns all tags.
        /// </summary>
        /// <returns></returns>
        public static SoundEffectTag[] ReplacableByTags(this SoundEffect sound)
        {
            return sound.GetAttribute<ReplacableByTagsAttribute>()?.Tags?.ToArray()
                ?? sound.GetAttribute<TagsAttribute>()?.Tags?.ToArray()
                ?? Enum.GetValues(typeof(SoundEffectTag)).Cast<SoundEffectTag>().ToArray();
        }

        /// <summary>
        /// <para>Replaces current sound effect with a new one</para>
        /// </summary>
        /// <returns>True if the sound was replaced, false otherwise.</returns>
        public static bool TryReplaceWith(this SoundEffect source, SoundEffect newSound)
        {
            var replacableAttribute = source.GetAttribute<ReplacableAttribute>();

            if (replacableAttribute == null)
            {
                return false;
            }

            var addresses = replacableAttribute.Addresses;

            var newValue = (ushort)newSound;

            var effect = source.GetAttribute<EffectAttribute>();
            if (effect != null)
            {
                newValue = (ushort)((effect.Flags & 0x0E00) | (newValue & 0xF1FF));
            }
            else
            {
                newValue = (ushort)(newSound + DefaultSoundEffectFlags);
            }

            foreach (var address in addresses)
            {
                ReadWriteUtils.WriteToROM(address, newValue);
            }

            return true;
        }
    }
}
