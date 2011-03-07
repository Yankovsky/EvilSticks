namespace Game
{
    public abstract class AIPlayer : Player
    {
        public AIPlayer(string name, int latency) : base(name)
        {
            _latency = latency;
        }

        protected int _latency;
    }
}