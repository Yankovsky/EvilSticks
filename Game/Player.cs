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

        public abstract void MakeMove();

        public void OnMoveMade(object move)
        {
            if (MoveMade != null)
                MoveMade(this, new GameStateChangedEventArgs(move));
        }

        public Game Game { get; internal set; }
        public string Name { get; private set; }
        public double WinsCount { get; internal set; }
        public int GamesCount { get; internal set; }

    }
}