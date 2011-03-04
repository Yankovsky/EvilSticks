using System;
using System.Windows.Input;
using EvilSticks.Model;
using EvilSticks.ViewModels;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace EvilSticks.ViewModels
{
    public class EducationViewModel : ViewModelBase
    {

        #region Public Properties

        private double _educationalGamesCount;
        public double EducationalGamesCount
        {
            get
            {
                return _educationalGamesCount;
            }
            set
            {
                if (_educationalGamesCount != value)
                {
                    _educationalGamesCount = value;
                    RaisePropertyChanged("EducationalGamesCount");
                }
            }
        }

        #endregion

        #region Commands

        public ICommand RunEducationalGamesCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    MessengerInstance.Send(new NotificationMessage("EducationStarted"));
                    var firstAI = new AIPlayer();
                    var secondAI = new AIPlayer();
                    for (int i = 0; i < EducationalGamesCount; i++)
                    {
                        var game = new GameViewModel(firstAI, secondAI);
                        game.Start();
                    }
                    var educatedAIPlayer = firstAI.WinsCount > secondAI.WinsCount ? firstAI : secondAI;
                    MessengerInstance.Send(new NotificationMessage<AIPlayer>(educatedAIPlayer, "EducationEnded"));
                });
            }
        }

        #endregion
    }

}
