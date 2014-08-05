using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sort
{
    class MergeSort<T> : ISort<T>
    {
        public void Sort(List<T> inList)
        {
            Sort(inList, Comparer<T>.Default);
            return;
        }

        public void Sort(List<T> inList, IComparer<T> comparer)
        {
            MergeSortImp(inList, 0, inList.Count - 1, comparer);
        }

        /// <summary>
        /// The actual implement of merge-sort.\n
        /// Will change the input list.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="comparer"></param>
        public void MergeSortImp(List<T> list, int begin, int end, IComparer<T> comparer)
        {
            if (begin < end)
            {
                int mid = (begin + end) / 2;
                MergeSortImp(list, begin, mid, comparer);
                MergeSortImp(list, mid + 1, end, comparer);
                Merge(list, begin, mid, end, comparer);
            }
        }

        /// <summary>
        /// Used in function Sort. this function will change input list.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="begin"></param>
        /// <param name="mid"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private void Merge(List<T> list, int begin, int mid, int end, IComparer<T> comparer)
        {
            // ln is the length of the left list
            int ln = mid - begin + 1;
            var lList = list.GetRange(begin, mid - begin + 1);
            // rn is the length of the right list
            int rn = end - mid;
            var rList = list.GetRange(mid + 1, end - mid);
            // li is the index in the left list
            int li = 0;
            // ri is the index in the right list
            int ri = 0;
            // k is the index in the original list
            for (int k = begin; k <= end; ++k)
            {
                // Check whether one of the lists are empty now.
                if (li == ln)
                {
                    // left list is empty
                    while (ri != rn)
                    {
                        list[k] = rList[ri];
                        ++k;
                        ++ri;
                    }
                    break;
                }
                else if (ri == rn)
                {
                    // right list is empty
                    while (li != ln)
                    {
                        list[k] = lList[li];
                        ++k;
                        ++li;
                    }
                    break;
                }
                else
                {
                    if (comparer.Compare(lList[li], rList[ri]) <= 0)
                    {
                        // Element in left list is smaller or equal
                        list[k] = lList[li];
                        ++li;
                    }
                    else
                    {
                        // Element in right list is smaller or equal
                        list[k] = rList[ri];
                        ++ri;
                    }
                }
            }
            // End of for loop
        }
    }
}
