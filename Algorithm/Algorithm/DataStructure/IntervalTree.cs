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
        protected override void LeftRotate(RBTreeNode<T> node)
        {
            var nil = node.GetNil();
            // Set right node
            var rNode = node.RightNode;
            // Move rNode's left subtree to node's right subtree.
            node.RightNode = rNode.LeftNode;
            if (rNode.LeftNode != nil)
            {
                rNode.LeftNode.ParentNode = node;
            }
            // Set rNode's parent.
            rNode.ParentNode = node.ParentNode;
            // Set node's parent node child to rNode
            if (node.ParentNode == nil)
            {
                _root = rNode as IntervalTreeNode<T>;
            }
            else if (node == node.ParentNode.LeftNode)
            {
                node.ParentNode.LeftNode = rNode;
            }
            else
            {
                node.ParentNode.RightNode = rNode;
            }
            // Set other links
            rNode.LeftNode = node;
            node.ParentNode = rNode;
            // New code for interval tree:
            // Fix the Max properties.
            var iNode = node as IntervalTreeNode<T>;
            iNode.Max = CalculateMax(iNode);
            var parent = iNode.ParentNode;
            parent.Max = CalculateMax(parent);
        }
        protected override void RightRotate(RBTreeNode<T> node)
        {
            var nil = node.GetNil();
            // Set left node.
            var lNode = node.LeftNode;
            // Move lNode's right subtree to node's left subtree.
            node.LeftNode = lNode.RightNode;
            if (lNode.RightNode != nil)
            {
                lNode.RightNode.ParentNode = node;
            }
            // Set lNode's parent.
            lNode.ParentNode = node.ParentNode;
            // Set node's parent node's child to lNode
            if (node.ParentNode == nil)
            {
                _root = lNode as IntervalTreeNode<T>;
            }
            else if (node == node.ParentNode.LeftNode)
            {
                node.ParentNode.LeftNode = lNode;
            }
            else
            {
                node.ParentNode.RightNode = lNode;
            }
            // Set other links
            lNode.RightNode = node;
            node.ParentNode = lNode;
            // New code for interval tree:
            // Fix the Max properties.
            var iNode = node as IntervalTreeNode<T>;
            iNode.Max = CalculateMax(iNode);
            var parent = iNode.ParentNode;
            parent.Max = CalculateMax(parent);
        }
        // Use default comparer to calculate the Max value
        private T CalculateMax(IntervalTreeNode<T> node)
        {
            var max = node.Max;
            var comparer = Comparer<T>.Default;
            if (comparer.Compare(node.LeftNode.Max, max) > 0)
            {
                max = node.LeftNode.Max;
            }
            if (comparer.Compare(node.RightNode.Max, max) > 0)
            {
                max = node.RightNode.Max;
            }
            return max;
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
                if (temp.LeftNode != IntervalTreeNode<T>.Nil && comparer.Compare(temp.LeftNode.Max, low) >= 0)
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
