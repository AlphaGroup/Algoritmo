/*
 * P309
 * Compared to BST, RBT is balanced in order to guarantee that basic dynamic-set operations take O(lgn) time in worst case.
 */

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

        public class RedBlackTreeNode<TP> : BinarySearchTree<TP>.TreeNode<TP>
        {
            public NodeColor Color { get; set; }
        }

    }
}