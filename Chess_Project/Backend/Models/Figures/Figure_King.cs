using Backend.Engines;
using System.Collections.Generic;

namespace Backend.Models.Figures
{
    internal class Figure_King : Figure
    {
        public Figure_King(bool isComputerFigure, FigureLocation initialLocation) :
             base(FigureCategory.King, isComputerFigure, baseWeight: 1000, initialLocation)
        {
        }

        public override IEnumerable<FigureMoveOption> GetPossibleMoves(IDispositionProvider disposition)
        {
            foreach (var ent in GetPossibleMovesBase(disposition,
                () => MoveSimulationHelpers.GetMoves_King(this.CurrentLocation),
                null,
                false))
            {
                yield return ent;
            }
        }
    }
}
