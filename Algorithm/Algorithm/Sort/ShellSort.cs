
// This sort is not recorded in <<Intro to Algo>> Thomas H. Cormen
// The reference is from :
// http://baike.baidu.com/view/178698.htm?fr=aladdin#reference-[2]-178698-wrap
// http://en.wikipedia.org/wiki/Shellsort
// The delta could be length/2 or length/3. Here we use length/2, which is the most popular one.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm.Interface;

namespace Algorithm.Sort
{
    class ShellSort<T> : ISort<T>
    {
        public void Sort(List<T> inList)
        {
            Sort(inList,Comparer<T>.Default);
        }

        public void Sort(List<T> inList, IComparer<T> comparer)
        {
            int len = inList.Count;
            // delta is the distance between each element in the same h-sub array
            int delta = len / 2;
            while (delta >= 1)
            {
                // subEnd is the index of the end element in a h-sub array
                int subEnd = delta;
                while (subEnd < len)
                {
                    // At first, subStart is the index of the start element in a h-sub array
                    // And then, it's used to traverse the whole sub array.
                    var key = inList[subEnd];
                    var subStart = subEnd - delta;
                    while (subStart >= 0 &&
                        comparer.Compare(inList[subStart], key) > 0)
                    {
                        inList[subStart + delta] = inList[subStart];
                        subStart -= delta;
                    }
                    inList[subStart + delta] = key;

                    ++subEnd;
                }
                delta /= 2;
            }
        }
    }
}
