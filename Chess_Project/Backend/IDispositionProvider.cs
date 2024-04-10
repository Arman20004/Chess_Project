using Backend.Models;
using System.Collections.Generic;

namespace Backend
{
    internal interface IDispositionProvider
    {

        void ApplyMove(FigureMoveDescriptor move);


        Figure GetFigureAtLocation(FigureLocation location);

        IEnumerable<Figure> ActiveFigures { get; }
    }
}
