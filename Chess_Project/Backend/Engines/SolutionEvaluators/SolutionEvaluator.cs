using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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
    }
}
