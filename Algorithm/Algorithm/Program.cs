using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm.Interface;
using Algorithm.Sort;
using Algorithm.UnitTest;

namespace Algorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            IUnitTest[] tests =
            {
                new SortTest(), // Test sort methods
                new HeapTest(), // Test heap data structure
                new PriorityQueueTest(), // Test priority-queue data structure
                new DoubleLinkedListTest(), // Test double-linked list
                new BinarySearchTreeTest(), // Test the binary search tree
                new RedBlackTreeTest(), // Test red-black tree structure.
            };
            foreach (var test in tests)
            {
                test.Test();
            }
            Console.WriteLine("End!");
            Console.Read();
        }
    }
}
