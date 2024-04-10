using Backend.State;
using System;

namespace Backend.Models
{
    internal class FigureLocation : IEquatable<FigureLocation>, ICloneable
    {
        public int HorizontalPosition { get; set; }
        public int VerticalPosition { get; set; }


        public FigureLocation(int horizontalPosition, int verticalPosition)
        {
            VerticalPosition = verticalPosition;
            HorizontalPosition = horizontalPosition;
        }

        public string PositionName
        {
            get
            {
                char letterName = (char)((int)'A' + HorizontalPosition);
                return $"{letterName}{GameSetup.BoardSize - VerticalPosition}";
            }
        }

        public bool Equals(FigureLocation other)
        {
            if (other == null) return false;

            return this.HorizontalPosition == other.HorizontalPosition &&
                this.VerticalPosition == other.VerticalPosition;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
