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
        }
    }
}
