/*
 * P309
 * Compared to BST, RBT is balanced in order to guarantee that basic dynamic-set operations take O(lgn) time in worst case.
 * The height of a RBTree <= 2lg(n+1)
 */

using System.Collections;
using System.Collections.Generic;

namespace Algorithm.DataStructure
{
    // TODO: Here I want to use interface IDynamicSet and inheritance from BST.
    // TODO: But I have to solve the problem about return value type. Further design is needed.
    internal class RedBlackTree<T>
    {
        public enum NodeColor
        {
            Red,
            Black
        }

        private RedBlackTreeNode<T> _root = RedBlackTreeNode<T>.Nil;

        // Max and min functions
        // Minimum
        public T Minimum()
        {
            return Minimum(_root).Key;
        }
        public RedBlackTreeNode<T> Minimum(RedBlackTreeNode<T> node)
        {
            while (node.LeftNode != null)
            {
                node = node.LeftNode;
            }
            return node;
        }

        // Maximum
        public T Maximum()
        {
            return Maximum(_root).Key;
        }
        public RedBlackTreeNode<T> Maximum(RedBlackTreeNode<T> node)
        {
            while (node.RightNode != null)
            {
                node = node.RightNode;
            }
            return node;
        }

        // Rotation: both run in O(1)
        // Left rotation assuming that input.RightNode!=Nil and that the root's parent is Nil.
        private void LeftRotate(RedBlackTreeNode<T> node)
        {
            // Set right node
            var rNode = node.RightNode;
            // Move rNode's left subtree to node's right subtree.
            node.RightNode = rNode.LeftNode;
            if (rNode.LeftNode != RedBlackTreeNode<T>.Nil)
            {
                rNode.LeftNode.ParentNode = node;
            }
            // Set rNode's parent.
            rNode.ParentNode = node.ParentNode;
            // Set node's parent node child to rNode
            if (node.ParentNode == RedBlackTreeNode<T>.Nil)
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
        private void RightRotate(RedBlackTreeNode<T> node)
        {
            // Set left node.
            var lNode = node.LeftNode;
            // Move lNode's right subtree to node's left subtree.
            node.LeftNode = lNode.RightNode;
            if (lNode.LeftNode != RedBlackTreeNode<T>.Nil)
            {
                lNode.LeftNode.ParentNode = node;
            }
            // Set lNode's parent.
            lNode.ParentNode = node.ParentNode;
            // Set node's parent node's child to lNode
            if (node.ParentNode == RedBlackTreeNode<T>.Nil)
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
        public void Insert(T val)
        {
            var node = new RedBlackTreeNode<T>
            {
                Key = val,
            };
            Insert(node);
        }
        // This overloaded function only assumes that the new node contains key.
        public void Insert(RedBlackTreeNode<T> newNode)
        {
            Insert(newNode, Comparer<T>.Default);
        }
        public void Insert(RedBlackTreeNode<T> newNode, IComparer<T> comparer)
        {
            var parent = RedBlackTreeNode<T>.Nil;
            var temp = _root;
            // Find the right place for new node
            while (temp != RedBlackTreeNode<T>.Nil)
            {
                parent = temp;
                temp = comparer.Compare(newNode.Key, temp.Key) < 0 ? temp.LeftNode : temp.RightNode;
            }
            newNode.ParentNode = parent;
            if (parent == RedBlackTreeNode<T>.Nil)
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
            newNode.LeftNode = RedBlackTreeNode<T>.Nil;
            newNode.RightNode = RedBlackTreeNode<T>.Nil;
            newNode.Color = NodeColor.Red;
            InsertFixUp(newNode);
        }

        // Recolor nodes and perform rotations in order to restore RBT properties.
        private void InsertFixUp(RedBlackTreeNode<T> inserted)
        {
            var node = inserted;
            // Loop as long as node's parent is red.
            while (node.ParentNode.Color == NodeColor.Red)
            {
                // The parent is the left child of grandfather
                // We distinguish this because we have to find the uncle.
                if (node.ParentNode == node.ParentNode.ParentNode.LeftNode)
                {
                    // Find uncle
                    var uncle = node.ParentNode.ParentNode.RightNode;
                    // Case 1: uncle is red.
                    if (uncle.Color == NodeColor.Red)
                    {
                        // Then we set parent and uncle to be black and set grandpa red.
                        node.ParentNode.Color = NodeColor.Black;
                        uncle.Color = NodeColor.Black;
                        node.ParentNode.ParentNode.Color = NodeColor.Red;
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
                        node.ParentNode.Color = NodeColor.Black;
                        node.ParentNode.ParentNode.Color = NodeColor.Red;
                        RightRotate(node.ParentNode.ParentNode);
                    }
                }
                // The parent is the right child of grandfather
                // The rest code is similiar to the if clause. Only exchang right and left
                else
                {
                    var uncle = node.ParentNode.ParentNode.LeftNode;
                    if (uncle.Color == NodeColor.Red)
                    {
                        node.ParentNode.Color = NodeColor.Black;
                        uncle.Color = NodeColor.Black;
                        node.ParentNode.ParentNode.Color = NodeColor.Red;
                        node = node.ParentNode.ParentNode;
                    }
                    else
                    {
                        if (node == node.ParentNode.LeftNode)
                        {
                            node = node.ParentNode;
                            RightRotate(node);
                        }
                        node.ParentNode.Color = NodeColor.Black;
                        node.ParentNode.ParentNode.Color = NodeColor.Red;
                        LeftRotate(node.ParentNode.ParentNode);
                    }
                }
            }
            _root.Color = NodeColor.Black;
        }

        // Delete
        // This function is the most complicated function of RBT.
        public void Delete(RedBlackTreeNode<T> z)
        {
            // TODO: Analyze and change variables' names
            var y = z;
            var yOldColor = y.Color;
            RedBlackTreeNode<T> x = null;
            if (z.LeftNode == RedBlackTreeNode<T>.Nil)
            {
                x = z.RightNode;
                Transplant(z, z.RightNode);
            }
            else if (z.RightNode == RedBlackTreeNode<T>.Nil)
            {
                x = z.LeftNode;
                Transplant(z, z.LeftNode);
            }
            else
            {
                y = Minimum(z.RightNode);
                yOldColor = y.Color;
                x = y.RightNode;
                if (y.ParentNode == z)
                {
                    x.ParentNode = y;
                }
                else
                {
                    Transplant(y, y.RightNode);
                    y.RightNode = z.RightNode;
                    y.RightNode.ParentNode = y;
                }
                Transplant(z, y);
                y.LeftNode = z.LeftNode;
                y.LeftNode.ParentNode = y;
                y.Color = z.Color;
            }
            if (yOldColor == NodeColor.Black)
            {
                DeleteFixUp(x);
            }
        }
        // Helper functions for Deletion
        private void Transplant(RedBlackTreeNode<T> replaced, RedBlackTreeNode<T> replacer)
        {
            if (replaced.ParentNode == RedBlackTreeNode<T>.Nil)
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

        private void DeleteFixUp(RedBlackTreeNode<T> x)
        {
            // TODO: Analyze and change variables' names.
            while (x != _root && x.Color == NodeColor.Black)
            {
                if (x == x.ParentNode.LeftNode)
                {
                    var w = x.ParentNode.RightNode;
                    // Case 1
                    if (w.Color == NodeColor.Red)
                    {
                        w.Color = NodeColor.Black;
                        x.ParentNode.Color = NodeColor.Red;
                        LeftRotate(x.ParentNode);
                        w = x.ParentNode.RightNode;
                    }
                    // Case 2
                    if (w.LeftNode.Color == NodeColor.Black && w.RightNode.Color == NodeColor.Black)
                    {
                        w.Color = NodeColor.Red;
                        x = x.ParentNode;
                    }
                    else
                    {
                        if (w.RightNode.Color == NodeColor.Black)
                        {
                            w.LeftNode.Color = NodeColor.Black;
                            w.Color = NodeColor.Red;
                            RightRotate(w);
                            w = x.ParentNode.RightNode;
                        }
                        w.Color = x.ParentNode.Color;
                        x.ParentNode.Color = NodeColor.Black;
                        w.RightNode.Color = NodeColor.Black;
                        LeftRotate(x.ParentNode);
                        x = _root;
                    }
                }
                // Same with "if" part with "right" and "left" exchanged.
                else
                {
                    var w = x.ParentNode.LeftNode;
                    // Case 1
                    if (w.Color == NodeColor.Red)
                    {
                        w.Color = NodeColor.Black;
                        x.ParentNode.Color = NodeColor.Red;
                        RightRotate(x.ParentNode);
                        w = x.ParentNode.LeftNode;
                    }
                    // Case 2
                    if (w.RightNode.Color == NodeColor.Black && w.LeftNode.Color == NodeColor.Black)
                    {
                        w.Color = NodeColor.Red;
                        x = x.ParentNode;
                    }
                    else
                    {
                        if (w.LeftNode.Color == NodeColor.Black)
                        {
                            w.RightNode.Color = NodeColor.Black;
                            w.Color = NodeColor.Red;
                            LeftRotate(w);
                            w = x.ParentNode.LeftNode;
                        }
                        w.Color = x.ParentNode.Color;
                        x.ParentNode.Color = NodeColor.Black;
                        w.LeftNode.Color = NodeColor.Black;
                        RightRotate(x.ParentNode);
                        x = _root;
                    }
                }
            }
            x.Color = NodeColor.Black;
        }

        // The class of RBT's node.
        public class RedBlackTreeNode<TP>
        {
            // The nil node representing leaves. Its properties have no meanings except Color.
            public static RedBlackTreeNode<TP> Nil = new RedBlackTreeNode<TP>
            {
                Color = NodeColor.Black,
                ParentNode = null,
                LeftNode = null,
                RightNode = null
            };

            public NodeColor Color { get; set; }
            public RedBlackTreeNode<TP> LeftNode { get; set; }
            public RedBlackTreeNode<TP> RightNode { get; set; }
            public RedBlackTreeNode<TP> ParentNode { get; set; }
            public TP Key { get; set; }
        }
    }
}