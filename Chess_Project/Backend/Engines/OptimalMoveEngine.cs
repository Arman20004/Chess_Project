using Backend.Engines.SolutionEvaluators;
using Backend.Models;
using Backend.Models.Solution;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Engines
{
    internal class OptimalMoveEngine
    {
        private readonly SolutionEvaluator[] _evaluators;

        public OptimalMoveEngine()
        {
            _evaluators = new SolutionEvaluator[]
            {
                new SolutionEvaluator_MaterialImprovement(),
                new SolutionEvaluator_MaterialLossAvoidance(),
                new SolutionEvaluator_PositionImprovement(),
                new SolutionEvaluator_FirstPossible()
            };
        }
        public FigureMoveOption GetOptimalMove(IDispositionProvider currentDisposition)
        {
            FigureMoveOption optimalMove = null;

            foreach (var eval in _evaluators)
            {
                optimalMove= eval.Evaluate(currentDisposition);
                if(optimalMove != null)
                {
                    Trace.WriteLine(optimalMove);
                    return optimalMove;
                }
            }

            return null;
        }

      

        private MoveRankDescriptor Evaluate_Mate()
        {
            return null;
        }
    }
}
