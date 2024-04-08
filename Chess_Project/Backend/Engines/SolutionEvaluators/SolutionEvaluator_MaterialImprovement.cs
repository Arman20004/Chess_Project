using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Engines.SolutionEvaluators
{
    internal  class SolutionEvaluator_MaterialImprovement : SolutionEvaluator
    {
        public SolutionEvaluator_MaterialImprovement()
            :base(SolutionCategory.MaterialImprovement)
        {

        }
                

        public override FigureMoveOption Evaluate(IDispositionProvider disposition)
        {
            FigureDefenceEvaluator defenceEvaluator= new FigureDefenceEvaluator();
            // DispositionSimulator simulator = new DispositionSimulator(disposition);

            List<FigureMoveOption> moves = new List<FigureMoveOption>();

            foreach(var figure in GetComputerFigures(disposition))
            {
                var options = figure.GetPossibleMoves(disposition)
                            .Where(x => x.TerminationReason == MovementTerminationReason.ReachedOpponentsFigure);

                if(options!= null && options.Any())
                {
                    moves.AddRange(options);
                }
            }

            if (moves.Count() < 1) return null;

            FigureDefenceDescriptor defenceRank = null;
            List<FigureMoveOption> goodOptions = new List<FigureMoveOption>();
            foreach (var move in moves)
            {
                Figure opponentFigure = disposition.GetFigureAtLocation(move.MoveToLocation);
                defenceRank= defenceEvaluator.ResolveDefenceRank(opponentFigure, disposition);

                if (defenceRank.IsDefended && 
                    Math.Abs(opponentFigure.Weight) < Math.Abs(move.SourceFigure.Weight))
                    continue;

                move.SetMoveRank(new Models.Solution.MoveRankDescriptor(_category,
                    defenceRank.IsDefended ? Math.Abs(opponentFigure.Weight) - Math.Abs(move.SourceFigure.Weight)  
                                            : Math.Abs(opponentFigure.Weight)
                    ));

                goodOptions.Add(move);

            }

            if(goodOptions.Count() < 1) return null;
            return  goodOptions.Max();
                        
        }
    }
}
