/*
 * P287
 * Average-time: theta(lgn) for dynamic-set operations
 * Including SEARCH,MINIMUM,MAXIMUM,,PREDECESSOR,SUCCESSOR,INSERT,DELETE
 */

using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;

namespace Algorithm.DataStructure
{
    class BinarySearchTree<T>
    {
        // Start the code about binary tree
        private TreeNode<T> _rootNode = null;

        // Part I: Querying. 

        // Search
        public TreeNode<T> Search(TreeNode<T> root, T key)
        {
            return Search(root, key, Comparer<T>.Default);
        }
        public TreeNode<T> Search(TreeNode<T> root, T key, IComparer<T> comparer)
        {
            while (root != null && comparer.Compare(root.Key, key) != 0)
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

        // Minimum
        public TreeNode<T> Minimum()
        {
            return Minimum(_rootNode);
        }
        public TreeNode<T> Minimum(TreeNode<T> node)
        {
            while (node.LeftNode != null)
            {
                node = node.LeftNode;
            }
            return node;
        }

        // Maximum
        public TreeNode<T> Maximum()
        {
            return Maximum(_rootNode);
        }
        public TreeNode<T> Maximum(TreeNode<T> node)
        {
            while (node.RightNode != null)
            {
                node = node.RightNode;
            }
            return node;
        }

        // Successor: the node with the smallest key greater than the input node's
        public TreeNode<T> Successor(TreeNode<T> node)
        {
            // If node has right subtree, then the successor must be the min one of that subtree.
            if (node.RightNode != null)
            {
                return Minimum(node);
            }
            // This node has no right subtree.
            var parent = node.ParentNode;
            // Skip the parent smaller than node.
            while (parent != null && node == parent.RightNode)
            {
                node = parent;
                parent = parent.ParentNode;
            }
            return parent;
        }

        // Predecessor: the node with the biggest key smaller than the input node's
        public TreeNode<T> Predecessor(TreeNode<T> node)
        {
            // If node has left subtree, then the successor must be the max one of that subtree.
            if (node.LeftNode != null)
            {
                return Maximum(node);
            }
            // This node has no right subtree.
            var parent = node.ParentNode;
            // Skip the parent bigger than node.
            while (parent != null && node == parent.LeftNode)
            {
                node = parent;
                parent = parent.ParentNode;
            }
            return parent;
        }

        // Part II: Insertion and deletion
        // Insertion takes a new node as input
        public void Insert(TreeNode<T> newNode)
        {
            Insert(newNode, Comparer<T>.Default);
        }
        public void Insert(TreeNode<T> newNode, IComparer<T> comparer)
        {
            newNode.ParentNode = null;
            newNode.RightNode = null;
            newNode.LeftNode = null;
            // Find the parent for the new node
            TreeNode<T> parent = null, temp = _rootNode;
            while (temp != null)
            {
                parent = temp;
                if (comparer.Compare(newNode.Key, temp.Key) < 0)
                {
                    temp = temp.LeftNode;
                }
                else
                {
                    temp = temp.RightNode;
                }
            }
            newNode.ParentNode = parent;
            if (parent == null)
            {
                // Tree was empty, use new ndoe as root.
                _rootNode = newNode;
            }
            else if (comparer.Compare(newNode.Key, parent.Key) < 0)
            {
                parent.LeftNode = newNode;
            }
            else
            {
                parent.RightNode = newNode;
            }
        }

        // Deletion
        // Deletion is complicated. There are three basic cases according to the number of its children.
        // P297, we deal with deletion in a different way not according to three cases directly.
        public void Delete(TreeNode<T> deadNode)
        {
            if (deadNode.LeftNode == null)
            {
                // Only has right child
                Transplant(deadNode, deadNode.RightNode);
            }
            else if (deadNode.RightNode == null)
            {
                // Only has left child
                Transplant(deadNode, deadNode.LeftNode);
            }
            else
            {
                // Has both children
                // Find the successor of the deadNode. The successor can't be null because it has both children.
                var succ = Minimum(deadNode.RightNode);
                // The successor isn't a direct child of deadNode. Then its left child must be null.
                if (succ.ParentNode != deadNode)
                {
                    // Replace successor by its right subtree.
                    Transplant(succ, succ.RightNode);
                    // Replace deadNode with succ(about right subtree)
                    succ.RightNode = deadNode.RightNode;
                    succ.RightNode.ParentNode = succ;
                }
                Transplant(deadNode, succ);
                // Replace deadNode with succ(about left subtree)
                succ.LeftNode = deadNode.LeftNode;
                succ.LeftNode.ParentNode = succ;
            }
        }
        // A helper function for Delete
        // It replaces one subtree with another, focusing on relationship between replaced's parent and replacer.
        private void Transplant(TreeNode<T> replaced, TreeNode<T> replacer)
        {
            if (replaced.ParentNode == null)
            {
                _rootNode = replacer;
            }
            else if (replaced == replaced.ParentNode.LeftNode)
            {
                replaced.ParentNode.LeftNode = replacer;
            }
            else
            {
                replaced.ParentNode.RightNode = replacer;
            }
            // Change replacer's parent.
            if (replacer != null)
            {
                replacer.ParentNode = replaced.ParentNode;
            }
        }

        // The class of tree nodes
        public class TreeNode<TP>
        {
            public TreeNode<TP> LeftNode { get; set; }
            public TreeNode<TP> RightNode { get; set; }
            public TreeNode<TP> ParentNode { get; set; }
            public TP Key { get; set; }
        }
    }
}