namespace Conway.DataModel
{
    /// <summary>
    /// <para>represents the coordinates of a grid (rectangular shape);</para>
    /// <para>right and bottom coordinates are included in the grid size</para>
    /// </summary> 
    public interface IGridSize
    {
        int Top { get; }
        int Left { get; }
        int Bottom { get; }
        int Right { get; }
        int Width { get; }
        int Height { get; }
        int Area { get; }
    }


    public class GridSize : IGridSize
    { 
        public GridSize()
        {
            Reset();
        }

        public int Top { get; private set; }
        public int Left { get; private set; }
        public int Bottom { get; private set; }
        public int Right { get; private set; }

        /// <summary>
        /// computes the area represented by the coordinates
        /// </summary>
        public int Area
        {
            get
            {
                return Width * Height;
            }
        }
        
        public int Width
        {
            get
            {
                if (Right < Left)
                {
                    return 0;
                }
                return (Right - Left + 1);
            }
        }
        
        public int Height
        {
            get
            {
                if (Bottom < Top)
                {
                    return 0;
                }
                return (Bottom - Top + 1);
            }
        }
        /// <summary>
        /// initialize with data which represents an empty region
        /// </summary>
        public void Reset()
        {
            Left = int.MaxValue;
            Right = int.MinValue;
            Top = int.MaxValue;
            Bottom = int.MinValue;
        }

        /// <summary>
        /// updates the coordinates so that the provided position is included in the area
        /// </summary>
        /// <param name="pos"></param>
        public void IncludePosition(MatrixPosition pos)
        {
            if (pos.Row < Top)
            {
                Top = pos.Row;
            }

            if (pos.Col < Left)
            {
                Left = pos.Col;
            }

            if (pos.Row > Bottom)
            {
                Bottom = pos.Row;
            }

            if (pos.Col > Right)
            {
                Right = pos.Col;
            }
        }
    }
}