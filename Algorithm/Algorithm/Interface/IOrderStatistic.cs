/*
 * This interface abstracts the operations a order statistic set should provide.
 * The definition of a node's rank is: the position at which it would be printed
 * in an inorder walk of the tree.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Interface
{
    interface IOrderStatistic<TKey, TNode>
    {
        // Return the element of the zero-based os'th element.
        TNode Select(int os);
        TNode Select(TNode tmpRoot, int os);
        // Return the rank of the node.
        int Rank(TNode node);
    }
}
