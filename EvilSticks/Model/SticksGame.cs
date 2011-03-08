using Game;

namespace EvilSticks.Model
{
    public sealed class SticksGame : TwoPlayerGame
    {
        public SticksGame(int sticksCount, int currentPlayerIndex, Player firstPlayer, Player secondPlayer)
            : base(currentPlayerIndex, firstPlayer, secondPlayer)
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