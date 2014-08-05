using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm.DataStructure;
using Algorithm.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithm.UnitTest
{
    class HeapTest : IUnitTest
    {
        public void Test()
        {
            // This input is from <<Intro to Algo>> P158
            var heap = new Heap<int>(new int[] { 4, 1, 3, 2, 16, 9, 10, 14, 8, 7 });
            Assert.AreEqual(6, heap.Parent(14));
            Assert.AreEqual(6, heap.Parent(13));
            Assert.AreEqual(11, heap.Parent(23));
            Assert.AreEqual(11, heap.Parent(24));
            Assert.AreEqual(5, heap.Left(2));
            Assert.AreEqual(19, heap.Left(9));
            Assert.AreEqual(12, heap.Right(5));
            Assert.AreEqual(24, heap.Right(11));
            heap.BuildMaxHeap();
            var maxHeap = new int[] { 16, 14, 10, 8, 7, 9, 3, 2, 4, 1 };
            Assert.AreEqual(true, ArrayEqual(maxHeap, heap.InnerArray, Comparer<int>.Default));
        }

        public bool ArrayEqual<T>(T[] arr1, T[] arr2, IComparer<T> comparer)
        {
            if (arr1.Length != arr2.Length)
            {
                return false;
            }
            for (int i = 0; i < arr1.Length; ++i)
            {
                if (comparer.Compare(arr1[i], arr2[i]) != 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
