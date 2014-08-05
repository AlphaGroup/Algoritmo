using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sort
{
    class BubbleSort<T> : ISort<T>
    {
        public void Sort(List<T> inList)
        {
            Sort(inList, Comparer<T>.Default);
        }

        public void Sort(List<T> inList, IComparer<T> comparer)
        {
            int length = inList.Count;
            for (int i = 0; i < length - 1; ++i)
            {
                for (int j = length - 1; j > i; --j)
                {
                    if (comparer.Compare(inList[j - 1], inList[j]) > 0)
                    {
                        T temp = inList[j];
                        inList[j] = inList[j - 1];
                        inList[j - 1] = temp;
                    }
                }
            }
        }
    }
}
