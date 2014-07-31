using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sort
{
    class InsertSort<T> : ISort<T>
    {
        /// <summary>
        /// Use default comparer.
        /// Won't change inputed list.
        /// </summary>
        /// <param name="inList"></param>
        /// <returns></returns>
        public List<T> Sort(List<T> inList)
        {
            return Sort(inList, Comparer<T>.Default);
        }

        /// <summary>
        /// Use inputed comparer. Won't change inputed list.
        /// </summary>
        /// <param name="inList"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public List<T> Sort(List<T> inList, IComparer<T> comparer)
        {
            var result = new List<T>(inList);
            for (int j = 1; j < result.Count; ++j)
            {
                var key = result.ElementAt(j);
                var i = j - 1;
                while (i >= 0 && comparer.Compare(result[i], key) > 0)
                {
                    result[i + 1] = result[i];
                    --i;
                }
                result[i + 1] = key;
            }
            return result;
        }
    }
}
