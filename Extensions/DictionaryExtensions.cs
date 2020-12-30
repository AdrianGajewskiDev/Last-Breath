using System;
using System.Collections.Generic;
using System.Linq;

public static class DictionaryExtensions
{
    public static TKey[] GetSortedAscending<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
    {
        var keys = dictionary.Keys.ToArray();
        Array.Sort(keys);

        return keys;
    }
}
