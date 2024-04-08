using Backend.Engines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Models.Figures
{
    internal class Figure_Queen : Figure
    {
        public Figure_Queen(bool isComputerFigure, FigureLocation initialLocation) 
            : base(FigureCategory.Queen, isComputerFigure, baseWeight: 90, initialLocation)
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
