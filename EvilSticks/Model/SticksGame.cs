using Game;

namespace EvilSticks.Model
{
    public sealed class SticksGame : TwoPlayerGame
    {
        public SticksGame(Player firstPlayer, Player secondPlayer, Player currentPlayer, int sticksCount)
            : base(firstPlayer, secondPlayer, currentPlayer)
        {
            SticksCount = sticksCount;
        }

        public int SticksCount { get; private set; }
        
        protected override void OnGameStateChanging(object move)
        {
            SticksCount -= (int)move;
        }

        protected override bool AreWinConditionsPerformed()
        {
            return SticksCount <= 0;
        }
    }
}