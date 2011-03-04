using System;
using System.ComponentModel;
using System.Windows.Input;
using EvilSticks.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace EvilSticks.ViewModels
{
    public class GameViewModel : ViewModelBase, IGame
    {
        public GameViewModel()
        {
            if (DesignerProperties.IsInDesignTool)
            {
                SticksCount = 2;
            }
            SticksCount = 11;
        }

        public GameViewModel(Player firstPlayer, Player secondPlayer)
        {
            SticksCount = 11;
        }

        private int _sticksCount;
        public int SticksCount
        {
            get
            {
                return _sticksCount;
            }
            set
            {
                if (_sticksCount != value)
                {
                    _sticksCount = value;
                    RaisePropertyChanged("SticksCount");
                }
            }
        }

        public event EventHandler<GameEndedEventArgs> GameEnded;

        public ICommand RemoveSticksCommand
        {
            get
            {
                return new RelayCommand<string>((param) =>
                {
                    var sticksToRemoveCount = int.Parse(param);
                    SticksCount -= sticksToRemoveCount;
                });
            }
        }

        public void Start()
        {

        }

    }

    public class GameEndedEventArgs : EventArgs
    {
        public GameEndedEventArgs()
        {

        }
    }
}
