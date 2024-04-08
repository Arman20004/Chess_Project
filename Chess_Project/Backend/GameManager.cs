using Backend.Engines;
using Backend.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace Backend
{
    internal class GameManager
    {
        private readonly IDispositionProvider _disposition;
        private readonly OptimalMoveEngine _optimalMoveEngine;
        public GameManager(IDispositionProvider disposition)
        {
            _disposition = disposition;
            _optimalMoveEngine= new OptimalMoveEngine();    
        }

        public GameManager()
            :this(DispositionFactory.GetDispositionInitialGame())
        {
            
        }

        public FigureMoveDescriptor DoComputerMove()
        {
            var computerMove = _optimalMoveEngine.GetOptimalMove(_disposition);
            if (computerMove == null)
            {
                Trace.WriteLine("Could not do any move");
                return null;
            }

            FigureMoveDescriptor move = new FigureMoveDescriptor(_disposition, computerMove);

            _disposition.ApplyMove(new FigureMoveDescriptor(_disposition, computerMove));

            return move;    
        }

        public void RegisterUserMove(FigureMoveDescriptor userMove)
        {
            _disposition.ApplyMove(userMove);
            
        }

        public IEnumerable<Figure> GetFigures()
        {
            return _disposition.ActiveFigures;
        }

        public Figure GetFigureAtLocaion(FigureLocation location)
        {
            return _disposition.GetFigureAtLocation(location);
        }
    }
}
