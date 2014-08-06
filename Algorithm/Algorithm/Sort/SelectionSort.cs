using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sort
{
    class SelectionSort<T> : ISort<T>
    {
        public void Sort(List<T> inList)
        {
            Sort(inList, Comparer<T>.Default);
        }

        public void Sort(List<T> inList, IComparer<T> comparer)
        {
            int len = inList.Count;
            for (int i = 0; i < len - 1; ++i)
            {
                // Find the smallest one among the [i...n]
                int smallest = i;
                var key = inList[i];
                for (int j = i + 1; j < len; ++j)
                {
                    if (comparer.Compare(key, inList[j]) > 0)
                    {
                        key = inList[j];
                        smallest = j;
                    }
                }
                if (smallest != i)
                {
                    Exchange(inList, smallest, i);
                }
            }
        }

        private void Exchange(List<T> list, int i, int j)
        {
            var temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }
}
