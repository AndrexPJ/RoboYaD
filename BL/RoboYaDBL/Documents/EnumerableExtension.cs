using System;
using System.Collections.Generic;

namespace RoboYaDBL.Documents
{
    static class EnumerableExtension
    {
        public static Dictionary<TKey, TValue> ToOldestValueDictionary<TKey, TValue>(this IEnumerable<TValue> enumerable, Func<TValue, TKey> keySelector)
            where TValue : IHasDateTime
        {
            var result = new Dictionary<TKey, TValue>();
            foreach (var element in enumerable)
            {
                var key = keySelector(element);
                if (result.ContainsKey(key) && result[key].datetime > element.datetime)
                    continue;
                result[key] = element;
            }
            return result;
        }      
    }
}