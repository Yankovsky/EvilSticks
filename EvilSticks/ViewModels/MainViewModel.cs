using System.Windows;
using System.Windows.Input;
using EvilSticks.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Game;

namespace EvilSticks.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            _player = new Player("Unnamed");
            InitializeCommands();
            RegisterToMessages();
        }

        private void RegisterToMessages()
        {
            Messenger.Default.Register<Tokens>(this, (token) =>
            {
                if (token == Tokens.MainPageLoaded)
                    ChangePlayerNameCommand.Execute(new object());
            });
            Messenger.Default.Register<Tokens>(this, (token) =>
            {
                if (token == Tokens.EducationStarted)
                    IsBusy = true;
            });
            Messenger.Default.Register<SticksAIPlayer>(this, Tokens.EducationEnded, (player) =>
            {
                IsBusy = false;
            });
        }

        #region Public Properties

        private bool _isBusy;
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                if (_isBusy != value)
                {
                    _isBusy = value;
                    RaisePropertyChanged("IsBusy");
                }
            }
        }

        #endregion

        #region Commands

        public ICommand ChangePlayerNameCommand { get; private set; }

        private void InitializeCommands()
        {
            ChangePlayerNameCommand = new RelayCommand(() =>
            {
                Messenger.Default.Send<Player, MainPage>(_player);
            });
        }

        #endregion

        #region Private Fields

        private Player _player;

        #endregion

    }
}