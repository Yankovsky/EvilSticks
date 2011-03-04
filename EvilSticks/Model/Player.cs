using System.ComponentModel;
namespace EvilSticks.Model
{
    public abstract class Player
    {
        public Player(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
        public IGame Game { get; set; }
        public int WinsCount { get; set; }
        public int GamesCount { get; set; }
        public bool IsPlayerTurn { get; set; }
    }
}