using Backend.Models;
using System.Collections.Generic;

namespace Backend.Engines
{
    internal class DispositionSimulator : IDispositionProvider
    {
        private readonly List<Figure> _figures = new List<Figure>();
        private readonly Figure[,] _figuresPlacement = new Figure[8, 8];

        private FigureMoveDescriptor _previousMove;
        private Figure _previousFigure;

        public DispositionSimulator(IDispositionProvider originalDisposition)
        {
            _figures = new List<Figure>(originalDisposition.ActiveFigures);

            foreach (var figure in _figures)
            {
                SetFigureAtLocation(figure, figure.CurrentLocation);
            }
        }


        public void ApplyMove(FigureMoveDescriptor move)
        {
            _previousMove = (FigureMoveDescriptor)move.Clone();

            if (move.OpponentFigure != null)
            {
                _previousFigure = this.GetFigureAtLocation(move.OpponentFigure.CurrentLocation);
            }

            ResetFigureAtLocation(move.SourceFigure, move.SourceFigure.CurrentLocation);
            SetFigureAtLocation(move.SourceFigure, move.MoveToLocation);

            move.SourceFigure.SetCurrentLocation(move.MoveToLocation);
            if (move.OpponentFigure != null)
            {
                _figures.Remove(move.OpponentFigure);
            }

        }

        private void SetFigureAtLocation(Figure figure, FigureLocation location)
        {
            _figuresPlacement[location.HorizontalPosition, location.VerticalPosition] = figure;
        }

        private void ResetFigureAtLocation(Figure figure, FigureLocation location)
        {
            _figuresPlacement[location.HorizontalPosition, location.VerticalPosition] = null;
        }

        public Figure GetFigureAtLocation(FigureLocation location)
        {
            return _figuresPlacement[location.HorizontalPosition, location.VerticalPosition];
        }

        public IEnumerable<Figure> ActiveFigures => _figures;

        public void RevertLastMove(Figure figure)
        {
            if (_previousMove == null)
                return;

            // action opposite to apply move
            if (_previousMove.OpponentFigure == null)
            {
                ResetFigureAtLocation(figure, _previousMove.MoveToLocation);
                SetFigureAtLocation(figure, _previousMove.SourceFigure.CurrentLocation);
                figure.SetCurrentLocation(_previousMove.SourceFigure.CurrentLocation);
            }
            else
            {
                // TODO : implement
                SetFigureAtLocation(figure, _previousMove.SourceFigure.CurrentLocation);
                // run on correct figure to restore
                SetFigureAtLocation(_previousFigure, _previousMove.OpponentFigure.CurrentLocation);

                figure.SetCurrentLocation(_previousMove.SourceFigure.CurrentLocation);
                _previousFigure.SetCurrentLocation(_previousMove.OpponentFigure.CurrentLocation);

                _figures.Add(_previousFigure);
            }

            _previousMove = null;

        }
    }
}
