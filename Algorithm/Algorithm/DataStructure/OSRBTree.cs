using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm.Interface;

namespace Algorithm.DataStructure
{
    class OSRBTree<T> : RBTree<T>, IDynamicSet<T, OSRBTree<T>.OSRBTreeNode<T>>
    {
        // The OSRBTree's node structure
        public class OSRBTreeNode<TP> : RBTreeNode<TP>
        {
            // The size indicates how much elements are there in the subtree rooted at this node.
            public int Size { get; set; }
            new public static OSRBTreeNode<TP> Nil = new OSRBTreeNode<TP>
            {
                Color = NodeColor.Black
            };
        }

        // The root node
        private OSRBTreeNode<T> _root = null;

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
            throw new NotImplementedException();
        }

        // Delete
        public void Delete(OSRBTreeNode<T> node)
        {
            throw new NotImplementedException();
        }
    }
}
