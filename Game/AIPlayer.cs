namespace Game
{
    public abstract class AIPlayer : Player
    {
        public AIPlayer(string name, int latency) : base(name)
        {
            Latency = latency;
        }

        public int Latency;

        public void ResetWinsCount()
        {
            WinsCount = 0;
        }

    }
}