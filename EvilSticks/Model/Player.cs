namespace EvilSticks.Model
{
    public abstract class Player
    {
        public IGame Game;

        public int WinsCount { get; set; }
    }
}