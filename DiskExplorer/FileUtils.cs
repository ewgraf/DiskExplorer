using System;
using System.Linq;

namespace DiskExplorer
{
    public static class FileUtils
    {
        public static readonly string[] FileSuffixes = new[] { "kF", "MF", "GF", "TF" };
        public static readonly string[] SizeSuffixes = new[] { "Bytes", "kB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

        public enum PrefixType
        {
            File,
            Size
        }

        [Obsolete]
        public static string GetPrefix(double value, PrefixType type)
        {
            int prefixId = 0;
            if (value < 1000d) {
                return $"{value:#.#} Bytes";
            }
            while ((value /= 1024) > 1000d) {
                prefixId++;
            }
            return $"{value:#.#} {(type == PrefixType.File ? FileSuffixes[prefixId] : SizeSuffixes[prefixId])}";
        }

        public static long GetPrefixValue(string value)
        {
            if (string.IsNullOrEmpty(value)) {
                throw new ArgumentException(nameof(string.IsNullOrEmpty), nameof(value));
            }
            if(!SizeSuffixes.Contains(value)) {
                throw new ArgumentException("Prefix should be length on 2, at least at 20.08.2017 14:06 GMT+3");
            }
            long multiplier = 1024;
            long prefixId = 0;
            while (SizeSuffixes[prefixId++] != value) { }
            long result = (long)Math.Pow(multiplier, prefixId++);
            return result;
        }

        // https://stackoverflow.com/questions/14488796/does-net-provide-an-easy-way-convert-bytes-to-kb-mb-gb-etc
        public static string SizeSuffix(long value, int decimalPlaces = 1)
        {
            if (value < 0) {
                return "-" + SizeSuffix(-value); }
            if (value == 0) {
                return "0 Bytes";
            }

            // mag is 0 for bytes, 1 for KB, 2, for MB, etc.
            int mag = (int)Math.Log(value, 1024);

            // 1L << (mag * 10) == 2 ^ (10 * mag) 
            // [i.e. the number of bytes in the unit corresponding to mag]
            decimal adjustedSize = (decimal)value / (1L << (mag * 10));

            // make adjustment when the value is large enough that
            // it would round up to 1000 or more
            if (Math.Round(adjustedSize, decimalPlaces) >= 1000) {
                mag += 1;
                adjustedSize /= 1024;
            }

            return string.Format("{0:n" + decimalPlaces + "} {1}", adjustedSize, SizeSuffixes[mag]);
        }
    }
}
