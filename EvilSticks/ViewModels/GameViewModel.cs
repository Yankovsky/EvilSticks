using System.Windows.Input;
using EvilSticks.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Game;

namespace EvilSticks.ViewModels
{
    public class GameViewModel : ViewModelBase
    {
        public GameViewModel()
        {
            if (IsInDesignMode)
            {
                FirstPlayer = new SticksHumanPlayer("Andrey");
                SecondPlayer = new SticksAIPlayer("Bot");
                _game = new SticksGame(FirstPlayer, SecondPlayer, SecondPlayer, 11);
            }
            else
            {
                Messenger.Default.Register<SticksPlayer>(this, Tokens.FirstPlayer, (firstPlayer) =>
                {
                    FirstPlayer = firstPlayer;
                });
                Messenger.Default.Register<SticksPlayer>(this, Tokens.SecondPlayer, (secondPlayer) =>
                {
                    SecondPlayer = secondPlayer;
                });
            }
        }

        #region Public Properties

        public SticksPlayer FirstPlayer
        {
            get
            {
                return (SticksPlayer)_game.FirstPlayer;
            }
        }

        public SticksPlayer SecondPlayer
        {
            get
            {
                return (SticksPlayer)_game.SecondPlayer;
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
                    _game.CurrentPlayer.OnMoveMade(this, new GameStateChangedEventArgs(sticksToRemoveCount));
                });
            }
        }

        #region

        private SticksGame _game;

        #endregion

    }
}