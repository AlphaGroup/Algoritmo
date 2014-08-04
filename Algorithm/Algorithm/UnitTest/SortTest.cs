using System;
using System.Collections.Generic;
using System.Linq;
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
            ISort<int>[] sorts =
            {
                new InsertSort<int>(), 
            };
            var input = new List<int>()
            {
                5,
                3,
                7,
                8,
                10,
                9,
                2
            };
            foreach (var sort in sorts)
            {
                bool flag = ListEqual(sort.Sort(input), input);
                Assert.AreEqual(true, flag);
            }
        }

        public bool ListEqual<T>(List<T> lList, List<T> rList)
        {
            var ldiff = lList.Except(rList);
            var rdiff = rList.Except(lList);
            if (!ldiff.Any() && !rdiff.Any())
            {
                return true;
            }
            return false;
        }
    }
}
