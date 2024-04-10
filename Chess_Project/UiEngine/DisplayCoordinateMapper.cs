using Backend.Models;
using Backend.State;
using System.Windows;

namespace Chess_Project.UiEngine
{
    internal class DisplayCoordinateMapper
    {
        private int _currentWidth;
        private int _currentHeight;

        public DisplayCoordinateMapper(int initialWidth, int initialHeight)
        {
            _currentWidth = initialWidth;
            _currentHeight = initialHeight;
        }

        public void ReSize(int newWidth, int newHeight)
        {
            _currentWidth = newWidth;
            _currentHeight = newHeight;
        }

        public int CellWidth => _currentWidth / GameSetup.BoardSize;
        public int CellHeigth => _currentHeight / GameSetup.BoardSize;
        public FigureLocation MapDisplayCoordinateToBoardSquare(Point point)
        {
            if (point.X < 0 || point.X > _currentWidth)
                return null;

            if (point.Y < 0 || point.Y > _currentHeight)
                return null;

            int horizCellIndex = (int)(point.X / CellWidth);
            int vertCellIndex = (int)(point.Y / CellHeigth);

            if (horizCellIndex >= GameSetup.BoardSize) horizCellIndex = GameSetup.BoardSize - 1;
            if (vertCellIndex >= GameSetup.BoardSize) vertCellIndex = GameSetup.BoardSize - 1;

            return new FigureLocation(horizCellIndex, vertCellIndex);
        }

        public Point GetUpperLeftCornerPointOfBoardSquare(FigureLocation location)
        {
            return new Point(location.HorizontalPosition * CellHeigth, location.VerticalPosition * CellHeigth);
        }
    }
}
