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
            // DEBUG
            inList.Sort();
            return;
            // DEBUG
            // Check the inputs
            var max = inList.Max();
            var min = inList.Min();
            if (max >= 1 || min < 0)
            {
                throw new Exception("Error! Invalid input for bucket sort!");
            }
            var arrOfList = new List<double>[inList.Count()];
        }
    }
}
