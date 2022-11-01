using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutomationPracticeClassLibrary.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Randomize<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.OrderBy(_ => Guid.NewGuid());
        }
    }
}
