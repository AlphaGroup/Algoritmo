/*
 * P348
 * This data structure called interval tree is an augment of red-black tree.
 * The concept of "interval" here means a closed range like [16,21].
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Algorithm.DataStructure
{
    class IntervalTree<T> : RBTree<T>
    {
        // The tree node structure
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
            new public IntervalTreeNode<TP> ParentNode
            {
                get { return base.ParentNode as IntervalTreeNode<TP>; }
                set { base.ParentNode = value; }
            }
            new public IntervalTreeNode<TP> LeftNode
            {
                get { return base.LeftNode as IntervalTreeNode<TP>; }
                set { base.LeftNode = value; }
            }
            new public IntervalTreeNode<TP> RightNode
            {
                get { return base.RightNode as IntervalTreeNode<TP>; }
                set { base.RightNode = value; }
            }
            // New properties
            // This is the high bound of the interval and the low bound is the key
            public TP High { get; set; }
            // This is the max value of any interval endpoint stored in the subtree rooted in this node.
            public TP Max { get; set; }
        }

        // The root property
        protected new IntervalTreeNode<T> _root
        {
            get { return base._root as IntervalTreeNode<T>; }
            set { base._root = value; }
        }

        // Constructor
        public IntervalTree()
            : base(IntervalTreeNode<T>.Nil)
        {
        }

        // The override insert methods, for inheritance purpose.
        // Usually won't use this, this will set low=high=val
        public override void Insert(T val)
        {
            Insert(val, val);
        }
        public override void Insert(T newVal, IComparer<T> comparer)
        {
            Insert(newVal, newVal, comparer);
        }
        // The overloaded insert methods.
        public void Insert(T low, T high)
        {
            Insert(low, high, Comparer<T>.Default);
        }
        public void Insert(T low, T high, IComparer<T> comparer)
        {
            var node = new IntervalTreeNode<T>()
            {
                Key = low,
                High = high,
                Max = high,
            };
            var nil = node.GetNil() as IntervalTreeNode<T>;
            var parent = nil;
            var temp = _root;
            // Find the right place for new node
            while (temp != nil)
            {
                // Added: Change the "Max" property
                // Elder node's Max is smaller than new node's Max
                if (comparer.Compare(temp.Max, high) < 0)
                {
                    temp.Max = high;
                }
                // End added
                parent = temp;
                temp = (comparer.Compare(node.Key, temp.Key) < 0 ? temp.LeftNode : temp.RightNode) as IntervalTreeNode<T>;
            }
            node.ParentNode = parent;
            if (parent == nil)
            {
                _root = node;
            }
            else if (comparer.Compare(node.Key, parent.Key) < 0)
            {
                parent.LeftNode = node;
            }
            else
            {
                parent.RightNode = node;
            }
            node.LeftNode = nil;
            node.RightNode = nil;
            node.Color = RBTreeNode<T>.NodeColor.Red;
            InsertFixUp(node);
        }

        // Override rotations
        // TODO
        protected override void LeftRotate(RBTreeNode<T> node)
        {
            base.LeftRotate(node);
        }
        protected override void RightRotate(RBTreeNode<T> node)
        {
            base.RightRotate(node);
        }

        // New operation
        // Search the node whose interval overlaps [low,high].
        public IntervalTreeNode<T> Search(T low, T high)
        {
            return Search(_root, low, high);
        }
        public IntervalTreeNode<T> Search(IntervalTreeNode<T> root, T low, T high)
        {
            return Search(root, low, high, Comparer<T>.Default);
        }
        public IntervalTreeNode<T> Search(IntervalTreeNode<T> root, T low, T high, IComparer<T> comparer)
        {
            var temp = _root;
            while (temp != IntervalTreeNode<T>.Nil && !Overlapped(temp, low, high, comparer))
            {
                if(temp.LeftNode!=IntervalTreeNode<T>.Nil && comparer.Compare(temp.LeftNode.Max,low)>=0)
                {
                    temp = temp.LeftNode;
                }
                else
                {
                    temp = temp.RightNode;
                }
            }
            return temp;
        }

        private bool Overlapped(IntervalTreeNode<T> node, T low, T high, IComparer<T> comparer)
        {
            if (comparer.Compare(node.Key, high) <= 0 &&
                comparer.Compare(low, node.High) <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
