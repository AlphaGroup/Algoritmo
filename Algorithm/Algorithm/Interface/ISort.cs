using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sort
{
    /// <summary>
    /// Assuming T implements IComparable.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    interface ISort<T>
    {
        /// <summary>
        /// Sort the input list by default comparer.
        /// </summary>
        /// <param name="inList"></param>
        /// <returns></returns>
        void Sort(List<T> inList);

        ///// <summary>
        ///// Sort the input list
        ///// Compare elements by inputed comparer.
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="inList"></param>
        ///// <param name="comparer"></param>
        ///// <returns></returns>
        void Sort(List<T> inList, IComparer<T> comparer);
    }

}
