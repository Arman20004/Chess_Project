using System;
using System.Collections.Generic;

namespace Backend.Models
{
    internal abstract class Figure : ICloneable
    {
        private readonly FigureCategory _category;
        private readonly int _weight;

        public Figure(FigureCategory figureCategory, bool isComputerFigure, int baseWeight, FigureLocation initialLocation)
        {
            _category = figureCategory;
            _weight = isComputerFigure ? -1 * baseWeight : baseWeight;
            SetCurrentLocation(initialLocation);
        }

        public FigureCategory Category => _category;

        public int Weight => _weight;

        public FigureLocation CurrentLocation { get; protected set; }

        public virtual void SetCurrentLocation(FigureLocation newLocation)
        {
            CurrentLocation = newLocation;
        }
        public abstract IEnumerable<FigureMoveOption> GetPossibleMoves(IDispositionProvider disposition);

        public bool IsComputerFigure => _weight < 0;


        protected IEnumerable<FigureMoveOption> GetPossibleMovesBase(IDispositionProvider disposition,
                                Func<IEnumerable<FigureLocation>> navigatorFunc,
                                Func<FigureMoveOption, bool> customAcceptanceChecker = null,
                                bool terminateOnReachingOtherFigure = true)
        {
            Figure otherFigure = null;
            FigureMoveOption moveOption = null;
            foreach (var move in navigatorFunc())
            {
                otherFigure = disposition.GetFigureAtLocation(move);
                moveOption = CreateFigureMoveOption(move, otherFigure);

                if (customAcceptanceChecker == null ||
                       customAcceptanceChecker(moveOption))
                {
                    yield return moveOption;
                }

                if (terminateOnReachingOtherFigure && otherFigure != null)
                {
                    yield break;
                }
            }
        }

        protected FigureMoveOption CreateFigureMoveOption(FigureLocation move, Figure otherFigure)
        {

            if (otherFigure == null)
            {
                return new FigureMoveOption(this, move, MovementTerminationReason.None);
            }

            return new FigureMoveOption(this, move,
                otherFigure.IsComputerFigure == this.IsComputerFigure ? MovementTerminationReason.ReachedSelfFigure
                : MovementTerminationReason.ReachedOpponentsFigure);


        }
        public object Clone()
        {
            Figure otherFigure = (Figure)this.MemberwiseClone();
            otherFigure.CurrentLocation = new FigureLocation(this.CurrentLocation.HorizontalPosition, this.CurrentLocation.VerticalPosition);
            return otherFigure;
        }
    }
}
