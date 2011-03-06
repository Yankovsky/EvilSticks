using Game;

namespace EvilSticks.Model
{
    public abstract class SticksPlayer : Player
    {
        internal SticksPlayer(string name) : base(name) { }

        public int SticksCount { private get; set; }

    }
}