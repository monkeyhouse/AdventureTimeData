using System;
using System.Collections.Generic;
using System.Linq;
using ATConsole;

namespace Consoles
{
    public static class ExtensionMethods

    {
        private static  Random random;

        static ExtensionMethods()
        {
            random = new Random((int)DateTime.Now.Ticks);
        }

        public static KeyItemT<T> GetRandomValue<T>(this IEnumerable<T> list)
        {
            var items = list as IList<T> ?? list.ToList();
            if (!items.Any())
                throw new ArgumentNullException("list","list has no elements");

            var i = random.Next(0, items.Count());
            return new KeyItemT<T>(i, items.ElementAt(i));
        }

        public static List<KeyItemT<T>> GetRandomValues<T>(this IEnumerable<T> list, int num)
        {
            var items = list as IList<T> ?? list.ToList();
            var count = items.Count();
            if (count == 0)
                throw new ArgumentNullException("list", "list has no elements");

            if (count < num)
                throw new ArgumentException(String.Format("list has less than {0} elements", num), "list");

            var keyItems = items.Select((t,i) => new KeyItemT<T>(i,t))
                .OrderBy(x => random.Next())
                .Take(num).ToList();
            return keyItems;
        }
     
    }
}