#region

using System;
using System.Collections.Generic;

#endregion

namespace gameserver
{
    public static class EnumerableUtils
    {
        public static T RandomElement<T>(this IEnumerable<T> source, Random rng)
        {
            T current = default(T);
            int count = 0;
            foreach (T element in source)
            {
                count++;
                if (rng.Next(count) == 0)
                {
                    current = element;
                }
            }
            if (count == 0)
            {
                throw new InvalidOperationException("Sequence was empty");
            }
            return current;
        }
    }

    public static class StringUtils
    {
        public static bool ContainsIgnoreCase(this string self, string val) => self.IndexOf(val, StringComparison.InvariantCultureIgnoreCase) != -1;

        public static bool EqualsIgnoreCase(this string self, string val) => self.Equals(val, StringComparison.InvariantCultureIgnoreCase);
    }

    public static class MathsUtils
    {
        public static double Dist(double x1, double y1, double x2, double y2) => Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));

        public static double DistSqr(double x1, double y1, double x2, double y2) => (x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2);

        public static double NextDouble(this Random rand, double minValue, double maxValue) => rand.NextDouble() * (maxValue - minValue) + minValue;

        public static List<T> Clone<T>(this List<T> list) => new List<T>(list);
    }
}