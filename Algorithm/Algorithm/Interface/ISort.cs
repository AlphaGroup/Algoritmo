using System.Collections.Generic;

namespace Algorithm.Interface
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
