using Backend;
using Backend.Models;
using Chess_Project.UiModel;
using System.Windows;

namespace Chess_Project.UiEngine
{
    internal class UserMoveTracker
    {
        private readonly DisplayCoordinateMapper _coordinateMapper;
        private readonly GameManager _gameManager;

        private UiFigure _trackingFigure;
        private Point _clickPointOffset;
        public UserMoveTracker(DisplayCoordinateMapper coordinateMapper, GameManager gameManager)
        {
            _coordinateMapper = coordinateMapper;
            _gameManager = gameManager;
        }

        public UiFigure TrackingFigure => _trackingFigure;

        public Point GetClickPointAdjusted(Point point)
        {
            return new Point(point.X - _clickPointOffset.X, point.Y - _clickPointOffset.Y);
        }
        public void StartTracking(UiFigure figure, Point initialClickPointOffset)
        {
            _trackingFigure = figure;
            _clickPointOffset = new Point(initialClickPointOffset.X % _coordinateMapper.CellWidth,
                                          initialClickPointOffset.Y % _coordinateMapper.CellHeigth);
        }

        public void ResetTracking()
        {
            _trackingFigure = null;
        }
        public FigureMoveDescriptor EndTracking(Point point)
        {
            if (_trackingFigure == null)
                return null; // impossible, to be called after start tarcking

            FigureLocation loc = _coordinateMapper.MapDisplayCoordinateToBoardSquare(point);
            if (loc == null) { return null; } // click outside our board borders


            Figure figure = _gameManager.GetFigureAtLocaion(loc);
            if (figure != null && !figure.IsComputerFigure) { return null; }// user clicked self figure, not allowed


            return figure == null ? new FigureMoveDescriptor(_trackingFigure.SourceFigure, loc)
                                    : new FigureMoveDescriptor(_trackingFigure.SourceFigure, figure);

        }



    }
}
