using Backend.Engines;
using System.Collections.Generic;

namespace Backend.Models.Figures
{
    internal class Figure_Bishop : Figure
    {
        public Figure_Bishop(bool isComputerFigure, FigureLocation initialLocation) :
            base(FigureCategory.Bishop, isComputerFigure, baseWeight: 33, initialLocation)
        {

        }


        public override IEnumerable<FigureMoveOption> GetPossibleMoves(IDispositionProvider disposition)
        {

            foreach (var ent in GetPossibleMovesBase(disposition,
                    () => MoveSimulationHelpers.GetMoves_Diagonal(this.CurrentLocation, MoveDirectionHorizontal.Left, MoveDirectionVertical.Up)))
            {
                yield return ent;
            }

            foreach (var ent in GetPossibleMovesBase(disposition,
                () => MoveSimulationHelpers.GetMoves_Diagonal(this.CurrentLocation, MoveDirectionHorizontal.Right, MoveDirectionVertical.Up)))
            {
                yield return ent;
            }

            foreach (var ent in GetPossibleMovesBase(disposition,
                    () => MoveSimulationHelpers.GetMoves_Diagonal(this.CurrentLocation, MoveDirectionHorizontal.Left, MoveDirectionVertical.Down)))
            {
                yield return ent;
            }

            foreach (var ent in GetPossibleMovesBase(disposition,
                () => MoveSimulationHelpers.GetMoves_Diagonal(this.CurrentLocation, MoveDirectionHorizontal.Right, MoveDirectionVertical.Down)))
            {
                yield return ent;
            }
        }
    }
}
