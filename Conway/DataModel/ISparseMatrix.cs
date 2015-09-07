using System.Collections.Generic;

namespace Conway.DataModel
{
    public interface ISparseMatrix<T> : IEnumerable<KeyValuePair<MatrixPosition, T>>
    {
        T this[MatrixPosition key] { get; set; }
        bool Remove(MatrixPosition key);
        bool TryGetValue(MatrixPosition key, out T value);
        bool IsConcurrent { get; }
    }
}
