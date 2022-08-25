using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ioni.Extensions
{
    /// <summary>
    /// Extension methods for Lists
    /// </summary>
    public static class ListExtensions 
    {
        /// <summary>
        /// Return a random item from the list.
        /// Sampling with replacement.
        /// </summary>
        /// <typeparam name="T">type</typeparam>
        /// <param name="list">this</param>
        /// <returns>Random Item</returns>
        public static T RandomItem<T>(this IList<T> list)
        {
            return list[Random.Range(0, list.Count)];
        }
        
        /// <summary>
        /// Removes a random item from the list, returning that item.
        /// Sampling without replacement.
        /// </summary>
        /// <typeparam name="T">type</typeparam>
        /// <param name="list">this</param>
        /// <returns>Removed Random Item</returns>
        public static T RemoveRandom<T>(this IList<T> list)
        {
            if (list.Count == 0) throw new System.IndexOutOfRangeException("Cannot remove a random item from an empty list");
            int index = UnityEngine.Random.Range(0, list.Count);
            T item = list[index];
            list.RemoveAt(index);
            return item;
        }
    }   
}
