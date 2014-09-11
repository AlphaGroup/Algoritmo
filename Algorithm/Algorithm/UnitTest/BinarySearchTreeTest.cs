using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Algorithm.DataStructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithm.Interface;

namespace Algorithm.UnitTest
{
    class BinarySearchTreeTest : IUnitTest
    {
        public void Test()
        {
            var tree = new BinarySearchTree<int>();
            tree.Insert(12);
            tree.Insert(5);
            tree.Insert(2);
            tree.Insert(18);
            tree.Insert(9);
            tree.Insert(15);
            tree.Insert(19);
            tree.Insert(13);
            tree.Insert(17);
            Assert.AreEqual(19, tree.Maximum());
            Assert.AreEqual(2, tree.Minimum());
            Assert.AreEqual(17, tree.Search(17).Key);
            Assert.AreEqual(12, tree.Search(12).Key);
            Assert.AreEqual(5, tree.Search(5).Key);
            tree.Delete(tree.Search(12));
            Assert.AreEqual(19, tree.Maximum());
            Assert.AreEqual(2, tree.Minimum());
            Assert.AreEqual(17, tree.Search(17).Key);
            Assert.AreEqual(null, tree.Search(12));
            Assert.AreEqual(5, tree.Search(5).Key);
            tree.Delete(tree.Search(2));
            Assert.AreEqual(19, tree.Maximum());
            Assert.AreEqual(5, tree.Minimum());
            Assert.AreEqual(17, tree.Search(17).Key);
            Assert.AreEqual(null, tree.Search(12));
            Assert.AreEqual(5, tree.Search(5).Key);
        }
    }
}
