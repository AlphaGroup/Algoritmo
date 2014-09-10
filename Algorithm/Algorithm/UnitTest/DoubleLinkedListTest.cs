using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithm.DataStructure;

namespace Algorithm.UnitTest
{
    class DoubleLinkedListTest : IUnitTest
    {
        public void Test()
        {
            var dbList = new DoubleLinkedList<int>();
            dbList.Insert(9);
            dbList.Insert(13);
            dbList.Insert(1);
            dbList.Insert(41);
            dbList.Insert(28);
            // The list now will be 28,41,1,13,9
            Assert.AreEqual(1, dbList[2]);
            Assert.AreEqual(9, dbList[4]);
            dbList.Delete(dbList.Search(1));
            Assert.AreEqual(13, dbList[2]);
        }
    }
}
