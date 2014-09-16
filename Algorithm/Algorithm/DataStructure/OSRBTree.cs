﻿/*
 * This tree is a variant of red-black tree. It is called order statistic tree.
 * To maintain the correctness of its size property, we have to modify RBTree's 
 * LeftRotate, RightRotate, Insert, InsertFixUp and Delete.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm.Interface;

namespace Algorithm.DataStructure
{
    class OSRBTree<T> : RBTree<T>, IDynamicSet<T, OSRBTree<T>.OSRBTreeNode<T>>, IOrderStatistic<T, OSRBTree<T>.OSRBTreeNode<T>>
    {
        // The OSRBTree's node structure
        public class OSRBTreeNode<TP> : RBTreeNode<TP>
        {
            // The size indicates how much elements are there in the subtree rooted at this node.
            public int Size { get; set; }
            // Use base's pointers to implement these pointers.
            new public OSRBTreeNode<TP> ParentNode
            {
                get
                {
                    return (OSRBTreeNode<TP>)base.ParentNode;
                }
                set
                {
                    base.ParentNode = (OSRBTreeNode<TP>)value;
                }
            }
            new public OSRBTreeNode<TP> LeftNode
            {
                get
                {
                    return (OSRBTreeNode<TP>)base.LeftNode;
                }
                set
                {
                    base.LeftNode = (OSRBTreeNode<TP>)value;
                }
            }
            new public OSRBTreeNode<TP> RightNode
            {
                get
                {
                    return (OSRBTreeNode<TP>)base.RightNode;
                }
                set
                {
                    base.RightNode = (OSRBTreeNode<TP>)value;
                }
            }
            public new static OSRBTreeNode<TP> Nil = new OSRBTreeNode<TP>
            {
                Color = NodeColor.Black,
                Size = 0
            };
        }

        // The root node
        protected new OSRBTreeNode<T> _root
        {
            get
            {
                return (OSRBTreeNode<T>)base._root;
            }
            set
            {
                base._root = (RBTreeNode<T>)value;
            }
        }
        // Constructor
        public OSRBTree()
            : base(OSRBTreeNode<T>.Nil)
        {
        }
        // Min and Max
        public override T Minimum()
        {
            return Minimum(_root).Key;
        }
        public OSRBTreeNode<T> Minimum(OSRBTreeNode<T> root)
        {
            // TODO: Find a way to use base.Minimum.( Create a function return the nill virtually)
            while (root.LeftNode != OSRBTreeNode<T>.Nil)
            {
                root = root.LeftNode;
            }
            return root;
        }
        public override T Maximum()
        {
            return Maximum(_root).Key;
        }
        public OSRBTreeNode<T> Maximum(OSRBTreeNode<T> root)
        {
            // TODO: Find a way to use base.Maximum.( Create a function return the nill virtually)
            while (root.RightNode != OSRBTreeNode<T>.Nil)
            {
                root = root.RightNode;
            }
            return root;
        }

        // Search
        // Use father's search function.
        new public OSRBTreeNode<T> Search(T key)
        {
            return (OSRBTreeNode<T>)base.Search(_root, key, Comparer<T>.Default);
        }
        public OSRBTreeNode<T> Search(OSRBTreeNode<T> root, T key)
        {
            return (OSRBTreeNode<T>)base.Search(root, key, Comparer<T>.Default);
        }
        new public OSRBTreeNode<T> Search(T key, IComparer<T> comparer)
        {
            return (OSRBTreeNode<T>)base.Search(_root, key, comparer);
        }
        public OSRBTreeNode<T> Search(OSRBTreeNode<T> root, T key, IComparer<T> comparer)
        {
            return (OSRBTreeNode<T>)base.Search(root, key, comparer);
        }

        // Successor and Predecessor
        public OSRBTreeNode<T> Successor(OSRBTreeNode<T> root)
        {
            return (OSRBTreeNode<T>)base.Successor(root);
        }
        public OSRBTreeNode<T> Predecessor(OSRBTreeNode<T> root)
        {
            return (OSRBTreeNode<T>)base.Predecessor(root);
        }

        // Override Rotations
        protected override void LeftRotate(RBTreeNode<T> node)
        {
            // Set right node
            var rNode = node.RightNode;
            // Move rNode's left subtree to node's right subtree.
            node.RightNode = rNode.LeftNode;
            if (rNode.LeftNode != OSRBTreeNode<T>.Nil)
            {
                rNode.LeftNode.ParentNode = node;
            }
            // Set rNode's parent.
            rNode.ParentNode = node.ParentNode;
            // Set node's parent node child to rNode
            if (node.ParentNode == OSRBTreeNode<T>.Nil)
            {
                _root = (OSRBTreeNode<T>)rNode;
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
            // Adjust size property
            var x = (OSRBTreeNode<T>)node;
            var y = x.ParentNode;
            y.Size = x.Size;
            x.Size = x.LeftNode.Size + x.RightNode.Size;
        }
        protected override void RightRotate(RBTreeNode<T> node)
        {
            // Set left node.
            var lNode = node.LeftNode;
            // Move lNode's right subtree to node's left subtree.
            node.LeftNode = lNode.RightNode;
            if (lNode.RightNode != OSRBTreeNode<T>.Nil)
            {
                lNode.RightNode.ParentNode = node;
            }
            // Set lNode's parent.
            lNode.ParentNode = node.ParentNode;
            // Set node's parent node's child to lNode
            if (node.ParentNode == OSRBTreeNode<T>.Nil)
            {
                _root = (OSRBTreeNode<T>)lNode;
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
            // Adjust size property
            var x = (OSRBTreeNode<T>)node;
            var y = x.ParentNode;
            y.Size = x.Size;
            x.Size = x.LeftNode.Size + x.RightNode.Size;
        }

        // Insert
        public override void Insert(T newVal)
        {
            Insert(newVal, Comparer<T>.Default);
        }
        public override void Insert(T newVal, IComparer<T> comparer)
        {
            var node = new OSRBTreeNode<T>()
            {
                Key = newVal
            };
            Insert(node, comparer);
        }
        public void Insert(OSRBTreeNode<T> newNode)
        {
            Insert(newNode, Comparer<T>.Default);
        }
        public void Insert(OSRBTreeNode<T> newNode, IComparer<T> comparer)
        {
            var parent = OSRBTreeNode<T>.Nil;
            var temp = _root;
            // Find the right place for new node
            while (temp != OSRBTreeNode<T>.Nil)
            {
                // Begin code for order statistics
                ++temp.Size;
                // End
                parent = temp;
                temp = comparer.Compare(newNode.Key, temp.Key) < 0 ? temp.LeftNode : temp.RightNode;
            }
            newNode.ParentNode = parent;
            if (parent == OSRBTreeNode<T>.Nil)
            {
                _root = newNode;
            }
            else if (comparer.Compare(newNode.Key, parent.Key) < 0)
            {
                parent.LeftNode = newNode;
            }
            else
            {
                parent.RightNode = newNode;
            }
            newNode.LeftNode = OSRBTreeNode<T>.Nil;
            newNode.RightNode = OSRBTreeNode<T>.Nil;
            newNode.Color = OSRBTreeNode<T>.NodeColor.Red;
            // We don't have to change this function. We just override rotation funcitons and 
            // the InsertFixUp will automatically call the right ones.
            InsertFixUp(newNode);
        }

        // Delete
        public void Delete(OSRBTreeNode<T> node)
        {
            throw new NotImplementedException();
        }

        // The operations for an order statistic.
        // Return the element whose rank is zero-based os'th.
        public OSRBTreeNode<T> Select(int os)
        {
            throw new NotImplementedException();
        }
        public OSRBTreeNode<T> Select(OSRBTreeNode<T> root, int os)
        {
            throw new NotImplementedException();
        }
        // Return the rank of the node.
        public int Rank(OSRBTreeNode<T> node)
        {
            throw new NotImplementedException();
        }
    }
}