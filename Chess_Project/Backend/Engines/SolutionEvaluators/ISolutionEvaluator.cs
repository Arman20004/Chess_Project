using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Engines.SolutionEvaluators
{
    internal interface ISolutionEvaluator
    {
        FigureMoveOption Evaluate();
    }
}
