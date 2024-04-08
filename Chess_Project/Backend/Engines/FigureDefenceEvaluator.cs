using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Engines
{
    internal class FigureDefenceEvaluator
    {
        private readonly Dictionary<Figure, FigureDefenceDescriptor> _dict;

        public FigureDefenceEvaluator()
        {
            _dict = new Dictionary<Figure, FigureDefenceDescriptor>();
        }

        public void Reset()
        {
            _dict.Clear();  
        }
        public FigureDefenceDescriptor ResolveDefenceRank(Figure figure, IDispositionProvider currentDisposition)
        {
            FigureDefenceDescriptor result = null;
            if (!_dict.TryGetValue(figure, out result))
            {
                result = CalculateDefenceRank(figure, currentDisposition);
                _dict[figure] = result;
            }

            return result;
        }

        private FigureDefenceDescriptor CalculateDefenceRank(Figure figure, IDispositionProvider currentDisposition)
        {
            foreach (var fgr in currentDisposition.ActiveFigures.Where(x => !x.IsComputerFigure))
            {
                if (fgr == figure) continue;

                FigureMoveOption defendingFigureOpt = fgr.GetPossibleMoves(currentDisposition)
                                         .FirstOrDefault(x => x.TerminationReason == MovementTerminationReason.ReachedSelfFigure
                                                        && x.MoveToLocation.Equals(figure.CurrentLocation));

                if (defendingFigureOpt != null)
                {
                    return new FigureDefenceDescriptor(1);
                    // in current version we do not implelement full check
                }
            }
            // no one figure defendes given figure
            return new FigureDefenceDescriptor(0);
        }
    }
}
