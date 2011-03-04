using System.Windows;
using EvilSticks.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

namespace EvilSticks.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            if (IsInDesignMode)
            {
                ViewModelLocator.CreateEducation();
                ViewModelLocator.CreateGame();
            }
            Messenger.Default.Register<Messages>(this, (message) =>
                {
                    if (message == Messages.EducationStarted)
                        IsBusy = true;
                });
            Messenger.Default.Register<AIPlayer>(this, Tokens.EducationEnded, (player) =>
                {
                    MessageBox.Show(player.Name + " win!");
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

    }
}