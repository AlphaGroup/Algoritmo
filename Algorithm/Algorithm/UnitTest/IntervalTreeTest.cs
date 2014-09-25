/*
 * The unit test for interval tree.
 */
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
    class IntervalTreeTest : IUnitTest
    {
        public void Test()
        {
            var intv = new IntervalTree<int>();
            intv.Insert(16, 21);
            intv.Insert(8, 9);
            intv.Insert(25, 30);
            intv.Insert(5, 8);
            intv.Insert(15, 23);
            intv.Insert(17, 19);
            intv.Insert(26, 26);
            intv.Insert(0, 3);
            intv.Insert(6, 10);
            intv.Insert(19, 20);
            // Test Search and Deletion
            Assert.AreEqual(16, intv.Search(14, 16).Key);
            Assert.AreEqual(21, intv.Search(14, 16).High);
            intv.Delete(intv.Search(16));
            Assert.AreEqual(15, intv.Search(14, 16).Key);
            Assert.AreEqual(23, intv.Search(14, 16).High);
            Assert.AreEqual(15, intv.Search(22, 24).Key);
            Assert.AreEqual(23, intv.Search(22, 24).High);
            intv.Delete(intv.Search(15));
            Assert.AreEqual(IntervalTree<int>.IntervalTreeNode<int>.Nil, intv.Search(22, 24));
        }
    }
}
