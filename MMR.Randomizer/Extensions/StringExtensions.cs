using MMR.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace MMR.Randomizer.Extensions
{
    public static class StringExtensions
    {
        public static IEnumerable<string> WrapLine(this string str, int width)
        {
            var words = str.Split(' ');
            var lines = new List<string>();
            var currentLine = new List<string>();
            var currentLength = 0;
            foreach (var word in words)
            {
                var length = word.Count(c => HasWidth(c));
                if (currentLength + length > width)
                {
                    lines.Add(string.Join(" ", currentLine));
                    currentLength = 0;
                    currentLine.Clear();
                }
                currentLine.Add(word);
                currentLength += length + 1;
            }
            lines.Add(string.Join(" ", currentLine));
            return lines;
        }
        
        public static string Wrap(this string str, int width, string newline, string endTextBox)
        {
            var newLines = new List<string>();
            var lines = str.Split(new string[] { newline }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                newLines.AddRange(line.WrapLine(width));
            }
            return string.Join(endTextBox, newLines.Chunk(4).Select(chunk => string.Join(newline, chunk)));
        }

        private static readonly ReadOnlyCollection<int> specialCharacters = new ReadOnlyCollection<int>(new int[]
        {
            0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x09, 0x11, 0x12, 0x13, 0x19, 0x1E, 0xBF, 0xC2, 0xC3, 0xE0,
        });

        private static bool HasWidth(char c)
        {
            return !char.IsWhiteSpace(c) && !specialCharacters.Contains(c);
        }

        private static Regex trickUrlRegex = new Regex("^https://www.youtube.com/watch\\?v=[a-zA-Z0-9_-]{11}$");
        public static bool IsValidTrickUrl(this string str)
        {
            return str != null && trickUrlRegex.IsMatch(str);
        }
    }
}
