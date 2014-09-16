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
    class OSRBTreeTest : IUnitTest
    {
        public void Test()
        {
            var osrb = new OSRBTree<int>();
            // Part I: Test the BST properties
            osrb.Insert(12);
            osrb.Insert(5);
            osrb.Insert(2);
            osrb.Insert(18);
            osrb.Insert(9);
            osrb.Insert(15);
            osrb.Insert(19);
            osrb.Insert(13);
            osrb.Insert(17);
            Assert.AreEqual(19, osrb.Maximum());
            Assert.AreEqual(2, osrb.Minimum());
            Assert.AreEqual(17, osrb.Search(17).Key);
            Assert.AreEqual(12, osrb.Search(12).Key);
            Assert.AreEqual(5, osrb.Search(5).Key);
            // Delete the root. In this case, it will cause right rotation.
            osrb.Delete(osrb.Search(12));
            Assert.AreEqual(19, osrb.Maximum());
            Assert.AreEqual(2, osrb.Minimum());
            Assert.AreEqual(17, osrb.Search(17).Key);
            Assert.AreEqual(OSRBTree<int>.OSRBTreeNode<int>.Nil, osrb.Search(12));
            Assert.AreEqual(5, osrb.Search(5).Key);
            osrb.Delete(osrb.Search(5));
            Assert.AreEqual(19, osrb.Maximum());
            Assert.AreEqual(2, osrb.Minimum());
            Assert.AreEqual(17, osrb.Search(17).Key);
            Assert.AreEqual(OSRBTree<int>.OSRBTreeNode<int>.Nil, osrb.Search(5));
            Assert.AreEqual(OSRBTree<int>.OSRBTreeNode<int>.Nil, osrb.Search(12));
            Assert.AreEqual(15, osrb.Search(15).Key);
            // Part II: test the IOrderStatistic
            IOrderStatistic<int, OSRBTree<int>.OSRBTreeNode<int>> ios = osrb;
            Assert.AreEqual(2, ios.Select(0).Key);
            Assert.AreEqual(9, ios.Select(1).Key);
            Assert.AreEqual(13, ios.Select(2).Key);
            Assert.AreEqual(15, ios.Select(3).Key);
            Assert.AreEqual(17, ios.Select(4).Key);
            Assert.AreEqual(18, ios.Select(5).Key);
            Assert.AreEqual(19, ios.Select(6).Key);
            Assert.AreEqual(0, ios.Rank(ios.Select(0)));
            Assert.AreEqual(1, ios.Rank(ios.Select(1)));
            Assert.AreEqual(2, ios.Rank(ios.Select(2)));
            Assert.AreEqual(3, ios.Rank(ios.Select(3)));
            Assert.AreEqual(4, ios.Rank(ios.Select(4)));
            Assert.AreEqual(5, ios.Rank(ios.Select(5)));
            Assert.AreEqual(6, ios.Rank(ios.Select(6)));
        }
    }
}
