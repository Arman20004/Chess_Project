using Backend.Engines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

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
