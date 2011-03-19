using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Game;

namespace EvilSticks.ViewModels
{
    public class ChangePlayerNameDialogViewModel : ViewModelBase
    {

        public ChangePlayerNameDialogViewModel()
        {
            if (IsInDesignMode)
                Name = "SuperPlayer";
            else
            {
                InitializeCommands();
                RegisterToMessages();
            }
        }
        
        private void RegisterToMessages()
        {
            Messenger.Default.Register<Player>(this, (player) =>
            {
                _player = player;
            });
        }

        #region Public Properties

        public string Name
        {
            get
            {
                return _player.Name;
            }
            set
            {
                if (_player.Name != value)
                {
                    _player.Name = value;
                    RaisePropertyChanged("Name");
                }
            }
        }

        #endregion

        #region Commands

        public ICommand SaveChangesCommand { get; private set; }

        private void InitializeCommands()
        {
            SaveChangesCommand = new RelayCommand(() =>
            {
                Messenger.Default.Send<Player>(_player, Tokens.PlayerNameChanged);
            });
        }

        #endregion

        #region Private Fields

        private Player _player;

        #endregion

    }
}
