using System;

namespace Game
{
    public abstract class Game
    {

        public Game(Player currentPlayer)
        {
            CurrentPlayer = currentPlayer;
        }

        public void Start()
        {
            CurrentPlayer.MakeMove();
        }

        public Player CurrentPlayer { get; private set; }
        public event EventHandler<GameEndedEventArgs> GameEnded;
        public event EventHandler<GameStateChangedEventArgs> GameStateChanged;

        protected void OnPlayerMadeTurn(object sender, GameStateChangedEventArgs e)
        {
            ChangeGameState(e);
            if (AreWinConditionsPerformed())
                EndGame(GameResult.WinnerExist);
            else if (AreDrawConditionsPerformed())
                EndGame(GameResult.Draw);
            else
            {
                CurrentPlayer = SwitchToPlayer();
                CurrentPlayer.MakeMove();
            }
        }

        private void ChangeGameState(GameStateChangedEventArgs e)
        {
            OnGameStateChanging(e.Move);
            if (GameStateChanged != null)
                GameStateChanged(this, e);
        }

        private void EndGame(GameResult result)
        {
            OnGameEnding(result);
            if (GameEnded != null)
                GameEnded(this, new GameEndedEventArgs(result));
        }

        protected abstract void OnGameStateChanging(object move);

        protected abstract void OnGameEnding(GameResult result);

        protected abstract Player SwitchToPlayer();

        protected abstract bool AreWinConditionsPerformed();

        protected virtual bool AreDrawConditionsPerformed()
        {
            return false;
        }
        
    }
}