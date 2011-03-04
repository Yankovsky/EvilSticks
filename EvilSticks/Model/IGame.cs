using System.Windows.Input;
using EvilSticks.Model;

namespace EvilSticks
{
    public interface IGame
    {
        Player FirstPlayer { get; set; }
        Player SecondPlayer { get; set; }
        int SticksCount { get; set; }
        void Start();
        ICommand RemoveSticksCommand { get; }
    }
}
