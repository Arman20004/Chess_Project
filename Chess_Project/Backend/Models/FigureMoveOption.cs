using Backend.Models.Solution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Models
{
    internal class FigureMoveOption : IComparable<FigureMoveOption> 
    {
        private readonly Figure _sourceFigure;

        private readonly FigureLocation _moveToLocation;

        private readonly MovementTerminationReason _movementTerminationReason;
        private MoveRankDescriptor _moveRank;

        public FigureMoveOption(Figure sourceFigure, FigureLocation moveToLocation, MovementTerminationReason terminationReason)
        {
            _sourceFigure = sourceFigure;
            _moveToLocation = moveToLocation;
            _movementTerminationReason = terminationReason;

        }

        public FigureMoveOption(Figure sourceFigure, FigureLocation moveToLocation, MovementTerminationReason terminationReason,
            MoveRankDescriptor moveRank)
            :this(sourceFigure, moveToLocation, terminationReason)
        {            
            _moveRank = moveRank;
        }

        public FigureLocation MoveToLocation => _moveToLocation;
        public MoveRankDescriptor MoveRank => _moveRank;
        public Figure SourceFigure => _sourceFigure;

        public MovementTerminationReason TerminationReason=> _movementTerminationReason;
        public bool WasEvaluated => _moveRank != null;

        public int CompareTo(FigureMoveOption other)
        {
            if (this.MoveRank == null && other.MoveRank == null)
                return 0;

            if (this.MoveRank == null)
                return -1;

            if (other.MoveRank == null)
                return 1;

            if(this.MoveRank.Category!= other.MoveRank.Category)
                return this.MoveRank.Category.CompareTo(other.MoveRank.Category);

            return this.MoveRank.Rank.CompareTo(other.MoveRank.Rank);

        }

        public void SetMoveRank(MoveRankDescriptor moveRank)
        {
            if (_moveRank!= null)
                throw new Exception("Move already was evaluated");

            _moveRank = moveRank;
        }

        public override string ToString()
        {
            return $"{_sourceFigure.Category}:{_sourceFigure.CurrentLocation.PositionName} -> {_moveToLocation.PositionName} /{_moveRank?.ToString()}/";
        }

    }
}
