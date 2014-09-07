/*
 * This data structure is implemented according to <<Introduction to algorithm(3th)>> P163
 * The Priority queue is based on heap.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.DataStructure
{
    class PriorityQueue<T>
    {
        private Heap<T> _heap;

        // The default constructor
        public PriorityQueue()
        {
            _heap = new Heap<T>();
            _heap.HeapSize = 0;
        }
        // The constructor
        public PriorityQueue(T[] input)
        {
            _heap = new Heap<T>(input);
            _heap.HeapSize = input.Length;
        }
        // Return the max element
        public T Max()
        {
            if (_heap.HeapSize == 0)
            {
                throw new Exception("Error! No elements in queue!");
            }
            else
            {
                return _heap[0];
            }
        }

        // Return the max element and remove it from the container.
        public T ExtractMax()
        {
            if (_heap.HeapSize == 0)
            {
                throw new Exception("Error! No elements in queue!");
            }
            else
            {
                T max = _heap[0];
                _heap[0] = _heap[_heap.HeapSize - 1];
                _heap.HeapSize--;
                _heap.MaxHeapify(0);
                return max;
            }
        }

        // Increase one element's key value.
        public bool IncreaseKey(int index, T newVal)
        {
            return IncreaseKey(index, newVal, Comparer<T>.Default);
        }
        public bool IncreaseKey(int index, T newVal, Comparer<T> comparer)
        {
            if (index < 0 || index > _heap.HeapSize - 1)
            {
                return false;
            }
            else
            {
                _heap[index] = newVal;
                // Keep comparing from index to root
                while (index > 0 || comparer.Compare(_heap[_heap.Parent(index)], _heap[index]) < 0)
                {
                    _heap.Exchange(_heap.Parent(index), index);
                    index = _heap.Parent(index);
                }
                return true;
            }
        }

        // Insert a new element into the queue
        public bool Insert(T newObj)
        {
            _heap.Push(newObj);
            _heap.HeapSize++;
            IncreaseKey(_heap.HeapSize - 1, newObj);
            return true;
        }
    }
}
