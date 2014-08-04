using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sort
{
    class BubbleSort<T> : ISort<T>
    {
        public List<T> Sort(List<T> inList)
        {
            return Sort(inList, Comparer<T>.Default);
        }

        public List<T> Sort(List<T> inList, IComparer<T> comparer)
        {
            var result = new List<T>(inList);
            int length = result.Count;
            for (int i = 0; i < length - 1; ++i)
            {
                for (int j = length - 1; j > i; --j)
                {
                    if (comparer.Compare(result[j - 1], result[j]) > 0)
                    {
                        T temp = result[j];
                        result[j] = result[j - 1];
                        result[j - 1] = temp;
                    }
                }
            }
            return result;
        }
    }
}
