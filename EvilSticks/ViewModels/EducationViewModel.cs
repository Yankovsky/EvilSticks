using System;
using System.Windows.Input;
using EvilSticks.Model;
using EvilSticks.ViewModels;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Threading;
using System.ComponentModel;
using GalaSoft.MvvmLight.Threading;

namespace EvilSticks.ViewModels
{
    public class EducationViewModel : ViewModelBase
    {

        public EducationViewModel()
        {
            if (IsInDesignMode)
            {
                EducatedAIPlayer = new AIPlayer(BotNames.GetRandom()) { GamesCount = 1000, WinsCount = 100 };
            }
        }

        #region Public Properties

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

        #region Commands

        public ICommand RunEducationalGamesCommand
        {
            get
            {
                return new RelayCommand<double>((educationalGamesCount) =>
                {
                    var firstAI = new AIPlayer(BotNames.GetRandom());
                    var secondAI = new AIPlayer(BotNames.GetRandom());

                    Messenger.Default.Send<Messages, MainViewModel>(Messages.EducationStarted);

                    var worker = new BackgroundWorker();
                    worker.DoWork += new DoWorkEventHandler((sender, e) =>
                    {
                        for (int i = 0; i < educationalGamesCount; i++)
                        {
                            var game = new GameViewModel() { FirstPlayer = firstAI, SecondPlayer = secondAI };
                            //Messenger.Default.Send<AIPlayer>(firstAI, Tokens.FirstPlayer);
                            //Messenger.Default.Send<AIPlayer>(secondAI, Tokens.SecondPlayer);
                            game.Start();
                        }
                    });

                    worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler((sender, e) =>
                    {
                        EducatedAIPlayer = firstAI.WinsCount > secondAI.WinsCount ? firstAI : secondAI;
                        Messenger.Default.Send<AIPlayer>(EducatedAIPlayer, Tokens.EducationEnded);
                    });

                    worker.RunWorkerAsync();
                });
            }
        }

        #endregion

    }

}
