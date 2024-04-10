namespace Backend.Models
{
    public enum FigureCategory
    {
        Pawn,
        Knight,
        Bishop,
        Rook,
        Queen,
        King
    }

    public enum MoveDirection
    {
        Horizontal,
        Vertical,
        Diagonal
    }

    public enum MoveDirectionHorizontal
    {
        Left,
        Right
    }

    public enum MoveDirectionVertical
    {
        Up,
        Down
    }

    public enum MovementTerminationReason
    {
        None,
        ReachedSelfFigure,
        ReachedOpponentsFigure
    }

    public enum SolutionCategory
    {
        None,
        PositionImprovement,
        MaterialLossAvoidance,
        MaterialImprovement,
        Mate,



    }
}
