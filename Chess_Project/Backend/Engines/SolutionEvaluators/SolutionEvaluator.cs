﻿using Backend.Models;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Engines.SolutionEvaluators
{
    internal abstract class SolutionEvaluator
    {
        protected readonly SolutionCategory _category;

        public SolutionEvaluator(SolutionCategory category)
        {
            _category = category;
        }
        public abstract FigureMoveOption Evaluate(IDispositionProvider disposition);

        public IEnumerable<Figure> GetComputerFigures(IDispositionProvider disposition)
        {
            return disposition.ActiveFigures.Where(x => x.IsComputerFigure);
        }

        protected bool IsComputerFigureAtHit(FigureLocation figureLocation, IDispositionProvider disposition)
        {
            foreach (var fgr in disposition.ActiveFigures.Where(x => !x.IsComputerFigure))
            {

                FigureMoveOption hitFigureOpt = fgr.GetPossibleMoves(disposition)
                                         .FirstOrDefault(x => x.TerminationReason == MovementTerminationReason.ReachedOpponentsFigure
                                                        && x.MoveToLocation.Equals(figureLocation));

                if (hitFigureOpt != null) return true;
            }

            return false;
        }
    }
}
