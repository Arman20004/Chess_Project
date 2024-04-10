namespace Backend.State
{
    internal static class GameSetup
    {
        public const bool ComputerPlaysBlack = true;

        // regular when white king initially stands at 1-st horizontal and white moves up 1->8 ( value = false)
        // flipped when white king initially stands at 8-th horizontal and white moves down 8->1 ( value = true)
        public const bool BoardFlipped = false;

        public const int BoardSize = 8;


    }
}
