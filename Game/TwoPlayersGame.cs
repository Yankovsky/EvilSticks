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
            FirstPlayer.Game = this;
            SecondPlayer = secondPlayer;
            SecondPlayer.MoveMade += OnPlayerMadeTurn;
            SecondPlayer.Game = this;
        }

        public Player FirstPlayer { get; private set; }
        public Player SecondPlayer { get; private set; }

        protected sealed override Player SwitchToPlayer()
        {
            return CurrentPlayer == FirstPlayer ? SecondPlayer : FirstPlayer;
        }
        
        protected sealed override void OnGameEnding(GameResult result)
        {
            FirstPlayer.GamesCount++;
            SecondPlayer.GamesCount++;
            FirstPlayer.MoveMade -= OnPlayerMadeTurn;
            SecondPlayer.MoveMade -= OnPlayerMadeTurn;
            if (result == GameResult.WinnerExist)
                CurrentPlayer.WinsCount++;
            else if (result == GameResult.Draw)
            {
                FirstPlayer.WinsCount += 0.5;
                SecondPlayer.WinsCount += 0.5;
            }
        }
    }
}