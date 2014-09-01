// This class is implemented according to <<Intro to Algo>> Page 151

using System;
using System.Collections.Generic;
using Algorithm.DataStructure;
using Algorithm.Interface;

namespace Algorithm.Sort
{
    internal class HeapSort<T> : ISort<T>, IActionProvider
    {
        private Heap<T> _heap = null;
        public List<object> GetListForJson()
        {
            if (_heap != null)
            {
                return _heap.ActionList;
            }
            else
            {
                return null;
            }
        }

        public void Sort(List<T> inList)
        {
            Sort(inList, Comparer<T>.Default);
        }

        public void Sort(List<T> inList, IComparer<T> comparer)
        {
            _heap = new Heap<T>(inList.ToArray());
            _heap.BuildMaxHeap();
            for (int i = _heap.Length - 1; i > 0; --i)
            {
                _heap.Exchange(0, i);
                --_heap.HeapSize;
                _heap.MaxHeapify(0);
            }
            // Change the input
            for (int i = 0; i < inList.Count; ++i)
            {
                inList[i] = _heap[i];
            }
        }
    }
}