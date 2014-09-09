/*
 * P195
 * Counting Sort can only be performed on non-negetive integer inputs.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm.Interface;

namespace Algorithm.Sort
{
    class CountingSort : ISort<int>
    {
        public void Sort(List<int> inList)
        {
            Sort(inList, Comparer<int>.Default);
        }

        // The comparer wouldn't be used
        public void Sort(List<int> inList, IComparer<int> comparer)
        {
            // Check inputs
            var max = inList.Max();
            var min = inList.Min();
            if (min < 0)
            {
                throw new Exception("Error! Inputs shouldn't be negative!");
            }
            // Create the auxiliary array
            var auxArr = new int[max + 1];
            // Count elements
            foreach (var elem in inList)
            {
                auxArr[elem]++;
            }
            // Calculate the postion
            for (int i = 1; i < max + 1; ++i)
            {
                auxArr[i] = auxArr[i] + auxArr[i - 1];
            }
            // Create the output array
            var output = new int[inList.Count()];
            foreach (var elem in inList)
            {
                // The index should be count-1
                output[--auxArr[elem]] = elem;
            }
            // Change the input
            for (var i = 0; i < inList.Count(); ++i)
            {
                inList[i] = output[i];
            }
        }
    }
}
