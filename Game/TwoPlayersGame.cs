using System;

namespace Game
{
    public abstract class TwoPlayerGame : Game
    {
        public TwoPlayerGame(Player firstPlayer, Player secondPlayer, Player currentPlayer)
            : base(currentPlayer)
        {
            FirstPlayer = firstPlayer;
            FirstPlayer.MoveMade += OnPlayerMadeTurn;
            SecondPlayer = secondPlayer;
            SecondPlayer.MoveMade += OnPlayerMadeTurn;
        }

        public Player FirstPlayer { get; private set; }
        public Player SecondPlayer { get; private set; }

        protected sealed override Player SwitchToPlayer()
        {
            return CurrentPlayer == FirstPlayer ? SecondPlayer : FirstPlayer;
        }

        public event EventHandler<EventArgs> GameTottalyEnded;
        protected sealed override void OnGameEnded(object sender, EventArgs e)
        {
            FirstPlayer.GamesCount++;
            SecondPlayer.GamesCount++;
            FirstPlayer.MoveMade -= OnPlayerMadeTurn;
            SecondPlayer.MoveMade -= OnPlayerMadeTurn;
            if (Result == GameResult.WinnerExist)
                CurrentPlayer.WinsCount++;
            else
            {
                FirstPlayer.WinsCount += 0.5;
                SecondPlayer.WinsCount += 0.5;
            }
            GameTottalyEnded(this, EventArgs.Empty);
        }
    }
}