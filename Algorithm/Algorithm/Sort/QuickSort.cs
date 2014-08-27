using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm.Interface;

namespace Algorithm.Sort
{
    class QuickSort<T> : ISort<T>, IActionProvider
    {
        private IComparer<T> _comparer = null;
        private List<object> _actionList = new List<object>();
        public void Sort(List<T> inList)
        {
            Sort(inList, Comparer<T>.Default);
        }

        public void Sort(List<T> inList, IComparer<T> comparer)
        {
            _comparer = comparer;
            // Clear the json result
            _actionList.Clear();
            Quick(inList, 0, inList.Count - 1);
        }

        public List<object> GetListForJson()
        {
            return _actionList;
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
        /// <param name="end">Included</param>
        /// <returns></returns>
        private int Partition(List<T> list, int start, int end)
        {
            // Now choose the last element as pivot
            var pivot = list[end];
            // For JSON
            _actionList.Add(new { action = "ASGN", param = string.Format(@"{0}={1}", -1, end) });
            // The lastSmall indicates index of the last ele which is smaller than pivot ele
            int lastSmall = start - 1;
            for (int j = start; j < end; j++)
            {
                if (_comparer.Compare(list[j], pivot) <= 0)
                {
                    ++lastSmall;
                    // Make sure it won't exchange with itself
                    if (lastSmall != j)
                    {
                        // Exchange each other
                        Exchange(list, lastSmall, j);
                        // For JSON
                        _actionList.Add(new { action = "EXCG", param = string.Format(@"{0},{1}", lastSmall, j) });
                    }
                }
            }
            // Make sure it won't exchange itself
            if (lastSmall + 1 != end)
            {
                // Exchange the pivot and the middle element
                Exchange(list, lastSmall + 1, end);
                // For JSON
                _actionList.Add(new { action = "EXCG", param = string.Format(@"{0},{1}", lastSmall + 1, -1) });
            }
            // For JSON
            _actionList.Add(new { action = "ASGN", param = string.Format(@"{0}={1}", end, -1) });
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
