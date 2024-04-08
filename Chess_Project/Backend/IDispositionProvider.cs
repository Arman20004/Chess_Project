using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend
{
    internal interface IDispositionProvider
    {

        void ApplyMove(FigureMoveDescriptor move);


        Figure GetFigureAtLocation(FigureLocation location); 

        IEnumerable<Figure> ActiveFigures { get; }
    }
}
