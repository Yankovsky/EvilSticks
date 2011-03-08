using System;

namespace Game
{
    public abstract class TwoPlayerGame : Game
    {
        #region Public Interface

        public TwoPlayerGame(int currentPlayerIndex, Player firstPlayer, Player secondPlayer)
            : base(currentPlayerIndex, firstPlayer, secondPlayer) { }

        public Player FirstPlayer { get { return _players[0]; } }
        public Player SecondPlayer { get { return _players[1]; } }
        public bool IsFirstPlayerTurn { get { return CurrentPlayer == FirstPlayer; } }

        #endregion

        #region Override

        protected override void SwitchToPlayer()
        {
            _currentPlayerIndex = _players[_currentPlayerIndex] == FirstPlayer ? 1 : 0;
        }
        
        protected sealed override void OnGameEnding(GameResult result)
        {
            if (result == GameResult.WinnerExist)
                _players[_currentPlayerIndex].WinsCount++;
            else if (result == GameResult.Draw)
            {
                FirstPlayer.WinsCount += 0.5;
                SecondPlayer.WinsCount += 0.5;
            }
        }

        #endregion
    }
}