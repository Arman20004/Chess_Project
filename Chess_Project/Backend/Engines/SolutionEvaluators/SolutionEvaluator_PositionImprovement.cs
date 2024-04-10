using Backend.Models;
using Backend.Models.Solution;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Engines.SolutionEvaluators
{
    internal class SolutionEvaluator_PositionImprovement : SolutionEvaluator
    {
        public SolutionEvaluator_PositionImprovement()
            : base(SolutionCategory.PositionImprovement)
        {

        }


        public override FigureMoveOption Evaluate(IDispositionProvider disposition)
        {
            DispositionSimulator simulator = new DispositionSimulator(disposition);

            List<FigureMoveOption> moves = new List<FigureMoveOption>();

            foreach (var figure in GetComputerFigures(disposition))
            {
                var options = figure.GetPossibleMoves(disposition)
                            .Where(x => x.TerminationReason == MovementTerminationReason.None);

                if (options == null || !options.Any()) continue;

                foreach (var move in options)
                {

                    simulator.ApplyMove(new FigureMoveDescriptor(simulator, move));

                    if (!IsComputerFigureAtHit(move.MoveToLocation, simulator))
                    {
                        move.SetMoveRank(new MoveRankDescriptor(_category, Evaluate_FiguresHitFieldsCount(simulator)));
                        moves.Add(move);
                    }

                    simulator.RevertLastMove(move.SourceFigure);
                }
            }

            if (moves.Count() < 1) return null;

            return moves.Max();

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
                    sum += GetNormalizedHitFieldsWeight(options.Count(), figure);
                }
            }

            return sum;
        }

        private int GetNormalizedHitFieldsWeight(int optionsCount, Figure figure)
        {
            return optionsCount;
            /*
            if (figure.Category == FigureCategory.Pawn || figure.Category == FigureCategory.King)
                return optionsCount;

            return (int) (optionsCount * 100 / (decimal)Math.Abs(figure.Weight));
            */

        }
    }
}
