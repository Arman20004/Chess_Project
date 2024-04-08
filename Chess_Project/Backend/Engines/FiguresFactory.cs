using Backend.Models;
using Backend.Models.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Engines
{
    internal class FiguresFactory
    {
        private static Dictionary<FigureCategory, Func<bool, FigureLocation, Figure>> _figureCreators
            = new Dictionary<FigureCategory, Func<bool, FigureLocation, Figure>>()
        {
            {FigureCategory.Pawn,   (isCompFigure, loc)=> new Figure_Pawn   (isCompFigure, loc)},
            {FigureCategory.Knight, (isCompFigure, loc)=> new Figure_Knight (isCompFigure, loc)},
            {FigureCategory.Bishop, (isCompFigure, loc)=> new Figure_Bishop (isCompFigure ,loc)},
            {FigureCategory.Rook,   (isCompFigure, loc)=> new Figure_Rook   (isCompFigure, loc)},
            {FigureCategory.Queen,  (isCompFigure, loc)=> new Figure_Queen  (isCompFigure, loc)},
            {FigureCategory.King,   (isCompFigure, loc)=> new Figure_King   (isCompFigure, loc)}
        };
        public static Figure CreateFigure(FigureCategory category, bool isComputerFigure, int horizontalPosition, int verticalPosition)
        {
            Func < bool, FigureLocation, Figure > crtFunc = _figureCreators[category];
            return crtFunc(isComputerFigure, new FigureLocation(horizontalPosition, verticalPosition));
        }
    }
}
