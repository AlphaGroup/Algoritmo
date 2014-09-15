/*
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
                Color = NodeColor.Black
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
        // Min and Max
        new public T Minimum()
        {
            return Minimum(_root).Key;
        }
        public OSRBTreeNode<T> Minimum(OSRBTreeNode<T> root)
        {
            return (OSRBTreeNode<T>)base.Minimum(root);
        }
        new public T Maximum()
        {
            return Maximum(_root).Key;
        }
        public OSRBTreeNode<T> Maximum(OSRBTreeNode<T> root)
        {
            return (OSRBTreeNode<T>)base.Maximum(root);
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
            throw new NotImplementedException();
        }
        protected override void RightRotate(RBTreeNode<T> node)
        {
            throw new NotImplementedException();
        }

        // Insert
        new public void Insert(T newVal)
        {
            Insert(newVal, Comparer<T>.Default);
        }
        new public void Insert(T newVal, IComparer<T> comparer)
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
