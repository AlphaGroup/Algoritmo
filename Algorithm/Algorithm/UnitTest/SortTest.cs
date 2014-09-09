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
            // Test input of ints
            // The inputs used to test every sort method
            List<int>[] intInputs =
            {
                new List<int>(){5,3,7,8,10,9,2},
                new List<int>(){3,7,8,10,9,100,2,20,16,13,10,59}
            };
            // The sorts to be tested
            ISort<int>[] intSorts =
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
            foreach (var sort in intSorts)
            {
                foreach (var input in intInputs)
                {
                    var output = new List<int>(input);
                    sort.Sort(output);
                    var standard = new List<int>(input);
                    standard.Sort();
                    bool flag = ListEqual(output, standard, Comparer<int>.Default);
                    Assert.AreEqual(true, flag);
                }
            }
            // Test input of doubles
            // The inputs used to test every sort method
            List<double>[] doubleInputs =
            {
                new List<double>(){5.3,3.5,7.2,8.4,10.1,9.1,2.0},
                new List<double>(){3.1,7.1,8.6,10.7,9.32,100.234,2.233,20.455,16.765,13.664,10.234,59.999}
            };
            // The sorts to be tested
            ISort<double>[] doubleSorts =
            {
                new InsertSort<double>(),  // Insertion sort
                new MergeSort<double>(),   // Merge sort
                new BubbleSort<double>(),  // Bubble sort
                new ShellSort<double>(),   // Shell sort
                new HeapSort<double>(),    // Heap sort
                new QuickSort<double>(),   // Quick sort
                new SelectionSort<double>(),   // Selection sort
            };
            foreach (var sort in doubleSorts)
            {
                foreach (var input in doubleInputs)
                {
                    var output = new List<double>(input);
                    sort.Sort(output);
                    var standard = new List<double>(input);
                    standard.Sort();
                    bool flag = ListEqual(output, standard, Comparer<double>.Default);
                    Assert.AreEqual(true, flag);
                }
            }
            // For special inputs
            List<double>[] bucketInputs =
            {
                new List<double>(){.3,.5,.2,.4,0.1,.1,.0},
                new List<double>(){.1,.1,.6,.7,.32,.234,.233,.455,.765,.664,.234,.999}
            };
            // The sorts to be tested
            var bucketSort = new BucketSort();
            foreach (var input in bucketInputs)
            {
                var output = new List<double>(input);
                bucketSort.Sort(output);
                var standard = new List<double>(input);
                standard.Sort();
                bool flag = ListEqual(output, standard, Comparer<double>.Default);
                Assert.AreEqual(true, flag);
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
