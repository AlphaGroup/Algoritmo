using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Algorithm.Interface;
using Algorithm.Sort;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithm.UnitTest
{
    class SortTest : IUnitTest
    {
        public void Test()
        {
            // The inputs used to test every sort method
            List<int>[] inputs =
            {
                new List<int>(){5,3,7,8,10,9,2},
                new List<int>(){3,7,8,10,9,100,2,20,16,13,10,59}
            };
            // The sorts to be tested
            ISort<int>[] sorts =
            {
                new InsertSort<int>(),  // Insertion sort
                new MergeSort<int>(),   // Merge sort
                new BubbleSort<int>(),  // Bubble sort
                new ShellSort<int>(),   // Shell sort
                new HeapSort<int>(),    // Heap sort
                new QuickSort<int>(),   // Quick sort
                new SelectionSort<int>(),   // Selection sort
                new CountingSort(),     // Counting sort
            };
            foreach (var sort in sorts)
            {
                foreach (var input in inputs)
                {
                    var output = new List<int>(input);
                    sort.Sort(output);
                    var standard = new List<int>(input);
                    standard.Sort();
                    bool flag = ListEqual(output, standard, Comparer<int>.Default);
                    Assert.AreEqual(true, flag);
                }
            }
        }

        public bool ListEqual<T>(List<T> lList, List<T> rList, IComparer<T> comparer)
        {
            if (lList.Count != rList.Count)
            {
                return false;
            }
            else
            {
                int n = lList.Count;
                for (int i = 0; i < n; ++i)
                {
                    if (0 != comparer.Compare(lList[i], rList[i]))
                        return false;
                }
                return true;
            }
        }
    }
}
