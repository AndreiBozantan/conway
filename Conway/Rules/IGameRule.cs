using Conway.DataModel;

namespace Conway.Rules
{
    public interface IGameRule
    {
        void UpdateCell(MatrixPosition pos, GameActions gameActions);
    }
}
