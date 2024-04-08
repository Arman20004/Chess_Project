using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Engines
{
    internal static class DispositionFactory
    {
        public static IDispositionProvider GetDispositionInitialGame()
        {
            List<Figure> figures = new List<Figure>();
            figures.AddRange(new Figure[]
            {
                //Rook positions

                FiguresFactory.CreateFigure(FigureCategory.Rook, true,  0, 0),
                FiguresFactory.CreateFigure(FigureCategory.Rook, true,  7, 0),
                FiguresFactory.CreateFigure(FigureCategory.Rook, false, 0, 7),
                FiguresFactory.CreateFigure(FigureCategory.Rook, false, 7, 7),

                //Knight positions

                FiguresFactory.CreateFigure(FigureCategory.Knight, true, 1, 0),
                FiguresFactory.CreateFigure(FigureCategory.Knight, true, 6, 0),
                FiguresFactory.CreateFigure(FigureCategory.Knight, false, 1, 7),
                FiguresFactory.CreateFigure(FigureCategory.Knight, false, 6, 7),

                //Bishop positions

                FiguresFactory.CreateFigure(FigureCategory.Bishop, true, 2, 0),
                FiguresFactory.CreateFigure(FigureCategory.Bishop, true, 5, 0),
                FiguresFactory.CreateFigure(FigureCategory.Bishop, false, 2, 7),
                FiguresFactory.CreateFigure(FigureCategory.Bishop, false, 5, 7),

                //Queen postions

                FiguresFactory.CreateFigure(FigureCategory.Queen, true, 3, 0),
                FiguresFactory.CreateFigure(FigureCategory.Queen, false, 3, 7),

                //King postions

                FiguresFactory.CreateFigure(FigureCategory.King, true, 4, 0),
                FiguresFactory.CreateFigure(FigureCategory.King, false, 4, 7),

              

            });

          
            for (int i = 0; i < State.GameSetup.BoardSize; i++)
            {
                //Black Pawn positions
                figures.Add(FiguresFactory.CreateFigure(FigureCategory.Pawn, !State.GameSetup.BoardFlipped, i, 1));
                
                //White Pawn positions
                figures.Add(FiguresFactory.CreateFigure(FigureCategory.Pawn, State.GameSetup.BoardFlipped, i, 6)); 
            }
           
            return new Disposition(figures);
        }
    }
}
