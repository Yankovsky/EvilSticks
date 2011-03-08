using System;
using System.Windows.Input;
using EvilSticks.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Game;
using Tools;
using System.ComponentModel;
using System.Threading;
using System.Windows.Threading;
using GalaSoft.MvvmLight.Threading;

namespace EvilSticks.ViewModels
{
    public class EducationViewModel : ViewModelBase
    {

        public EducationViewModel()
        {
            if (IsInDesignMode)
                EducatedAIPlayer = new SticksAIPlayer("Bot", 0);
            else
                InitializeCommands();
        }

        #region Public Properties

        private SticksAIPlayer _educatedAIPlayer;
        public SticksAIPlayer EducatedAIPlayer
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

        private int _totalEducationalGamesCount = 1;
        public int TotalEducationalGamesCount
        {
            get
            {
                return _totalEducationalGamesCount;
            }
            set
            {
                if (_totalEducationalGamesCount != value)
                {
                    _totalEducationalGamesCount = value;
                    RaisePropertyChanged("TotalEducationalGamesCount");
                }
            }
        }

        #endregion

        #region Commands

        public ICommand RunEducationalGamesCommand { get; private set; }

        private void InitializeCommands()
        {
            RunEducationalGamesCommand = new RelayCommand(() =>
            {
                int _currentEducationalGamesCount = 0;
                var firstAI = new SticksAIPlayer(BotNames.GetRandom(), 0);
                var secondAI = new SticksAIPlayer(BotNames.GetRandom(), 0);

                Messenger.Default.Send<Tokens, MainViewModel>(Tokens.EducationStarted);
                /*
                var worker = new BackgroundWorker();
                worker.DoWork += new DoWorkEventHandler((sender, e) =>
                {
                    for (int i = 0; i < TotalEducationalGamesCount; i++)
                    {
                        var currentPlayerIndex = DataHelper.GetRandomElement(0, 1);
                        var game = new SticksGame(11, currentPlayerIndex, firstAI, secondAI);
                        game.GameEnded += new EventHandler<GameEndedEventArgs>((sender2, e2) =>
                        {
                            if (++_currentEducationalGamesCount == _totalEducationalGamesCount)
                            {
                                DispatcherHelper.CheckBeginInvokeOnUI(() =>
                                    {
                                        EducatedAIPlayer = firstAI.WinsCount > secondAI.WinsCount ? firstAI : secondAI;
                                        EducatedAIPlayer.Latency = 1000;
                                        Messenger.Default.Send<SticksAIPlayer>(EducatedAIPlayer, Tokens.EducationEnded);
                                    });
                            }
                        });
                        game.Start();
                    }
                });
                worker.RunWorkerAsync();*/
                RunEducationalGames(firstAI, secondAI);
                    
            }, () =>
            {
                return true;
            });
        }

        private void RunEducationalGames(SticksAIPlayer firstAI, SticksAIPlayer secondAI)
        {
            var currentPlayerIndex = DataHelper.GetRandomElement(0, 1);
            var game = new SticksGame(11, currentPlayerIndex, firstAI, secondAI);
            game.GameEnded += new EventHandler<GameEndedEventArgs>((sender2, e2) =>
            {
                if (++_currentEducationalGamesCount == _totalEducationalGamesCount)
                {
                    EducatedAIPlayer = firstAI.WinsCount > secondAI.WinsCount ? firstAI : secondAI;
                    EducatedAIPlayer.Latency = 1000;
                    Messenger.Default.Send<SticksAIPlayer>(EducatedAIPlayer, Tokens.EducationEnded);
                }
                else
                    RunEducationalGames(firstAI, secondAI);
            });
            game.Start();
        }

        #endregion

        #region Private Fields

        private int _currentEducationalGamesCount;

        #endregion

    }
}