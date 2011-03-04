using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using EvilSticks.Model;
using GalaSoft.MvvmLight.Threading;
using System.Windows;

namespace EvilSticks.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
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