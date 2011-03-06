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

        public void OnMoveMade(object sender, GameStateChangedEventArgs e)
        {
            if (MoveMade != null)
                MoveMade(this, e);
        }

        public string Name { get; private set; }
        public double WinsCount { get; internal set; }
        public int GamesCount { get; internal set; }

        public abstract void MakeMove();

    }
}