using System;
using System.Collections.Generic;

namespace MMR.Common.Utils
{
    public static class DictionaryHelper
    {
        public static Dictionary<string, List<string>> FromProgramArguments(string[] args)
        {
            var result = new Dictionary<string, List<string>>();
            if (args.Length == 0)
            {
                return result;
            }
            string currentArgument = null;
            string currentParameter = string.Empty;
            bool inQuote = false;
            foreach (var arg in args)
            {
                var processingArg = arg;
                if (processingArg.StartsWith("-"))
                {
                    currentArgument = processingArg;
                    result[currentArgument] = new List<string>();
                    continue;
                }
                if (currentArgument == null)
                {
                    throw new ArgumentException($"Error: unnamed argument '{processingArg}'");
                }

                if (!inQuote)
                {
                    if (processingArg.StartsWith("\""))
                    {
                        processingArg = processingArg.Substring(1);
                        inQuote = true;
                    }
                }
                if (inQuote)
                {
                    if (processingArg.EndsWith("\""))
                    {
                        processingArg = processingArg.Substring(0, processingArg.Length - 1);
                        inQuote = false;
                    }
                }

                if (inQuote)
                {
                    currentParameter += processingArg + " ";
                }
                else
                {
                    result[currentArgument].Add(currentParameter + processingArg);
                    currentParameter = string.Empty;
                }
            }
            return result;
        }
    }
}
