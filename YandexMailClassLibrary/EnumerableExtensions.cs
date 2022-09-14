using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YandexMailClassLibrary
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<int> TakeRandomly(this IEnumerable<int> enumerable, int length)
        {
            return enumerable.OrderBy(x => Guid.NewGuid()).Take(length);
        }
    }
}
