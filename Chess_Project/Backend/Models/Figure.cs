using Backend.Engines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        protected IEnumerable<FigureMoveOption> GetPossibleMovesBase(IDispositionProvider disposition, Func<IEnumerable<FigureLocation>> navigatorFunc)
        {
            Figure otherFigure = null;

            foreach (var move in navigatorFunc())
            {
                otherFigure = disposition.GetFigureAtLocation(move);
                if (otherFigure == null)
                {
                    yield return new FigureMoveOption(this, move, MovementTerminationReason.None);
                }
                else
                {
                    yield return new FigureMoveOption(this, move,
                        otherFigure.IsComputerFigure == this.IsComputerFigure ? MovementTerminationReason.ReachedSelfFigure
                        : MovementTerminationReason.ReachedOpponentsFigure);

                    yield break;
                }
            }
        }

        public object Clone()
        {
            Figure otherFigure = (Figure)this.MemberwiseClone();
            otherFigure.CurrentLocation = new FigureLocation(this.CurrentLocation.HorizontalPosition, this.CurrentLocation.VerticalPosition);
            return otherFigure;
        }
    }
}
