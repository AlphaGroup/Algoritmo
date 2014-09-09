using Algorithm.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm.DataStructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithm.UnitTest
{
    class PriorityQueueTest : IUnitTest
    {
        public void Test()
        {
            var queue = new PriorityQueue<int>(new int[] { 1, 6, 4, 5, 2, 99, 23 });
            Assert.AreEqual(99, queue.Max());
            Assert.AreEqual(99, queue.ExtractMax());
            Assert.AreEqual(23, queue.Max());
            queue.Insert(77);
            Assert.AreEqual(77, queue.Max());
            queue.IncreaseKey(3, 100);
            Assert.AreEqual(100, queue.ExtractMax());
            Assert.AreEqual(77, queue.Max());
            queue.Insert(110);
            Assert.AreEqual(110, queue.Max());
        }
    }
}
