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
            // Test Max property's maintainance.
            Assert.AreEqual(30, (intv.Search(16) as IntervalTree<int>.IntervalTreeNode<int>).Max);
            Assert.AreEqual(23, (intv.Search(8) as IntervalTree<int>.IntervalTreeNode<int>).Max);
            Assert.AreEqual(23, (intv.Search(15) as IntervalTree<int>.IntervalTreeNode<int>).Max);
            Assert.AreEqual(10, (intv.Search(5) as IntervalTree<int>.IntervalTreeNode<int>).Max);
            Assert.AreEqual(10, (intv.Search(6) as IntervalTree<int>.IntervalTreeNode<int>).Max);
            Assert.AreEqual(3, (intv.Search(0) as IntervalTree<int>.IntervalTreeNode<int>).Max);
            Assert.AreEqual(30, (intv.Search(25) as IntervalTree<int>.IntervalTreeNode<int>).Max);
            Assert.AreEqual(20, (intv.Search(17) as IntervalTree<int>.IntervalTreeNode<int>).Max);
            Assert.AreEqual(26, (intv.Search(26) as IntervalTree<int>.IntervalTreeNode<int>).Max);
            Assert.AreEqual(20, (intv.Search(19) as IntervalTree<int>.IntervalTreeNode<int>).Max);
        }
    }
}
