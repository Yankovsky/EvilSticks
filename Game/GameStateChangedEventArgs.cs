using System;

namespace Game
{
    public class GameStateChangedEventArgs : EventArgs
    {
        public GameStateChangedEventArgs(object move)
        {
            Move = move;
        }

        public object Move { get; private set; }
    }
}
