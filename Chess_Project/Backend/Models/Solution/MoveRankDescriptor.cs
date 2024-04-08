using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Models.Solution
{
    internal class MoveRankDescriptor
    {
        public SolutionCategory Category { get; private set; }

        public int Rank { get; private set; }

        public MoveRankDescriptor(SolutionCategory category, int rank)
        {
            Category = category;
            Rank = rank;    
        }

        public override string ToString()
        {
            if(Category >= SolutionCategory.MaterialImprovement)
            {
                return $"{Category}: Rank={Rank}";
            }
            else
            {
                return $"{Category}";
            }
            
        }
    }
}
