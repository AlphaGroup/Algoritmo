/*
 * worst-case: theta(n^2)
 * average-case: theta(n)
 * The bucket sort assumes that the inputs are a uniform distribution.
 * Which means that all inputs belong to [0,1) randomly.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm.Interface;

namespace Algorithm.Sort
{
    class BucketSort : ISort<double>
    {
        public void Sort(List<double> inList)
        {
            Sort(inList, Comparer<double>.Default);
        }

        public void Sort(List<double> inList, IComparer<double> comparer)
        {
            // Check the inputs
            var max = inList.Max();
            var min = inList.Min();
            if (max >= 1 || min < 0)
            {
                throw new Exception("Error! Invalid input for bucket sort!");
            }
            // Create the auxiliary array of lists
            var n = inList.Count();
            var arrOfList = new List<double>[n];
            foreach (var elem in inList)
            {
                // Calculate the index in auxiliary array
                var index = (int)(n * elem);
                try
                {
                    arrOfList[index].Add(elem);
                }
                catch (NullReferenceException ex)
                {
                    // The list object is not created yet
                    arrOfList[index] = new List<double>();
                    arrOfList[index].Add(elem);
                }
            }
            // Sort each bucket
            var sorter = new InsertSort<double>();
            foreach (var list in arrOfList)
            {
                if (list != null)
                {
                    sorter.Sort(list);
                }
            }
            // Concatenate the lists
            var output = new List<double>();
            foreach (var list in arrOfList)
            {
                if (list != null)
                {
                    output.AddRange(list);
                }
            }
            // Change the input list
            for (int i = 0; i < n; ++i)
            {
                inList[i] = output[i];
            }
        }
    }
}
