/*
 * P309
 * Compared to BST, RBT is balanced in order to guarantee that basic dynamic-set operations take O(lgn) time in worst case.
 * The height of a RBTree <= 2lg(n+1)
 */

using System.Collections;
using System.Collections.Generic;
using Algorithm.Interface;

namespace Algorithm.DataStructure
{
    // TODO: Here I want to use interface IDynamicSet and inheritance from BST.
    // TODO: But I have to solve the problem about return value type. Further design is needed.
    class RBTree<T> : IDynamicSet<T, RBTree<T>.RBTreeNode<T>>
    {
        // The class of RBT's node.
        public class RBTreeNode<TP>
        {
            // The node's color
            public enum NodeColor
            {
                Red,
                Black
            }
            // The nil node representing leaves. Its properties have no meanings except Color.
            public static RBTreeNode<TP> Nil = new RBTreeNode<TP>
            {
                Color = NodeColor.Black,
                ParentNode = null,
                LeftNode = null,
                RightNode = null
            };

            public NodeColor Color { get; set; }
            public RBTreeNode<TP> ParentNode { get; set; }
            public RBTreeNode<TP> LeftNode { get; set; }
            public RBTreeNode<TP> RightNode { get; set; }
            public TP Key { get; set; }
        }

        protected RBTreeNode<T> _root = null;
        // Constructors
        public RBTree()
        {
            _root = RBTreeNode<T>.Nil;
        }
        // Constructor for derived classes
        protected RBTree(RBTreeNode<T> root)
        {
            _root = root;
        }

        // Max and min functions
        // Minimum
        public virtual T Minimum()
        {
            return Minimum(_root).Key;
        }
        public RBTreeNode<T> Minimum(RBTreeNode<T> node)
        {
            while (node.LeftNode != RBTreeNode<T>.Nil)
            {
                node = node.LeftNode;
            }
            return node;
        }
        // Maximum
        public virtual T Maximum()
        {
            return Maximum(_root).Key;
        }
        public RBTreeNode<T> Maximum(RBTreeNode<T> node)
        {
            while (node.RightNode != RBTreeNode<T>.Nil)
            {
                node = node.RightNode;
            }
            return node;
        }

        // Search
        public RBTreeNode<T> Search(T key)
        {
            return Search(_root, key);
        }
        public RBTreeNode<T> Search(T key, IComparer<T> comparer)
        {
            return Search(_root, key, comparer);
        }
        public RBTreeNode<T> Search(RBTreeNode<T> root, T key)
        {
            return Search(root, key, Comparer<T>.Default);
        }
        public RBTreeNode<T> Search(RBTreeNode<T> root, T key, IComparer<T> comparer)
        {
            while (root != RBTreeNode<T>.Nil && comparer.Compare(root.Key, key) != 0)
            {
                if (comparer.Compare(key, root.Key) < 0)
                {
                    root = root.LeftNode;
                }
                else
                {
                    root = root.RightNode;
                }
            }
            return root;
        }

        // Rotation: both run in O(1)
        // Left rotation assuming that input.RightNode!=Nil and that the root's parent is Nil.
        protected virtual void LeftRotate(RBTreeNode<T> node)
        {
            // Set right node
            var rNode = node.RightNode;
            // Move rNode's left subtree to node's right subtree.
            node.RightNode = rNode.LeftNode;
            if (rNode.LeftNode != RBTreeNode<T>.Nil)
            {
                rNode.LeftNode.ParentNode = node;
            }
            // Set rNode's parent.
            rNode.ParentNode = node.ParentNode;
            // Set node's parent node child to rNode
            if (node.ParentNode == RBTreeNode<T>.Nil)
            {
                _root = rNode;
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
        }

        // Right rotation assuming that input.LeftNode!=Nil and that the root's parent is Nil.
        protected virtual void RightRotate(RBTreeNode<T> node)
        {
            // Set left node.
            var lNode = node.LeftNode;
            // Move lNode's right subtree to node's left subtree.
            node.LeftNode = lNode.RightNode;
            if (lNode.RightNode != RBTreeNode<T>.Nil)
            {
                lNode.RightNode.ParentNode = node;
            }
            // Set lNode's parent.
            lNode.ParentNode = node.ParentNode;
            // Set node's parent node's child to lNode
            if (node.ParentNode == RBTreeNode<T>.Nil)
            {
                _root = lNode;
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
        }

        // Insert
        public virtual void Insert(T val)
        {
            var node = new RBTreeNode<T>
            {
                Key = val
            };
            Insert(node, Comparer<T>.Default);
        }
        public virtual void Insert(T newVal, IComparer<T> comparer)
        {
            var node = new RBTreeNode<T>
            {
                Key = newVal
            };
            Insert(node, comparer);
        }
        public void Insert(RBTreeNode<T> newNode)
        {
            Insert(newNode, Comparer<T>.Default);
        }
        public void Insert(RBTreeNode<T> newNode, IComparer<T> comparer)
        {
            var parent = RBTreeNode<T>.Nil;
            var temp = _root;
            // Find the right place for new node
            while (temp != RBTreeNode<T>.Nil)
            {
                parent = temp;
                temp = comparer.Compare(newNode.Key, temp.Key) < 0 ? temp.LeftNode : temp.RightNode;
            }
            newNode.ParentNode = parent;
            if (parent == RBTreeNode<T>.Nil)
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
            newNode.LeftNode = RBTreeNode<T>.Nil;
            newNode.RightNode = RBTreeNode<T>.Nil;
            newNode.Color = RBTreeNode<T>.NodeColor.Red;
            InsertFixUp(newNode);
        }

        // Recolor nodes and perform rotations in order to restore RBT properties.
        protected virtual void InsertFixUp(RBTreeNode<T> inserted)
        {
            var node = inserted;
            // Loop as long as node's parent is red.
            while (node.ParentNode.Color == RBTreeNode<T>.NodeColor.Red)
            {
                // The parent is the left child of grandfather
                // We distinguish this because we have to find the uncle.
                if (node.ParentNode == node.ParentNode.ParentNode.LeftNode)
                {
                    // Find uncle
                    var uncle = node.ParentNode.ParentNode.RightNode;
                    // Case 1: uncle is red.
                    if (uncle.Color == RBTreeNode<T>.NodeColor.Red)
                    {
                        // Then we set parent and uncle to be black and set grandpa red.
                        node.ParentNode.Color = RBTreeNode<T>.NodeColor.Black;
                        uncle.Color = RBTreeNode<T>.NodeColor.Black;
                        node.ParentNode.ParentNode.Color = RBTreeNode<T>.NodeColor.Red;
                        node = node.ParentNode.ParentNode;
                    }
                    // Uncle is black.
                    else
                    {
                        // Case 2
                        if (node == node.ParentNode.RightNode)
                        {
                            // Then we transform the situation into case 3.
                            node = node.ParentNode;
                            LeftRotate(node);
                        }
                        // Case 3
                        node.ParentNode.Color = RBTreeNode<T>.NodeColor.Black;
                        node.ParentNode.ParentNode.Color = RBTreeNode<T>.NodeColor.Red;
                        RightRotate(node.ParentNode.ParentNode);
                    }
                }
                // The parent is the right child of grandfather
                // The rest code is similiar to the if clause. Only exchang right and left
                else
                {
                    var uncle = node.ParentNode.ParentNode.LeftNode;
                    if (uncle.Color == RBTreeNode<T>.NodeColor.Red)
                    {
                        node.ParentNode.Color = RBTreeNode<T>.NodeColor.Black;
                        uncle.Color = RBTreeNode<T>.NodeColor.Black;
                        node.ParentNode.ParentNode.Color = RBTreeNode<T>.NodeColor.Red;
                        node = node.ParentNode.ParentNode;
                    }
                    else
                    {
                        if (node == node.ParentNode.LeftNode)
                        {
                            node = node.ParentNode;
                            RightRotate(node);
                        }
                        node.ParentNode.Color = RBTreeNode<T>.NodeColor.Black;
                        node.ParentNode.ParentNode.Color = RBTreeNode<T>.NodeColor.Red;
                        LeftRotate(node.ParentNode.ParentNode);
                    }
                }
            }
            _root.Color = RBTreeNode<T>.NodeColor.Black;
        }

        // Delete
        // The restoration of RBT properties is in DeleteFixUp function.
        public void Delete(RBTreeNode<T> removed)
        {
            // We set this variable because we care about the color of the node actually removed.
            var actualRemoved = removed;
            var yOldColor = actualRemoved.Color;
            // This replacer is the one who moves to the y's original position.
            RBTreeNode<T> replacer = null;
            // The removed has only one or less children.
            if (removed.LeftNode == RBTreeNode<T>.Nil)
            {
                replacer = removed.RightNode;
                Transplant(removed, removed.RightNode);
            }
            else if (removed.RightNode == RBTreeNode<T>.Nil)
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
            }
            // If the color of the node actually removed is black, then this could violate RBT's properties.
            if (yOldColor == RBTreeNode<T>.NodeColor.Black)
            {
                DeleteFixUp(replacer);
            }
        }
        // Helper function for Deletion: transplant the replacer to the replaced, it won't change references about childeren.
        protected void Transplant(RBTreeNode<T> replaced, RBTreeNode<T> replacer)
        {
            if (replaced.ParentNode == RBTreeNode<T>.Nil)
            {
                _root = replacer;
            }
            else if (replaced == replaced.ParentNode.LeftNode)
            {
                replaced.ParentNode.LeftNode = replacer;
            }
            else
            {
                replaced.ParentNode.RightNode = replacer;
            }
            replacer.ParentNode = replaced.ParentNode;
        }
        // Helper function for deletion: restore the RBT properties.
        protected void DeleteFixUp(RBTreeNode<T> dbBlack)
        {
            // Logically we regard dbBlack has double black color so that it can restore RBT properties 5th.
            while (dbBlack != _root && dbBlack.Color == RBTreeNode<T>.NodeColor.Black)
            {
                if (dbBlack == dbBlack.ParentNode.LeftNode)
                {
                    var sibling = dbBlack.ParentNode.RightNode;
                    // Case 1
                    if (sibling.Color == RBTreeNode<T>.NodeColor.Red)
                    {
                        sibling.Color = RBTreeNode<T>.NodeColor.Black;
                        dbBlack.ParentNode.Color = RBTreeNode<T>.NodeColor.Red;
                        LeftRotate(dbBlack.ParentNode);
                        sibling = dbBlack.ParentNode.RightNode;
                    }
                    // Case 2
                    if (sibling.LeftNode.Color == RBTreeNode<T>.NodeColor.Black && sibling.RightNode.Color == RBTreeNode<T>.NodeColor.Black)
                    {
                        sibling.Color = RBTreeNode<T>.NodeColor.Red;
                        dbBlack = dbBlack.ParentNode;
                    }
                    else
                    {
                        if (sibling.RightNode.Color == RBTreeNode<T>.NodeColor.Black)
                        {
                            sibling.LeftNode.Color = RBTreeNode<T>.NodeColor.Black;
                            sibling.Color = RBTreeNode<T>.NodeColor.Red;
                            RightRotate(sibling);
                            sibling = dbBlack.ParentNode.RightNode;
                        }
                        sibling.Color = dbBlack.ParentNode.Color;
                        dbBlack.ParentNode.Color = RBTreeNode<T>.NodeColor.Black;
                        sibling.RightNode.Color = RBTreeNode<T>.NodeColor.Black;
                        LeftRotate(dbBlack.ParentNode);
                        dbBlack = _root;
                    }
                }
                // Same with "if" part with "right" and "left" exchanged.
                else
                {
                    var w = dbBlack.ParentNode.LeftNode;
                    // Case 1
                    if (w.Color == RBTreeNode<T>.NodeColor.Red)
                    {
                        w.Color = RBTreeNode<T>.NodeColor.Black;
                        dbBlack.ParentNode.Color = RBTreeNode<T>.NodeColor.Red;
                        RightRotate(dbBlack.ParentNode);
                        w = dbBlack.ParentNode.LeftNode;
                    }
                    // Case 2
                    if (w.RightNode.Color == RBTreeNode<T>.NodeColor.Black && w.LeftNode.Color == RBTreeNode<T>.NodeColor.Black)
                    {
                        w.Color = RBTreeNode<T>.NodeColor.Red;
                        dbBlack = dbBlack.ParentNode;
                    }
                    else
                    {
                        if (w.LeftNode.Color == RBTreeNode<T>.NodeColor.Black)
                        {
                            w.RightNode.Color = RBTreeNode<T>.NodeColor.Black;
                            w.Color = RBTreeNode<T>.NodeColor.Red;
                            LeftRotate(w);
                            w = dbBlack.ParentNode.LeftNode;
                        }
                        w.Color = dbBlack.ParentNode.Color;
                        dbBlack.ParentNode.Color = RBTreeNode<T>.NodeColor.Black;
                        w.LeftNode.Color = RBTreeNode<T>.NodeColor.Black;
                        RightRotate(dbBlack.ParentNode);
                        dbBlack = _root;
                    }
                }
            }
            dbBlack.Color = RBTreeNode<T>.NodeColor.Black;
        }


        // Successor and Predecessor
        public RBTreeNode<T> Successor(RBTree<T>.RBTreeNode<T> root)
        {
            // If node has right subtree, then the successor must be the min one of that subtree.
            if (root.RightNode != RBTreeNode<T>.Nil)
            {
                return Minimum(root);
            }
            // This node has no right subtree.
            var parent = root.ParentNode;
            // Skip the parent smaller than node.
            while (parent != RBTreeNode<T>.Nil && root == parent.RightNode)
            {
                root = parent;
                parent = parent.ParentNode;
            }
            return parent;
        }

        // Predecessor: the node with the biggest key smaller than the input node's
        public RBTreeNode<T> Predecessor(RBTree<T>.RBTreeNode<T> root)
        {
            // If node has left subtree, then the successor must be the max one of that subtree.
            if (root.LeftNode != RBTreeNode<T>.Nil)
            {
                return Maximum(root);
            }
            // This node has no right subtree.
            var parent = root.ParentNode;
            // Skip the parent bigger than node.
            while (parent != RBTreeNode<T>.Nil && root == parent.LeftNode)
            {
                root = parent;
                parent = parent.ParentNode;
            }
            return parent;
        }
    }
}