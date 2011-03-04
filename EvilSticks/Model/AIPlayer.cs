namespace EvilSticks.Model
{
    public class AIPlayer : Player
    {
        public AIPlayer(string name) : base(name) { }
        public int[] Boxes { get; set; }
        public void MakeMove()
        {
            var sticksCount = Game.SticksCount;
            var decision = 2;
            Game.RemoveSticksCommand.Execute(decision);
        }
    }
}