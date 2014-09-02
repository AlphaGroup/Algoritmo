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
        // This is the list containing actions
        public readonly List<object> ActionList = new List<object>();
        // This is the inner array
        public T[] InnerArray { get; set; }
        // indicate how many elements are in array
        public int Length
        {
            get { return InnerArray.Length; }
        }
        // The indexer
        public T this[int index]
        {
            get { return InnerArray[index]; }
            set { InnerArray[index] = value; }
        }

        // indicate how many elements are in heap
        public int HeapSize { get; set; }

        // It won't copy or clone the inArray.
        public Heap(T[] inArray)
        {
            InnerArray = inArray;
        }

        /// <summary>
        /// Build innerArray to a max-heap.
        /// </summary>
        public void BuildMaxHeap()
        {
            HeapSize = InnerArray.Length;
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
                comparer.Compare(InnerArray[left], InnerArray[index]) > 0)
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
                comparer.Compare(InnerArray[right], InnerArray[largest]) > 0)
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
            T temp = InnerArray[i];
            InnerArray[i] = InnerArray[j];
            InnerArray[j] = temp;
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
