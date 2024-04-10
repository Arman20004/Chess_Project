using Backend.State;
using Chess_Project.UiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Figure = Backend.Models.Figure;

namespace Chess_Project.UiEngine
{
    internal class UiFiguresManager
    {
        private const string _imageRelativePathPrefix = "/Chess_assets/";
        private readonly List<UiFigure> _figures;
        private readonly Action<Image> _imageEventHandlersAssigner;
        private readonly Grid _chessBoardGrid;
        private readonly Canvas _movementOwnerGrid;
        private readonly DisplayCoordinateMapper _displayCoordinateMapper;
        public UiFiguresManager(DisplayCoordinateMapper displayCoordinateMapper,
            Grid chessBoardGrid, Canvas movementOwnerGrid,
            Action<Image> imageEventHandlersAssigner)
        {
            _displayCoordinateMapper = displayCoordinateMapper;
            _chessBoardGrid = chessBoardGrid;
            _movementOwnerGrid = movementOwnerGrid;
            _imageEventHandlersAssigner = imageEventHandlersAssigner;

            _figures = new List<UiFigure>();
        }

        public void InitializeFigures(IEnumerable<Figure> figures)
        {
            if (figures == null || !figures.Any()) return;

            foreach (var fig in figures)
            {
                UiFigure uiFig = CreateUiFigure(fig);
                _figures.Add(uiFig);
            }
        }

        public UiFigure ResolveUiFigure(Figure figure)
        {
            return _figures.FirstOrDefault(x => x.SourceFigure == figure);
        }

        public void ReDrawMovingFigure(UiFigure figure, Point point)
        {
            Canvas.SetLeft(figure.SourceImage, point.X);
            Canvas.SetTop(figure.SourceImage, point.Y);
            //figure.SourceImage.Margin = new Thickness(point.X, point.Y, 0, 0);
            //  Trace.WriteLine($"( {point.X}, {point.Y} ) ");
        }

        public void RedrawAllFigures()
        {

            foreach (var uiFigure in _figures)
            {
                uiFigure.SourceImage.Width = _displayCoordinateMapper.CellWidth;
                uiFigure.SourceImage.Height = _displayCoordinateMapper.CellHeigth;
                uiFigure.SourceImage.UpdateLayout();

            }
        }
        public void InvalidateFigure(Figure figure)
        {

            UiFigure uiFig = ResolveUiFigure(figure);
            if (uiFig == null) return;

            InvalidateFigure(uiFig);

        }

        public void InvalidateFigure(UiFigure figure)
        {
            Grid.SetColumn(figure.SourceImage, figure.SourceFigure.CurrentLocation.HorizontalPosition);
            Grid.SetRow(figure.SourceImage, figure.SourceFigure.CurrentLocation.VerticalPosition);

        }
        public void BeginFigureMove(UiFigure figure)
        {

            _chessBoardGrid.Children.Remove(figure.SourceImage);
            figure.SourceImage.Width = _displayCoordinateMapper.CellWidth;
            figure.SourceImage.Height = _displayCoordinateMapper.CellHeigth;

            _movementOwnerGrid.Children.Add(figure.SourceImage);

        }

        public void EndFigureMove(UiFigure figure)
        {
            _movementOwnerGrid.Children.Remove(figure.SourceImage);
            Grid.SetColumn(figure.SourceImage, figure.SourceFigure.CurrentLocation.HorizontalPosition);
            Grid.SetRow(figure.SourceImage, figure.SourceFigure.CurrentLocation.VerticalPosition);

            _chessBoardGrid.Children.Add(figure.SourceImage);

        }


        public void DeleteFigure(UiFigure uiFigure)
        {
            _figures.Remove(uiFigure);
            _chessBoardGrid.Children.Remove(uiFigure.SourceImage);
        }

        public void DeleteFigure(Figure figure)
        {
            UiFigure uiFig = ResolveUiFigure(figure);
            if (uiFig != null)
            {
                DeleteFigure(uiFig);
            }
        }

        private UiFigure CreateUiFigure(Figure figure)
        {
            Image imgControl = CreateFigureImage(figure);
            UiFigure uiFigure = new UiFigure(figure, imgControl);
            imgControl.Tag = uiFigure;
            return uiFigure;

        }
        private Image CreateFigureImage(Figure figure)
        {
            Image imgControl = new Image();
            if (!figure.IsComputerFigure)
            {
                _imageEventHandlersAssigner(imgControl);
            }

            BitmapImage imgBitmap = new BitmapImage();
            imgBitmap.BeginInit();
            imgBitmap.UriSource = new Uri(ResolveFigureImageName(figure), UriKind.Relative);
            imgBitmap.EndInit();
            imgControl.Source = imgBitmap;
            //imgControl.Height = 125;
            //imgControl.Width = 125;

            Grid.SetColumn(imgControl, figure.CurrentLocation.HorizontalPosition);
            Grid.SetRow(imgControl, figure.CurrentLocation.VerticalPosition);
            _chessBoardGrid.Children.Add(imgControl);

            return imgControl;
        }

        private string ResolveFigureImageName(Figure figure)
        {
            // bitwise XOR ( for default setup computer plays black) 
            string colorPrefix =
                (figure.IsComputerFigure ^ GameSetup.ComputerPlaysBlack) ?
                "White" : "Black";

            return $"{_imageRelativePathPrefix}{colorPrefix}_{figure.Category}.png";
            // e.g.  White_Bishop.png
        }



    }
}
