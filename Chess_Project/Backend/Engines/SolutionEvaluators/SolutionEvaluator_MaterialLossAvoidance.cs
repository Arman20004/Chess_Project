using Backend.Models;
using Backend.Models.Solution;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Engines.SolutionEvaluators
{
    internal class SolutionEvaluator_MaterialLossAvoidance : SolutionEvaluator
    {
        public SolutionEvaluator_MaterialLossAvoidance()
            : base(SolutionCategory.MaterialLossAvoidance)
        {

        }

        public override FigureMoveOption Evaluate(IDispositionProvider disposition)
        {
            DispositionSimulator simulator = new DispositionSimulator(disposition);

            List<FigureMoveOption> moves = new List<FigureMoveOption>();

            foreach (var figure in GetComputerFigures(disposition).OrderByDescending(x => Math.Abs(x.Weight)))
            {
                if (!IsComputerFigureAtHit(figure.CurrentLocation, disposition)) continue;

                var options = figure.GetPossibleMoves(disposition)
                          .Where(x => x.TerminationReason == MovementTerminationReason.None);

                if (options == null || !options.Any()) continue;

                foreach (var move in options)
                {
                    simulator.ApplyMove(new FigureMoveDescriptor(simulator, move));
                    if (!IsComputerFigureAtHit(figure.CurrentLocation, simulator))
                    {
                        // we can safe the figure
                        move.SetMoveRank(new MoveRankDescriptor(_category, Math.Abs(figure.Weight)));
                        return move;
                    }

                    simulator.RevertLastMove(move.SourceFigure);
                }
            }



            return null;

        }




        private int Evaluate_FiguresHitFieldsCount(IDispositionProvider disposition)
        {
            int sum = 0;
            foreach (var figure in GetComputerFigures(disposition))
            {
                var options = figure.GetPossibleMoves(disposition)
                            .Where(x => x.TerminationReason == MovementTerminationReason.None);

                if (options != null && options.Any())
                {
                    sum += options.Count();
                }
            }

            return sum;
        }
    }
}
