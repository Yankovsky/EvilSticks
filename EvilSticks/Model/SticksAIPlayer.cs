using Game;

namespace EvilSticks.Model
{
    public class SticksAIPlayer : SticksPlayer
    {
        public SticksAIPlayer(string name) : base(name) { }

        public int[] Boxes { get; private set; }

        public override void MakeMove()
        {
            //TODO AILOGIC
            OnMoveMade(this, new GameStateChangedEventArgs(1));
        }

    }
}