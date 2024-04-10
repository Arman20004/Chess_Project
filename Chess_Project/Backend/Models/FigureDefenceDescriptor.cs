namespace Backend.Models
{
    internal class FigureDefenceDescriptor
    {
        private readonly int _defenceRank;
        public FigureDefenceDescriptor(int rank)
        {
            _defenceRank = rank;
        }
        public int DefenceRank => _defenceRank;

        public bool IsDefended => _defenceRank > 0;
    }
}
