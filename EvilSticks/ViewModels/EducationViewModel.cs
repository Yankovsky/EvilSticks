using System;
using System.Windows.Input;
using EvilSticks.Model;
using EvilSticks.Tools;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Game;
using Tools;

namespace EvilSticks.ViewModels
{
    public class EducationViewModel : ViewModelBase
    {

        public EducationViewModel()
        {
            if (IsInDesignMode)
                EducatedAIPlayer = new SticksAIPlayer("Bot", 0);
            else
            {
                InitializeCommands();
                RegisterToMessages();
            }
        }

        private void RegisterToMessages()
        {
            Messenger.Default.Register<Tokens>(this, (token) =>
            {
                if (token == Tokens.GameStarted)
                {
                    _isGameInProgress = true;
                    (RunEducationalGamesCommand as RelayCommand).RaiseCanExecuteChanged();
                }
            });
            Messenger.Default.Register<Tuple<Player, Player>>(this, Tokens.GameEnded, (playersPair) =>
            {
                _isGameInProgress = false;
                (RunEducationalGamesCommand as RelayCommand).RaiseCanExecuteChanged();
            });
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
                _currentEducationalGamesCount = 0;
                _firstAI = new SticksAIPlayer(BotNames.GetRandom(), 0);
                _secondAI = new SticksAIPlayer(BotNames.GetRandom(), 0);

                Messenger.Default.Send<Tokens, MainViewModel>(Tokens.EducationStarted);
                RunEducationalGames();                    
            }, () =>
            {
                return !_isGameInProgress;
            });
        }

        private void RunEducationalGames()
        {
            var currentPlayerIndex = CollectionExtensions.GetRandomElement(0, 1);
            var game = new SticksGame(11, currentPlayerIndex, _firstAI, _secondAI);
            game.GameEnded += OnGameEnded;
            game.Start();
        }

        private void OnGameEnded(object sender, EventArgs e)
        {
            (sender as SticksGame).GameEnded -= OnGameEnded;
            _currentEducationalGamesCount++;
            if (_currentEducationalGamesCount == _totalEducationalGamesCount)
            {
                EducatedAIPlayer = _firstAI.WinsCount > _secondAI.WinsCount ? _firstAI : _secondAI;
                EducatedAIPlayer.Latency = 1000;
                Messenger.Default.Send<SticksAIPlayer>(EducatedAIPlayer, Tokens.EducationEnded);
            }
            else
            {
                RunEducationalGames();
            }
        }

        #endregion

        #region Private Fields

        private bool _isGameInProgress;
        private int _currentEducationalGamesCount;
        private SticksAIPlayer _firstAI;
        private SticksAIPlayer _secondAI;

        #endregion

    }
}