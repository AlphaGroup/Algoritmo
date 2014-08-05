﻿using System;
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
        /// </summary>
        /// <param name="inList"></param>
        /// <returns></returns>
        public void Sort(List<T> inList)
        {
            Sort(inList, Comparer<T>.Default);
        }

        /// <summary>
        /// Use inputed comparer.
        /// </summary>
        /// <param name="inList"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public void Sort(List<T> inList, IComparer<T> comparer)
        {
            for (int j = 1; j < inList.Count; ++j)
            {
                var key = inList.ElementAt(j);
                var i = j - 1;
                while (i >= 0 && comparer.Compare(inList[i], key) > 0)
                {
                    inList[i + 1] = inList[i];
                    --i;
                }
                inList[i + 1] = key;
            }
        }
    }
}
