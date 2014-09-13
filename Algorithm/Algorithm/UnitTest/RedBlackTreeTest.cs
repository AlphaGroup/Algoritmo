using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm.Interface;
using Algorithm.DataStructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithm.UnitTest
{
    class RedBlackTreeTest : IUnitTest
    {
        public void Test()
        {
            var rbtree = new RedBlackTree<int>();
            rbtree.Insert(12);
            rbtree.Insert(5);
            rbtree.Insert(2);
            rbtree.Insert(18);
            rbtree.Insert(9);
            rbtree.Insert(15);
            rbtree.Insert(19);
            rbtree.Insert(13);
            rbtree.Insert(17);
            Assert.AreEqual(19, rbtree.Maximum());
            Assert.AreEqual(2, rbtree.Minimum());
            Assert.AreEqual(17, rbtree.Search(17).Key);
            Assert.AreEqual(12, rbtree.Search(12).Key);
            Assert.AreEqual(5, rbtree.Search(5).Key);
            // Delete the root. In this case, it will cause right rotation.
            rbtree.Delete(rbtree.Search(12));
            Assert.AreEqual(19, rbtree.Maximum());
            Assert.AreEqual(2, rbtree.Minimum());
            Assert.AreEqual(17, rbtree.Search(17).Key);
            Assert.AreEqual(RedBlackTree<int>.RedBlackTreeNode<int>.Nil, rbtree.Search(12));
            Assert.AreEqual(5, rbtree.Search(5).Key);
            rbtree.Delete(rbtree.Search(5));
            Assert.AreEqual(19, rbtree.Maximum());
            Assert.AreEqual(2, rbtree.Minimum());
            Assert.AreEqual(17, rbtree.Search(17).Key);
            Assert.AreEqual(RedBlackTree<int>.RedBlackTreeNode<int>.Nil, rbtree.Search(5));
            Assert.AreEqual(RedBlackTree<int>.RedBlackTreeNode<int>.Nil, rbtree.Search(12));
            Assert.AreEqual(15, rbtree.Search(15).Key);
        }
    }
}
