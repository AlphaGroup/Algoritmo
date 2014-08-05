using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sort
{
    class QuickSort<T> : ISort<T>
    {
        private IComparer<T> _comparer = null;
        public void Sort(List<T> inList)
        {
            Sort(inList, Comparer<T>.Default);
        }

        public void Sort(List<T> inList, IComparer<T> comparer)
        {
            _comparer = comparer;
            Quick(inList, 0, inList.Count - 1);
        }

        public void Quick(List<T> list, int start, int end)
        {
            if (start < end)
            {
                int pos = Partition(list, start, end);
                Quick(list, start, pos - 1);
                Quick(list, pos, end);
            }
        }

        /// <summary>
        /// <para>Rearranges the subarray in place.</para>
        /// <para>Return the position of the pivot element</para>
        /// </summary>
        /// <param name="list"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private int Partition(List<T> list, int start, int end)
        {
            // TODO: now choose the last element as pivot
            var pivot = list[end];
            // The lastSmall indicates index of the last ele which is smaller than pivot ele
            int lastSmall = start - 1;
            for (int j = start; j < end; j++)
            {
                if (_comparer.Compare(list[j], pivot) <= 0)
                {
                    ++lastSmall;
                    Exchange(list, lastSmall, j);
                }
            }
            Exchange(list, lastSmall + 1, end);
            return lastSmall + 1;
        }

        private void Exchange(List<T> list, int i, int j)
        {
            var temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }
}
