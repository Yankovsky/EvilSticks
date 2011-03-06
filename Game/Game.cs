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
            GameEnded += OnGameEnded;
            CurrentPlayer.MakeMove();
        }

        public Player CurrentPlayer { get; private set; }
        public event EventHandler<EventArgs> GameEnded;

        protected void OnPlayerMadeTurn(object sender, GameStateChangedEventArgs e)
        {
            CurrentPlayer = SwitchToPlayer();
            ChangeGameState(e);
            if (AreWinConditionsPerformed())
            {
                Result = GameResult.WinnerExist;
                GameEnded(this, EventArgs.Empty);
            }
            else if (this is IDrawable)
            {
                if ((this as IDrawable).AreDrawConditionsPerformed())
                {
                    Result = GameResult.Draw;
                    GameEnded(this, EventArgs.Empty);
                }
            }
            else
                CurrentPlayer.MakeMove();
        }

        protected abstract void ChangeGameState(GameStateChangedEventArgs e);

        protected abstract Player SwitchToPlayer();

        protected abstract bool AreWinConditionsPerformed();

        protected abstract void OnGameEnded(object sender, EventArgs e);

        protected GameResult Result;  

    }
}