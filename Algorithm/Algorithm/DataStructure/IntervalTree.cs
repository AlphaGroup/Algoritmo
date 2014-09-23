/*
 * P348
 * This data structure called interval tree is an augment of red-black tree.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.DataStructure
{
    class IntervalTree<T> : RBTree<T>
    {
        public class IntervalTreeNode<TP> : RBTreeNode<TP>
        {
            public new static IntervalTreeNode<TP> Nil = new IntervalTreeNode<TP>()
            {
                Color = NodeColor.Black
            };
            public override RBTreeNode<TP> GetNil()
            {
                return Nil;
            }
        }
    }
}
