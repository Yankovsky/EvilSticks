using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using EvilSticks.Model;

namespace EvilSticks.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel()
        {
        }

        void EducationStarted()
        {
            IsBusy = true;
        }

        void EducationEnded()
        {
            //EducatedAIPlayer = e.AIPlayer;
            IsBusy = false;
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

        private AIPlayer _educatedAIPlayer;
        public AIPlayer EducatedAIPlayer
        {
            get
            {
                return _educatedAIPlayer;
            }
            set
            {
                if (_educatedAIPlayer != value)
                {
                    _educatedAIPlayer = value;
                    RaisePropertyChanged("EducatedAIPlayer");
                }
            }
        }

        #endregion

    }
}