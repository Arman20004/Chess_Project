using Backend.Models;
using Backend.Models.Solution;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Engines.SolutionEvaluators
{
    internal class SolutionEvaluator_FirstPossible : SolutionEvaluator
    {
        public SolutionEvaluator_FirstPossible()
            : base(SolutionCategory.None)
        {

        }


        public override FigureMoveOption Evaluate(IDispositionProvider disposition)
        {

            List<FigureMoveOption> moves = new List<FigureMoveOption>();

            foreach (var figure in GetComputerFigures(disposition))
            {
                var option = figure.GetPossibleMoves(disposition).FirstOrDefault();

                if (option != null)
                {
                    option.SetMoveRank(new MoveRankDescriptor(_category, 0));
                    return option;
                }
            }

            return null;

        }
    }
}
