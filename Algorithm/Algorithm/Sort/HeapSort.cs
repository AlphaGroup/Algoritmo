﻿/*
 * This class is implemented according to <<Intro to Algo>> Page 151
 * Worst-case: O(nlgn)
 * Average-case: N/A
 */

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
            // Set the heap's size
            _heap.HeapSize = _heap.Length;
            // Build the max-heap
            _heap.BuildMaxHeap();
            // Extract the top element from maxed heap
            for (int i = _heap.Length - 1; i > 0; --i)
            {
                _heap.Exchange(0, i);
                // For JSON
                _heap.ActionList.Add(new { action = "EXCG", param = string.Format(@"{0},{1}", 0, i) });
                // End JSON
                --_heap.HeapSize;
                // For JSON
                _heap.ActionList.Add(new { action = "ASGN", param = string.Format(@"{0}={1}", -1, i) });
                // End JSON
                _heap.MaxHeapify(0);
            }
            // For JSON
            _heap.ActionList.Add(new { action = "ASGN", param = string.Format(@"{0}={1}", -1, 0) });
            _heap.ActionList.Add(new { action = "EXIT", param = "" });
            // End JSON
            // Change the input
            for (int i = 0; i < inList.Count; ++i)
            {
                inList[i] = _heap[i];
            }
        }
    }
}