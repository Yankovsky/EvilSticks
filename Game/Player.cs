using System;

namespace Game
{
    public abstract class Player
    {
        public Player(string name)
        {
            Name = name;
        }

        public event EventHandler<GameStateChangedEventArgs> MoveMade;

        public void OnMoveMade(object move)
        {
            if (MoveMade != null)
                MoveMade(this, new GameStateChangedEventArgs(move));
        }

        public abstract void MakeMove();

        public string Name { get; set; }
        public double WinsCount { get; internal set; }
        public int GamesCount { get; internal set; }
        private Game game;
        public Game Game
        {
            get
            {
                return game;
            }
            internal set
            {
                game = value;
                Game.GameInitialized += OnGameStarted;
            }
        }

        protected virtual void OnGameStarted(object sender, EventArgs e)
        {

        }
    }
}