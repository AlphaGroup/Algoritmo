// This class is implemented according to <<Intro to Algo>> Page 151
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm.DataStructure;
using Algorithm.Interface;

namespace Algorithm.Sort
{
    class HeapSort<T> : ISort<T>
    {
        public void Sort(List<T> inList)
        {
            Sort(inList, Comparer<T>.Default);
        }

        public void Sort(List<T> inList, IComparer<T> comparer)
        {
            var heap = new Heap<T>(inList.ToArray());
            heap.BuildMaxHeap();
            for (int i = heap.Length - 1; i > 0; --i)
            {
                heap.Exchange(0, i);
                --heap.HeapSize;
                heap.MaxHeapify(0);
            }
            // Change the input
            for (int i = 0; i < inList.Count; ++i)
            {
                inList[i] = heap[i];
            }
        }
    }
}
