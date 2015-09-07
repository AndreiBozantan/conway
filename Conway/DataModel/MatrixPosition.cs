namespace Conway.DataModel
{
    public struct MatrixPosition
    {
        public MatrixPosition(int row, int col)
        {
            this.Row = row;
            this.Col = col;
        }

        public int Row;
        public int Col;
    }
}