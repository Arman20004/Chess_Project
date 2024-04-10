using Backend.Models;

namespace Backend.Engines.SolutionEvaluators
{
    internal interface ISolutionEvaluator
    {
        FigureMoveOption Evaluate();
    }
}
