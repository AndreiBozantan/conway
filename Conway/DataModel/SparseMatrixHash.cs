using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Conway.DataModel
{
    public class SparseMatrixHash<T> : Dictionary<MatrixPosition, T>, ISparseMatrix<T>
    {
        public bool IsConcurrent
        {
            get
            {
                return false;
            }
        }

    }

    public class ConcurrentSparseMatrixHash<T> : ConcurrentDictionary<MatrixPosition, T>, ISparseMatrix<T>
    {
        public bool Remove(MatrixPosition key)
        {
            lock (this)
            {
                T value;
                return base.TryRemove(key, out value);
            }
        }

        public bool IsConcurrent
        {
            get
            {
                return true;
            }
        }

    }
}
