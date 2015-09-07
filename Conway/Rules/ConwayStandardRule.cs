using Conway.DataModel;

namespace Conway.Rules
{
    /// <summary>
    /// implementation of the Conway's game of life  standard rules
    /// </summary>
    public class ConwayStandardRule : IGameRule
    {
        public void UpdateCell(MatrixPosition pos, GameActions gameActions)
        {
            int n = gameActions.GetNeighboursCount(pos);
            if (n != 2 && n != 3)
            {
                // kill if alive
                gameActions.KillCell(pos);
            }
            else if (n == 3)
            {
                // reproduce - if not alive
                gameActions.CreateCell(pos);
            }
        }
    }
}
 