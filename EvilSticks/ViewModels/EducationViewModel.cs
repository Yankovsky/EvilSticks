using System.ComponentModel;
using System.Windows.Input;
using EvilSticks.Model;
using EvilSticks.Tools;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using Game;

namespace EvilSticks.ViewModels
{
    public class EducationViewModel : ViewModelBase
    {

        public EducationViewModel()
        {
            if (IsInDesignMode)
            {
                EducatedAIPlayer = new SticksAIPlayer(BotNames.GetRandom());
            }
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

        #endregion

        #region Commands

        public ICommand RunEducationalGamesCommand
        {
            get
            {
                return new RelayCommand<double>((param) =>
                {
                    var educationalGamesCount = (int)param;
                    var firstAI = new SticksAIPlayer(BotNames.GetRandom());
                    var secondAI = new SticksAIPlayer(BotNames.GetRandom());

                    Messenger.Default.Send<Messages, MainViewModel>(Messages.EducationStarted);
                    /*
                    var worker = new BackgroundWorker();
                    worker.DoWork += new DoWorkEventHandler((sender, e) =>
                    {
                        for (int i = 0; i < educationalGamesCount; i++)
                        {
                            var game = new SticksGame(firstAI, secondAI, BoolTools.GetRandomBool(), 11);
                            game.Start();
                        }
                    });

                    worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler((sender, e) =>
                    {
                        EducatedAIPlayer = firstAI.WinsCount > secondAI.WinsCount ? firstAI : secondAI;
                        Messenger.Default.Send<SticksAIPlayer>(EducatedAIPlayer, Tokens.EducationEnded);
                    });

                    worker.RunWorkerAsync();
                     * */
                    a = 0;
                    b = educationalGamesCount;
                    for (int i = 0; i < educationalGamesCount; i++)
                    {
                        lock (this)
                        { 
                            var currentPlayer = BoolTools.GetRandomBool() == true ? firstAI : secondAI;
                            var game = new SticksGame(firstAI, secondAI, currentPlayer, 11);
                        game.GameTottalyEnded += new EventHandler<EventArgs>((sender, e) => { TryToEnd(firstAI, secondAI); });
                        game.Start();
                        }
                    }
                });
            }
        }
        int a;
        double b;
        void TryToEnd(SticksAIPlayer firstAI, SticksAIPlayer secondAI)
        {
            a++;
            if (a == b)
            { 
                EducatedAIPlayer = firstAI.WinsCount > secondAI.WinsCount ? firstAI : secondAI;
                Messenger.Default.Send<SticksAIPlayer>(EducatedAIPlayer, Tokens.EducationEnded);
            }
        }
        #endregion

    }

}
