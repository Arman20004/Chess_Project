using Backend.Engines;
using System.Collections.Generic;

namespace Backend.Models.Figures
{
    internal class Figure_Rook : Figure
    {
        public Figure_Rook(bool isComputerFigure, FigureLocation initialLocation)
            : base(FigureCategory.Rook, isComputerFigure, baseWeight: 50, initialLocation)
        {
        }

        public override IEnumerable<FigureMoveOption> GetPossibleMoves(IDispositionProvider disposition)
        {
            foreach (var ent in GetPossibleMovesBase(disposition,
                    () => MoveSimulationHelpers.GetMoves_Horizontal(this.CurrentLocation, MoveDirectionHorizontal.Left)))
            {
                yield return ent;
            }

            foreach (var ent in GetPossibleMovesBase(disposition,
                () => MoveSimulationHelpers.GetMoves_Horizontal(this.CurrentLocation, MoveDirectionHorizontal.Right)))
            {
                yield return ent;
            }

            foreach (var ent in GetPossibleMovesBase(disposition,
                    () => MoveSimulationHelpers.GetMoves_Vertical(this.CurrentLocation, MoveDirectionVertical.Down)))
            {
                yield return ent;
            }

            foreach (var ent in GetPossibleMovesBase(disposition,
                () => MoveSimulationHelpers.GetMoves_Vertical(this.CurrentLocation, MoveDirectionVertical.Up)))
            {
                yield return ent;
            }
        }
    }
}
