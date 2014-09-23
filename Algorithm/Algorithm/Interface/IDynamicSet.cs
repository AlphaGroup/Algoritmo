using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Interface
{
    interface IDynamicSet<TKey, TNode>
    {
        // Min and Max
        TKey Minimum();
        TNode Minimum(TNode root);
        TKey Maximum();
        TNode Maximum(TNode root);
        // Search
        TNode Search(TKey key);
        TNode Search(TKey key, IComparer<TKey> comparer);
        TNode Search(TNode root, TKey key);
        TNode Search(TNode root, TKey key, IComparer<TKey> comparer);
        // Successor and Predeccessor
        TNode Successor(TNode root);
        TNode Predecessor(TNode root);
        // Insert
        void Insert(TKey newVal);
        void Insert(TKey newVal, IComparer<TKey> comparer);
        //void Insert(TNode newNode);
        //void Insert(TNode newNode, IComparer<TKey> comparer);
        // Delete
        void Delete(TNode node);
    }
}
