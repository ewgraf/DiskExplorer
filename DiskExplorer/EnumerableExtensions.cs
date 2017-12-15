using System;
using System.Collections.Generic;
using System.Linq;

namespace DiskExplorer {
    public static class EnumerableExtensions {
        // https://stackoverflow.com/questions/11830174/how-to-flatten-tree-via-linq
        public static IEnumerable<T> Flatten<T>(this IEnumerable<T> e, Func<T, IEnumerable<T>> f) {
            return e.SelectMany(c => f(c).Flatten(f)).Concat(e);
        }
    }
}
