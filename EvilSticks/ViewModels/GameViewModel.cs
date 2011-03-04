using System.Windows.Input;
using EvilSticks.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace EvilSticks.ViewModels
{
    public class GameViewModel : ViewModelBase, IGame
    {
        public GameViewModel()
        {
            if (IsInDesignMode)
            {
                SticksCount = 11;
                FirstPlayer = new HumanPlayer("Andrey") { WinsCount = 5 };
                SecondPlayer = new AIPlayer("Bot") { WinsCount = 4 };
            }
            else
            {
                SticksCount = 11;
                Messenger.Default.Register<Player>(this, Tokens.FirstPlayer, (firstPlayer) =>
                {
                    FirstPlayer = firstPlayer;
                });
                Messenger.Default.Register<Player>(this, Tokens.SecondPlayer, (secondPlayer) =>
                {
                    SecondPlayer = secondPlayer;
                });
            }
        }

        #region Public Properties

        private Player _firstPlayer;
        public Player FirstPlayer
        {
            get
            {
                return _firstPlayer;
            }
            set
            {
                if (_firstPlayer != value)
                {
                    _firstPlayer = value;
                    RaisePropertyChanged("FirstPlayer");
                }
            }
        }

        private Player _secondPlayer;
        public Player SecondPlayer
        {
            get
            {
                return _secondPlayer;
            }
            set
            {
                if (_secondPlayer != value)
                {
                    _secondPlayer = value;
                    RaisePropertyChanged("SecondPlayer");
                }
            }
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

        #endregion

        public ICommand RemoveSticksCommand
        {
            get
            {
                return new RelayCommand<string>((param) =>
                {
                    var sticksToRemoveCount = int.Parse(param);
                    if (SticksCount - sticksToRemoveCount > 0)
                        SticksCount -= sticksToRemoveCount;
                    else
                    {
                        SticksCount = 0;
                        Messenger.Default.Send(new NotificationMessage("GameEnded"));
                    }
                });
            }
        }

        public void Start()
        {
        }
        
    }
}