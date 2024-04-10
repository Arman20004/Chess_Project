using Backend.Engines;
using System.Collections.Generic;

namespace Backend.Models.Figures
{
    internal class Figure_Knight : Figure
    {
        public Figure_Knight(bool isComputerFigure, FigureLocation initialLocation)
            : base(FigureCategory.Knight, isComputerFigure, baseWeight: 30, initialLocation)
        {

        }

        public override IEnumerable<FigureMoveOption> GetPossibleMoves(IDispositionProvider disposition)
        {
            foreach (var ent in GetPossibleMovesBase(disposition,
                  () => MoveSimulationHelpers.GetMoves_Knight(this.CurrentLocation),
                  null, false))
            {
                yield return ent;
            }

        }
    }
}
