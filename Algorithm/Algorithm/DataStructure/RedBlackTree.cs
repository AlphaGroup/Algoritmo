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
            var node = new RedBlackTreeNode<T>()
            {
                Key = val,
            };
            Insert(node);
        }
        // This overloaded function only assumes that the new node contains key.
        public void Insert(RedBlackTreeNode<T> newNode)
        {
            Insert(newNode,Comparer<T>.Default);
        }
        public void Insert(RedBlackTreeNode<T> newNode,IComparer<T> comparer)
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
            InsertFixUp();
        }

        // Recolor nodes and perform rotations in order to restore RBT properties.
        private void InsertFixUp()
        {
            
        }

        public class RedBlackTreeNode<TP>
        {
            // The nil node representing leaves
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