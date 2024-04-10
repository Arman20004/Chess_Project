using Backend.Models;
using System.Windows.Controls;

namespace Chess_Project.UiModel
{
    internal class UiFigure
    {
        private readonly Figure _figure;
        private readonly Image _image;

        public UiFigure(Figure figure, Image image)
        {
            _figure = figure;
            _image = image;
        }

        public Figure SourceFigure => _figure;

        public Image SourceImage => _image;

    }
}
