using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darwin_s_Lab
{
    static class Tools
    {
        public static Random rdm = new Random();

        /// <summary>
        /// Shuffles the given list in place.
        /// Source: https://stackoverflow.com/a/1262619
        /// </summary>
        /// <typeparam name="T">List elements type</typeparam>
        /// <param name="list">list to shuffle in place</param>
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rdm.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
