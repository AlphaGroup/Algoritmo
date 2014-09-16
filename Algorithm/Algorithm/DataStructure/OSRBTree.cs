/*
 * This tree is a variant of red-black tree. It is called order statistic tree.
 * To maintain the correctness of its size property, we have to modify RBTree's 
 * LeftRotate, RightRotate, Insert, InsertFixUp and Delete.
 */
using System;
using System.CodeDom.Compiler;
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

            // Override GetNil
            public override RBTreeNode<TP> GetNil()
            {
                return Nil;
            }
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
            return (OSRBTreeNode<T>)base.Minimum(root);
        }
        public override T Maximum()
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
            var nil = node.GetNil() as OSRBTreeNode<T>;
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
            x.Size = x.LeftNode.Size + x.RightNode.Size + 1;
        }
        protected override void RightRotate(RBTreeNode<T> node)
        {
            var nil = node.GetNil() as OSRBTreeNode<T>;
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
            x.Size = x.LeftNode.Size + x.RightNode.Size + 1;
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
                Key = newVal,
                Size = 1
            };
            Insert(node, comparer);
        }
        public void Insert(OSRBTreeNode<T> newNode)
        {
            Insert(newNode, Comparer<T>.Default);
        }
        public void Insert(OSRBTreeNode<T> newNode, IComparer<T> comparer)
        {
            var nil = newNode.GetNil() as OSRBTreeNode<T>;
            var parent = nil;
            var temp = _root;
            // Find the right place for new node
            while (temp != nil)
            {
                // Begin code for order statistics
                ++temp.Size;
                // End
                parent = temp;
                temp = comparer.Compare(newNode.Key, temp.Key) < 0 ? temp.LeftNode : temp.RightNode;
            }
            newNode.ParentNode = parent;
            if (parent == nil)
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
            newNode.LeftNode = nil;
            newNode.RightNode = nil;
            newNode.Color = OSRBTreeNode<T>.NodeColor.Red;
            // We don't have to change this function. We just override rotation funcitons and 
            // the InsertFixUp will automatically call the right ones.
            InsertFixUp(newNode);
        }

        // Delete
        public void Delete(OSRBTreeNode<T> removed)
        {
            var nil = removed.GetNil() as OSRBTreeNode<T>;
            // We set this variable because we care about the color of the node actually removed.
            var actualRemoved = removed;
            var yOldColor = actualRemoved.Color;
            // This replacer is the one who moves to the y's original position.
            OSRBTreeNode<T> replacer = null;
            // The removed has only one or less children.
            if (removed.LeftNode == nil)
            {
                replacer = removed.RightNode;
                Transplant(removed, removed.RightNode);
            }
            else if (removed.RightNode == nil)
            {
                replacer = removed.LeftNode;
                Transplant(removed, removed.LeftNode);
            }
            // The removed has two children.
            else
            {
                // Find the successor.
                actualRemoved = Minimum(removed.RightNode);
                yOldColor = actualRemoved.Color;
                replacer = actualRemoved.RightNode;
                // The successor is a direct child.
                if (actualRemoved.ParentNode == removed)
                {
                    replacer.ParentNode = actualRemoved;
                }
                // The successor is a indirect child
                else
                {
                    Transplant(actualRemoved, actualRemoved.RightNode);
                    actualRemoved.RightNode = removed.RightNode;
                    actualRemoved.RightNode.ParentNode = actualRemoved;
                }
                Transplant(removed, actualRemoved);
                // To some extend, we can comprehend it in this way:
                // Although the node removed is the input node,
                // logically speaking, the node actually removed is the successor, and let the (input's node).key = successor's key.
                // That's why we have the rest code.
                actualRemoved.LeftNode = removed.LeftNode;
                actualRemoved.LeftNode.ParentNode = actualRemoved;
                actualRemoved.Color = removed.Color;
                actualRemoved.Size = removed.Size;
            }
            // Adjust the size property
            if (replacer != nil)
            {
                replacer.Size = replacer.LeftNode.Size + replacer.RightNode.Size + 1;
            }
            while (replacer.ParentNode != nil)
            {
                replacer = replacer.ParentNode;
                --replacer.Size;
            }
            // If the color of the node actually removed is black, then this could violate RBT's properties.
            if (yOldColor == OSRBTreeNode<T>.NodeColor.Black)
            {
                DeleteFixUp(replacer);
            }
        }

        // The operations for an order statistic.
        // Return the element whose rank is zero-based os'th.
        public OSRBTreeNode<T> Select(int os)
        {
            return Select(_root, os);
        }
        public OSRBTreeNode<T> Select(OSRBTreeNode<T> tmpRoot, int os)
        {
            if (os + 1 > tmpRoot.Size)
            {
                return tmpRoot.GetNil() as OSRBTreeNode<T>;
            }
            // The rank of the input tmpRoot
            var rank = tmpRoot.LeftNode.Size;
            // This is the node we are looking for.
            if (os == rank)
            {
                return tmpRoot;
            }
            // The node we are looking are on the left subtree of tmpRoot.
            else if (os < rank)
            {
                return Select(tmpRoot.LeftNode, os);
            }
            // The node we are looking are on the right subtree of tmpRoot.
            else
            {
                return Select(tmpRoot.RightNode, os - rank - 1);
            }
        }
        // Return the rank of the node.
        public int Rank(OSRBTreeNode<T> node)
        {
            throw new NotImplementedException();
        }
    }
}
