using Backend.Engines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

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
                  () => MoveSimulationHelpers.GetMoves_Knight(this.CurrentLocation)))
            {
                yield return ent;
            }
                                
        }
    }
}
