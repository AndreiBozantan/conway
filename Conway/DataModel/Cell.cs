using System.Diagnostics;

namespace Conway.DataModel
{
    public interface ICell
    {
        bool IsAlive { get;}
        bool IsNew { get; }
        bool IsAliveOrNew { get; }
        int IntValue { get; }
    }

    /// <summary>
    /// <para>represents a Cell in Conway's Game of Life</para>
    /// <para>it holds a bool state, which is alive or dead</para>
    /// <para>separately it also holds a new state, modified during an update operation</para>
    /// <para>the new state is saved as the current state when the Commit method is invoked</para>
    /// </summary>
    public class Cell : ICell
    {
        public Cell()
        {
            _curState = false;
            _newState = true;
        }

        public bool IsAlive
        {
            get
            {
                return _curState;
            }
        }

        public bool IsNew
        {
            get
            {
                return _newState && !_curState;
            }
        }

        public bool IsAliveOrNew
        {
            get
            {
                return _newState || _curState;
            }
        }

        public int IntValue
        {
            get
            {
                return _curState ? 1 : 0;
            }
        }

        public void Kill()
        {
            Debug.Assert(_curState == true);
            _newState = false;
        }

        public void Resurect()
        {
            Debug.Assert(_curState == false);
            _newState = true;
        }

        public void Commit()
        {
            _curState = _newState;
        }

        private bool _curState;
        private bool _newState;
    }

}
