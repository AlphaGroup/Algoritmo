// This data structure represents Heap.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm.Interface;

namespace Algorithm.DataStructure
{
    /// <summary>
    /// Represent heap.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class Heap<T>
    {
        // This is the inner container
        private List<T> _container;
        // This is the list containing actions
        public readonly List<object> ActionList = new List<object>();
        // indicate how many elements are in heap
        public int HeapSize { get; set; }
        // indicate how many elements are in array
        public int Length
        {
            get { return _container.Count(); }
        }

        // The indexer
        public T this[int index]
        {
            get { return _container[index]; }
            set { _container[index] = value; }
        }

        // The default constructor
        public Heap()
        {
            _container = new List<T>();
        }

        // The contructor
        public Heap(T[] inArray)
        {
            _container = new List<T>(inArray);
        }

        // Add an element into heap
        public bool Push(T newVal)
        {
            _container.Add(newVal);
            return true;
        }

        // Return the result in a new array
        public T[] Output()
        {
            return _container.ToArray();
        }

        /// <summary>
        /// Build innerArray to a max-heap.
        /// </summary>
        public void BuildMaxHeap()
        {
            // The heap's size should be set by customer
            //HeapSize = InnerArray.Length;
            // Start from leaves
            for (int i = Length / 2 - 1; i >= 0; --i)
            {
                MaxHeapify(i);
            }
        }

        /// <summary>
        /// <para>Maintain max-heap property.</para>
        /// <para>Assuming left and right sub trees are max-heaped.</para>
        /// </summary>
        /// <param name="index"></param>
        public void MaxHeapify(int index)
        {
            MaxHeapify(index, Comparer<T>.Default);
        }

        /// <summary>
        /// <para>Maintain max-heap property.</para>
        /// <para>Assuming left and right sub trees are max-heaped.</para>
        /// </summary>
        public void MaxHeapify(int index, IComparer<T> comparer)
        {
            int left = Left(index);
            int right = Right(index);
            int largest = index;
            // For JSON
            ActionList.Add(new { action = "MARK", param = string.Format(@"{0}", largest) });
            // End JSON
            // Find the largest one in these three(right,left,index)
            if (left < HeapSize &&
                comparer.Compare(_container[left], _container[index]) > 0)
            {
                // For JSON 
                ActionList.Add(new { action = "MARK", param = string.Format(@"{0}", -(largest + 1)) });
                // End JSON 
                largest = left;
                // For JSON
                ActionList.Add(new { action = "MARK", param = string.Format(@"{0}", largest) });
                // End JSON
            }
            if (right < HeapSize &&
                comparer.Compare(_container[right], _container[largest]) > 0)
            {
                // For JSON 
                ActionList.Add(new { action = "MARK", param = string.Format(@"{0}", -(largest + 1)) });
                // End JSON 
                largest = right;
                // For JSON
                ActionList.Add(new { action = "MARK", param = string.Format(@"{0}", largest) });
                // End JSON
            }
            // Exchange elements if necessary and recursively solve it.
            if (largest != index)
            {
                Exchange(largest, index);
                // For JSON
                ActionList.Add(new { action = "EXCG", param = string.Format(@"{0},{1}", largest, index) });
                ActionList.Add(new { action = "MARK", param = string.Format(@"{0}", -(index + 1)) });
                // End JSON
                MaxHeapify(largest, comparer);
            }
            else
            {
                // For JSON 
                ActionList.Add(new { action = "MARK", param = string.Format(@"{0}", -(largest + 1)) });
                // End JSON 
            }
        }

        public void Exchange(int i, int j)
        {
            T temp = _container[i];
            _container[i] = _container[j];
            _container[j] = temp;
        }

        /// <summary>
        /// return index of the input's parent.
        /// if input is the root, return root itself.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int Parent(int index)
        {
            if (index == 0)
                return 0;
            return (index - 1) / 2;
        }

        /// <summary>
        /// Return index of the left element of the input.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int Left(int index)
        {
            return (index << 1) + 1;
        }

        /// <summary>
        /// Return index of the right element of the input.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int Right(int index)
        {
            return (index << 1) + 2;
        }

    }
}
