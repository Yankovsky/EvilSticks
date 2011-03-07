using System;

namespace Game
{
    public class GameEndedEventArgs : EventArgs
    {
        public GameEndedEventArgs(GameResult gameResult)
        {
            Result = gameResult;
        }

        public GameResult Result { get; private set; }
    }
}