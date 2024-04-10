using Backend.Models;
using System;
using System.Collections.Generic;

namespace Backend.Engines
{
    internal static class MoveSimulationHelpers
    {
        private readonly static Tuple<int, int>[] _knightMoveOffsets = new Tuple<int, int>[]
        {
            new Tuple<int, int>(-1,  2),
            new Tuple<int, int>( 1,  2),
            new Tuple<int, int>( 2,  1),
            new Tuple<int, int>( 2,  -1)
           /* mirrored offsets are not required
            new Tuple<int, int>( 1,  -2),
            new Tuple<int, int>( -1, -2),
            new Tuple<int, int>( -2, -1),
            new Tuple<int, int>( -2, 1)
           */
        };


        public static IEnumerable<FigureLocation> GetMoves_Horizontal(FigureLocation fromCurrentLocation, MoveDirectionHorizontal direction)
        {
            int step = direction == MoveDirectionHorizontal.Right ? 1 : -1;
            int currentHorizPosition = fromCurrentLocation.HorizontalPosition + step;

            while (IsPositionWithinBoard(currentHorizPosition))
            {
                yield return new FigureLocation(currentHorizPosition, fromCurrentLocation.VerticalPosition);
                currentHorizPosition += step;
            }

        }

        public static IEnumerable<FigureLocation> GetMoves_Vertical(FigureLocation fromCurrentLocation, MoveDirectionVertical direction)
        {
            int step = direction == MoveDirectionVertical.Up ? 1 : -1;
            if (State.GameSetup.BoardFlipped)
            {
                step *= -1;
            }

            int currentVertPosition = fromCurrentLocation.VerticalPosition + step;

            while (IsPositionWithinBoard(currentVertPosition))
            {
                yield return new FigureLocation(fromCurrentLocation.HorizontalPosition, currentVertPosition);
                currentVertPosition += step;
            }
        }

        public static IEnumerable<FigureLocation> GetMoves_Diagonal(FigureLocation fromCurrentLocation, MoveDirectionHorizontal directionHoriz, MoveDirectionVertical directionVert)
        {
            int stepHoriz = directionHoriz == MoveDirectionHorizontal.Right ? 1 : -1;
            int stepVert = directionVert == MoveDirectionVertical.Up ? -1 : 1;

            if (State.GameSetup.BoardFlipped)
            {
                stepVert *= -1;
            }

            int currentHorizPosition = fromCurrentLocation.HorizontalPosition + stepHoriz;
            int currentVertPosition = fromCurrentLocation.VerticalPosition + stepVert;

            while (IsPositionWithinBoard(currentHorizPosition, currentVertPosition))
            {
                yield return new FigureLocation(currentHorizPosition, currentVertPosition);

                currentHorizPosition += stepHoriz;
                currentVertPosition += stepVert;
            }
        }

        public static IEnumerable<FigureLocation> GetMoves_Pawn(IDispositionProvider disposition,
               bool isComputerFigure, FigureLocation fromCurrentLocation, bool isOriginalPosition)
        {
            // pawn always moves up (when not flipped)
            // int step = State.GameSetup.BoardFlipped ? -1 : 1;
            int step = isComputerFigure ? 1 : -1;

            // move one  square up
            int vertPosition = fromCurrentLocation.VerticalPosition + step;
            if (!IsPositionWithinBoard(vertPosition))
                yield break;

            yield return new FigureLocation(fromCurrentLocation.HorizontalPosition, vertPosition);

            Figure figure = null;
            // eat  left  and right
            FigureLocation[] eatLocations = new FigureLocation[]
            {
                new FigureLocation(fromCurrentLocation.HorizontalPosition - 1, vertPosition), // left
                new FigureLocation(fromCurrentLocation.HorizontalPosition + 1, vertPosition), // right
            };

            foreach (var eatLoc in eatLocations)
            {

                if (IsPositionWithinBoard(eatLoc))
                {
                    figure = disposition.GetFigureAtLocation(eatLoc);
                    if (figure != null  /* && !figure.IsMyFigure */)
                    {
                        yield return eatLoc;
                    }
                }
            }


            // when at original position, move two squares up 
            if (!isOriginalPosition)
                yield break;

            vertPosition += step;
            if (!IsPositionWithinBoard(vertPosition))
                yield break;

            yield return new FigureLocation(fromCurrentLocation.HorizontalPosition, vertPosition);


        }

        public static IEnumerable<FigureLocation> GetMoves_Knight(FigureLocation fromCurrentLocation)
        {
            int horizPosition;
            int vertPosition;
            foreach (var ent in _knightMoveOffsets)
            {
                horizPosition = fromCurrentLocation.HorizontalPosition + ent.Item1;
                vertPosition = fromCurrentLocation.VerticalPosition + ent.Item2;

                if (IsPositionWithinBoard(horizPosition, vertPosition))
                    yield return new FigureLocation(horizPosition, vertPosition);

                // same mirrored (array gives 4 offsets, other 4 are mirrored
                horizPosition = fromCurrentLocation.HorizontalPosition - ent.Item1;
                vertPosition = fromCurrentLocation.VerticalPosition - ent.Item2;

                if (IsPositionWithinBoard(horizPosition, vertPosition))
                    yield return new FigureLocation(horizPosition, vertPosition);

            }
        }

        public static IEnumerable<FigureLocation> GetMoves_King(FigureLocation fromCurrentLocation)
        {
            List<Tuple<int, int>> kingMoveOffsets = new List<Tuple<int, int>>();
            for (int i = -1; i <= 1; i++) { kingMoveOffsets.Add(new Tuple<int, int>(i, 1)); }
            for (int i = -1; i <= 1; i++) { kingMoveOffsets.Add(new Tuple<int, int>(i, -1)); }

            kingMoveOffsets.Add(new Tuple<int, int>(-1, 0));
            kingMoveOffsets.Add(new Tuple<int, int>(1, 0));

            int horizPosition;
            int vertPosition;
            foreach (var ent in kingMoveOffsets)
            {
                horizPosition = fromCurrentLocation.HorizontalPosition + ent.Item1;
                vertPosition = fromCurrentLocation.VerticalPosition + ent.Item2;

                if (IsPositionWithinBoard(horizPosition, vertPosition))
                    yield return new FigureLocation(horizPosition, vertPosition);

            }
        }

        public static bool IsPositionWithinBoard(int position)
        {
            return position >= 0 && position < State.GameSetup.BoardSize;
        }

        public static bool IsPositionWithinBoard(int positionHorizontal, int positionVertical)
        {
            return IsPositionWithinBoard(positionHorizontal) && IsPositionWithinBoard(positionVertical);
        }

        public static bool IsPositionWithinBoard(FigureLocation location)
        {
            return IsPositionWithinBoard(location.HorizontalPosition, location.VerticalPosition);
        }

    }
}
