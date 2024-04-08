using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Models
{
    internal class FigureMoveDescriptor : ICloneable
    {
        private readonly Figure _sourceFigure;
        private readonly Figure _opponentFigure; // null for regulat moves or figure to eat

        private readonly FigureLocation _moveToLocation;

        public FigureMoveDescriptor(IDispositionProvider disposition,FigureMoveOption move)
        {
            _sourceFigure = move.SourceFigure;
            _moveToLocation = move.MoveToLocation;

            if (move.TerminationReason == MovementTerminationReason.ReachedOpponentsFigure)
            {
                _opponentFigure = disposition.GetFigureAtLocation(move.MoveToLocation);
            }

        }
        public FigureMoveDescriptor(Figure sourceFigure, FigureLocation moveToLocation)
        {
            _sourceFigure = sourceFigure;
            _moveToLocation = moveToLocation;
            _opponentFigure = null;

        }

        public FigureMoveDescriptor(Figure sourceFigure, Figure opponentFigure)
        {
            _sourceFigure = sourceFigure;
            _moveToLocation = opponentFigure.CurrentLocation;
            _opponentFigure = opponentFigure;

        }
 
        public FigureLocation MoveToLocation => _moveToLocation;
        public Figure SourceFigure => _sourceFigure;
        
        public Figure OpponentFigure => _opponentFigure;

        public object Clone()
        {
            if (_opponentFigure == null)
            {
                return new FigureMoveDescriptor((Figure)this.SourceFigure.Clone(), 
                                   (FigureLocation)this.MoveToLocation.Clone()  );
            }
            else
            {
                return new FigureMoveDescriptor((Figure)this.SourceFigure.Clone(),
                                   (Figure)this.OpponentFigure.Clone());
            }
        }
    }
}
