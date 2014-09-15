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

        private OSRBTreeNode<T> _root = null;

        new public T Minimum()
        {
            return Minimum(_root).Key;
        }

        public OSRBTreeNode<T> Minimum(OSRBTreeNode<T> root)
        {
            return (OSRBTreeNode<T>)base.Minimum(root);
        }

        public OSRBTreeNode<T> Maximum(OSRBTreeNode<T> root)
        {
            return (OSRBTreeNode<T>)base.Maximum(root);
        }

        public new OSRBTreeNode<T> Search(T key)
        {
            throw new NotImplementedException();
        }

        OSRBTreeNode<T> IDynamicSet<T, OSRBTreeNode<T>>.Search(T key, IComparer<T> comparer)
        {
            throw new NotImplementedException();
        }

        public OSRBTreeNode<T> Search(OSRBTreeNode<T> root, T key)
        {
            throw new NotImplementedException();
        }

        public OSRBTreeNode<T> Search(OSRBTreeNode<T> root, T key, IComparer<T> comparer)
        {
            throw new NotImplementedException();
        }

        public OSRBTreeNode<T> Successor(OSRBTreeNode<T> root)
        {
            throw new NotImplementedException();
        }

        public OSRBTreeNode<T> Predecessor(OSRBTreeNode<T> root)
        {
            throw new NotImplementedException();
        }

        public void Insert(OSRBTreeNode<T> newNode)
        {
            throw new NotImplementedException();
        }

        public void Insert(OSRBTreeNode<T> newNode, IComparer<T> comparer)
        {
            throw new NotImplementedException();
        }

        public void Delete(OSRBTreeNode<T> node)
        {
            throw new NotImplementedException();
        }
    }
}
