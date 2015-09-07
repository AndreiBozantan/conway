using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Conway.DataModel;
using Conway.Rules;

namespace Conway
{
    /// <summary>
    /// <para>implements the basic operations required for Conway's Game of Life</para>
    /// <para>this implementation uses a grid which is resized as required, in order to contain all the living cells</para>
    /// <para>the implementation also uses parallel processing if possible</para>
    /// </summary>
    /// <typeparam name="TRule">specifies the type which implements the rules for the game</typeparam>
    /// <typeparam name="TStorage">specifies the data type to be used for data storage</typeparam>
    public class GenericGame<TRule, TStorage>
        where TStorage : ISparseMatrix<Cell>, new()
        where TRule : IGameRule, new()
    {
        #region init and convert public methods
        static GenericGame()
        {
            _deadCell = new Cell();
            _deadCell.Commit();
            _deadCell.Kill();
            _deadCell.Commit();

        }

        public GenericGame()
        {
            _actions = new GameActions
            {
                CreateCell = this.CreateCell,
                KillCell = this.KillCell,
                GetNeighboursCount = this.GetNeighboursCount
            };
            
            _rule = new TRule();
        }

        /// <summary>
        /// initializes or reinitializes the game grid using the provided data
        /// </summary>
        /// <param name="input">grid data in plaintext format http://www.conwaylife.com/wiki/Plaintext </param>
        /// <param name="chAliveCell">the character used to represent alive cells</param>
        /// <param name="chDeadCell">the character used to represent dead cells</param>
        public void InitGrid(string[] input, char chAliveCell = 'O', char chDeadCell = '.')
        {
            _data = new TStorage();
            _totalSize = new GridSize();
            _aliveSize = new GridSize();
            _liveCellsCount = 0;
            _deadCellsCount = 0;

            int row = 0;
            foreach (var line in input)
            {
                // skip comments
                if (line.StartsWith("!"))
                {
                    continue;
                }
                for (int col = 0; col < line.Length; col++)
                {
                    if (line[col] == chAliveCell)
                    {
                        CreateCell(new MatrixPosition(row, col));
                    }
                    else if (line[col] != chDeadCell)
                    {
                        throw new ArgumentException("invalid input charcter at position: " + row.ToString() + ", " + col.ToString());
                    }
                }
                row++;
            }

            CommitCellsState();
        }

        /// <summary>
        /// converts the game grid to plaintext format http://www.conwaylife.com/wiki/Plaintext
        /// </summary>
        /// <param name="chAliveCell">the character used to represent alive cells</param>
        /// <param name="chDeadCell">the character used to represent dead cells</param>
        /// <returns></returns>
        public string ToString(char chAliveCell, char chDeadCell)
        {
            Debug.Assert(_data != null, "Call InitGrid before this!");

            StringBuilder sb = new StringBuilder();
            string lineSep = "";
            for (int row = _totalSize.Top; row <= _totalSize.Bottom; row++)
            {
                sb.Append(lineSep);
                lineSep = Environment.NewLine;
                for (int col = _totalSize.Left; col <= _totalSize.Right; col++)
                {
                    if (this[row, col].IsAlive)
                    {
                        sb.Append(chAliveCell);
                    }
                    else
                    {
                        sb.Append(chDeadCell);
                    }
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// converts the game grid to plaintext format http://www.conwaylife.com/wiki/Plaintext
        /// </summary>
        public override string ToString()
        {
            return ToString('O', '.');
        }
        #endregion

        #region update
        /// <summary>
        /// computes the next state of the game
        /// </summary>
        public void Update()
        {
            Debug.Assert(_data != null, "Call InitGrid before this!");

            // additional rows and columns on the edges are processed in order to create new cells if necessary
            if (_data.IsConcurrent && Environment.ProcessorCount > 1)
            {
                Parallel.For(_aliveSize.Top - 1, _aliveSize.Bottom + 2, row => UpdateRow(row));
            }
            else
            {
                for (int row = _aliveSize.Top - 1; row <= _aliveSize.Bottom + 1; row++)
                {
                    UpdateRow(row);
                }
            }

            _generation++;

            CommitCellsState();

            ClearDeadCells();
        }

        // helper function used to update the cells in one row
        private void UpdateRow(int row)
        {
            for (int col = _aliveSize.Left - 1; col <= _aliveSize.Right + 1; col++)
            {
                _rule.UpdateCell(new MatrixPosition(row, col), _actions);
            }
        }        
        #endregion

        #region cell manipulation utils
        /// <summary>
        /// <para>helper to find cell at specified position</para>
        /// <para>always return a non null value - the actual living cell or a reference to a dead cell</para>
        /// </summary>
        public ICell this[int row, int col]
        {
            get
            {
                Cell cell = null;
                if (_data.TryGetValue(new MatrixPosition(row, col), out cell))
                {
                    return cell;
                }
                return _deadCell;
            }
        }

        /// <summary>
        /// <para>helper to find cell at specified position</para>
        /// <para>always return a non null value - the actual living cell or a reference to a dead cell</para>
        /// </summary>
        private Cell this[MatrixPosition pos]
        {
            get
            {
                Cell cell = null;
                if (_data.TryGetValue(pos, out cell))
                {
                    return cell;
                }
                return _deadCell;
            }
        }
        
        /// <summary>
        /// creates a new cell at specified position
        /// </summary>
        /// <returns>
        /// <para>false, when there is already an living cell at the position</para>
        /// <para>true, when a new cell is created successfuly</para>
        /// </returns>
        private bool CreateCell(MatrixPosition pos)
        {
            Cell cell = null;
            if (_data.TryGetValue(pos, out cell))
            {
                if (cell.IsAlive)
                {
                    return false;
                }

                cell.Resurect();
                _liveCellsCount++;
                _deadCellsCount--;
                return true;
            }

            cell = new Cell();
            _data[pos] = cell;
            _liveCellsCount++;

            return true;
        }

        /// <summary>
        /// kills a cell at the specified position
        /// </summary>
        /// <returns>
        /// <para>false, when a living cell is NOT found at the specified position</para>
        /// <true>true, when a living cell is found at the specified position, and killed</true>
        /// </returns>
        private bool KillCell(MatrixPosition pos)
        {
            Cell cell = this[pos];
            if (cell.IsAlive)
            {
                cell.Kill();
                _deadCellsCount++;
                _liveCellsCount--;
                return true;
            }
            return false;
        }
        
        /// <summary>
        /// computes the number of living neighbours cells for the specified position
        /// </summary>
        private int GetNeighboursCount(MatrixPosition pos)
        {
            pos.Row--; pos.Col--;
            int c1 = this[pos].IntValue;

            pos.Col++;
            int c2 = this[pos].IntValue;

            pos.Col++;
            int c3 = this[pos].IntValue;

            pos.Row++;
            int c4 = this[pos].IntValue;

            pos.Col--; pos.Col--;
            int c5 = this[pos].IntValue;

            pos.Row++;
            int c6 = this[pos].IntValue;

            pos.Col++;
            int c7 = this[pos].IntValue;

            pos.Col++;
            int c8 = this[pos].IntValue;

            int s = c1 + c2 + c3 + c4 + c5 + c6 + c7 + c8;
            return s;
        }
        #endregion

        #region private helper functions
        /// <summary>
        /// after the update is complete, this will enable the new cells and disable the killed cells
        /// it also updates the area of the grid and the area with the live cells
        /// TODO: optimize commit to process only the modified cells
        /// </summary>
        private void CommitCellsState()
        {
            _totalSize.Reset();
            _aliveSize.Reset();

            foreach (var item in _data)
            {
                CommitCell(item.Key, item.Value);
            }
        }

        private void CommitCell(MatrixPosition pos, Cell cell)
        {
            cell.Commit();

            _totalSize.IncludePosition(pos);

            if (cell.IsAlive)
            {
                _aliveSize.IncludePosition(pos);
            }
        }

        private void ClearDeadCells()
        {
            if (_totalSize.Width > 2 * _aliveSize.Width ||
                _totalSize.Height > 2 * _aliveSize.Height)
            {
                var deadCells = FindDeadCells();
                RemoveCells(deadCells);
                _deadCellsCount = 0;
            }
        }

        IEnumerable<MatrixPosition> FindDeadCells()
        {
            if (_data.IsConcurrent && Environment.ProcessorCount > 1)
            {
                var deadCells = new System.Collections.Concurrent.ConcurrentBag<MatrixPosition>();
                Parallel.ForEach(_data, item => { if (!item.Value.IsAliveOrNew) deadCells.Add(item.Key); });
                return deadCells;
            }
            else
            {
                var deadCells = new List<MatrixPosition>(_deadCellsCount);
                foreach (var item in _data)
                {
                    if (!item.Value.IsAliveOrNew)
                    {
                        deadCells.Add(item.Key);
                    }
                }

                return deadCells;
            }
        }

        private void RemoveCells(IEnumerable<MatrixPosition> list)
        {
            if (_data.IsConcurrent && Environment.ProcessorCount > 1)
            {
                Parallel.ForEach(list, item => _data.Remove(item));
            }
            else
            {
                foreach (var item in list)
                {
                    _data.Remove(item);
                }
            }
        }
        #endregion

        #region fields
        public IGridSize GridSize
        {
            get
            {
                return _totalSize;
            }
        }

        // helper object which stores alive cells positions and temporary state during the update
        private TStorage _data;
        private IGameRule _rule;
        private int _liveCellsCount;
        private int _deadCellsCount;
        private int _generation;
        private GridSize _totalSize;
        private GridSize _aliveSize;
        private GameActions _actions;
        private static Cell _deadCell;
        #endregion
    }


    public class Game : GenericGame<ConwayStandardRule, ConcurrentSparseMatrixHash<Cell>>
    //public class Game : GenericGame<ConwayStandardRule, SparseMatrixHash<Cell>>
    {
    }


    public class GameActions
    {
        public Func<MatrixPosition, bool> KillCell;
        public Func<MatrixPosition, bool> CreateCell;
        public Func<MatrixPosition, int> GetNeighboursCount;
    };
}