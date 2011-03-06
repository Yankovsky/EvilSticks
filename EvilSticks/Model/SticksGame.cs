using Game;

namespace EvilSticks.Model
{
    public sealed class SticksGame : TwoPlayerGame
    {
        public SticksGame(SticksPlayer firstPlayer, SticksPlayer secondPlayer, SticksPlayer currentPlayer, int sticksCount)
            : base(firstPlayer, secondPlayer, currentPlayer)
        {
            _sticksCount = sticksCount;
            (FirstPlayer as SticksPlayer).SticksCount = sticksCount;
            (SecondPlayer as SticksPlayer).SticksCount = sticksCount;
        }

        private int _sticksCount { get; private set; }

        protected override bool AreWinConditionsPerformed()
        {
            return _sticksCount <= 0;
        }

        protected override void ChangeGameState(GameStateChangedEventArgs e)
        {
            _sticksCount -= (int)e.Move;
        }
    }
}