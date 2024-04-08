using Backend.Engines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Models.Figures
{
    internal class Figure_Pawn : Figure
    {
        private readonly FigureLocation _originalLocation;
        public Figure_Pawn(bool isComputerFigure, FigureLocation initialLocation) :
            base(FigureCategory.Pawn, isComputerFigure, baseWeight: 10, initialLocation)
        {
            _originalLocation = initialLocation;
        }

        

        public override IEnumerable<FigureMoveOption> GetPossibleMoves(IDispositionProvider disposition)
        {
            bool isPawnAtOriginalLocation = _originalLocation.Equals(this.CurrentLocation);

            foreach (var ent in GetPawnPossibleMoves(disposition,
               () => MoveSimulationHelpers.GetMoves_Pawn(disposition, this.IsComputerFigure, this.CurrentLocation, isPawnAtOriginalLocation)))
            {
                yield return ent;
            }
        }


        private IEnumerable<FigureMoveOption> GetPawnPossibleMoves(IDispositionProvider disposition, Func<IEnumerable<FigureLocation>> navigatorFunc)
        {
            Figure otherFigure = null;

            foreach (var move in navigatorFunc())
            {
                otherFigure = disposition.GetFigureAtLocation(move);
                if (otherFigure == null)
                {
                    yield return new FigureMoveOption(this, move, MovementTerminationReason.None);
                }
                else
                {
                    FigureMoveOption moveOpt =  new FigureMoveOption(this, move,
                        otherFigure.IsComputerFigure == this.IsComputerFigure ? MovementTerminationReason.ReachedSelfFigure
                        : MovementTerminationReason.ReachedOpponentsFigure);

                    if (!IsValidPawnMove(moveOpt)) continue;
                    // no break for pawn, evaluate all options    
                    yield return moveOpt;
                    
                }
            }
        }

        //to filter out straight eat
        private bool IsValidPawnMove(FigureMoveOption moveOpt)
        {
            if (moveOpt.TerminationReason != MovementTerminationReason.ReachedOpponentsFigure)
                return true;

            //pawn cannot eat straight
            return moveOpt.MoveToLocation.HorizontalPosition != this.CurrentLocation.HorizontalPosition;
            

            
        }
    }
}
