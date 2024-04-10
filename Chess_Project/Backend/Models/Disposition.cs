using System.Collections.Generic;

namespace Backend.Models
{
    internal class Disposition : IDispositionProvider
    {
        private readonly List<Figure> _figures = new List<Figure>();
        private readonly Figure[,] _figuresPlacement = new Figure[8, 8];

        public Disposition(IEnumerable<Figure> figuresWithInitialPlacements)
        {
            _figures = new List<Figure>(figuresWithInitialPlacements);

            foreach (var figure in figuresWithInitialPlacements)
            {
                SetFigureAtLocation(figure, figure.CurrentLocation);
            }
        }


        public void ApplyMove(FigureMoveDescriptor move)
        {
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
    }
}
