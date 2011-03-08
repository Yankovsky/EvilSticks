using System;

namespace Game
{
    public abstract class Game
    {
        #region Public Interface

        public Game(int currentPlayerIndex, params Player[] players)
        {
            if (currentPlayerIndex < players.GetLowerBound(0) || currentPlayerIndex > players.GetUpperBound(0))
                throw new IndexOutOfRangeException("CurrentPlayerIndex cannot be negative or more than players count!");

            _currentPlayerIndex = currentPlayerIndex;
            _players = players;
            foreach (var player in _players)
            {
                player.MoveMade += OnPlayerMadeTurn;
                player.Game = this;
            }
            GameInitialized(this, EventArgs.Empty);
        }

        public void Start()
        {
            CurrentPlayer.MakeMove();
        }

        public Player CurrentPlayer { get { return _players[_currentPlayerIndex]; } }

        #endregion

        #region Events

        public event EventHandler<EventArgs> GameInitialized;
        public event EventHandler<EventArgs> MoveRequested;
        public event EventHandler<GameEndedEventArgs> GameEnded;
        public event EventHandler<GameStateChangedEventArgs> GameStateChanged;

        #endregion

        #region EventHadlers

        protected void OnPlayerMadeTurn(object sender, GameStateChangedEventArgs e)
        {
            ChangeGameState(e);
            if (AreWinConditionsPerformed())
                EndGame(GameResult.WinnerExist);
            else if (AreDrawConditionsPerformed())
                EndGame(GameResult.Draw);
            else
            {
                SwitchToPlayer();
                if (MoveRequested != null)
                    MoveRequested(this, EventArgs.Empty);
                CurrentPlayer.MakeMove();
            }
        }

        #endregion

        #region Private Methods

        private void ChangeGameState(GameStateChangedEventArgs e)
        {
            OnGameStateChanging(e.Move);
            if (GameStateChanged != null)
                GameStateChanged(this, e);
        }

        private void EndGame(GameResult result)
        {
            foreach (var player in _players)
            {
                player.GamesCount++;
                player.MoveMade -= OnPlayerMadeTurn;
            }
            OnGameEnding(result);
            if (GameEnded != null)
                GameEnded(this, new GameEndedEventArgs(result));
        }
        
        #endregion

        #region Abstract and Virtual Methods

        protected abstract void OnGameStateChanging(object move);

        protected abstract void OnGameEnding(GameResult result);

        protected abstract void SwitchToPlayer();

        protected abstract bool AreWinConditionsPerformed();

        protected virtual bool AreDrawConditionsPerformed()
        {
            return false;
        }

        #endregion

        #region Protected Members

        protected Player[] _players { get; private set; }
        protected int _currentPlayerIndex { get; set; }

        #endregion
    }
}