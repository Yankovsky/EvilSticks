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
    public class GameViewModel : ViewModelBase
    {
        public GameViewModel()
        {
            if (IsInDesignMode)
                _game = new SticksGame(11, 1, new Player("Player"), new SticksAIPlayer("Bot", 0));
            else
            {
                InitializeCommands();
                RegisterToMessages();
                _educatedAIPlayer = new SticksAIPlayer(BotNames.GetRandom(), 1000);
            }
        }

        private void RegisterToMessages()
        {
            Messenger.Default.Register<Player>(this, Tokens.PlayerNameChanged, (player) =>
            {
                if (_humanPlayer == null)
                    _humanPlayer = player;
                RaisePropertyChanged("HumanPlayerName");
                (NewGameCommand as RelayCommand).RaiseCanExecuteChanged();
            });
            Messenger.Default.Register<SticksAIPlayer>(this, Tokens.EducationEnded, (educatedAIPlayer) =>
            {
                _educatedAIPlayer = educatedAIPlayer;
                _educatedAIPlayer.ResetWinsCount();
                RaisePropertyChanged("EducatedAIPlayerName");
                (NewGameCommand as RelayCommand).RaiseCanExecuteChanged();
            });
        }

        #region Event Handlers

        private void OnGameEnded(object sender, EventArgs e)
        {
            var winner = _game.CurrentPlayer;
            var loser = winner == _game.FirstPlayer ? _game.SecondPlayer : _game.FirstPlayer;
            Messenger.Default.Send<Tuple<Player, Player>>(new Tuple<Player, Player>(winner, loser), Tokens.GameEnded);
            IsGameAlive = false;
            RaisePropertyChanged("HumanPlayerWinsCount");
            RaisePropertyChanged("EducatedAIPlayerWinsCount");
        }

        private void OnGameStateChanged(object sender, GameStateChangedEventArgs e)
        {
            RaisePropertyChanged("SticksCount");
        }

        private void OnMoveRequested(object sender, EventArgs e)
        {
            RaisePropertyChanged("IsFirstPlayerTurn");
            (RemoveSticksCommand as RelayCommand<string>).RaiseCanExecuteChanged();
        }   

        #endregion

        #region Public Properties

        public string HumanPlayerName
        {
            get
            {
                return _humanPlayer != null ? _humanPlayer.Name : null;
            }
        }

        public double HumanPlayerWinsCount
        {
            get
            {
                return _humanPlayer != null ? _humanPlayer.WinsCount : 0.0;
            }
        }

        public string EducatedAIPlayerName
        {
            get
            {
                return _educatedAIPlayer != null ? _educatedAIPlayer.Name : null;
            }
        }

        public double EducatedAIPlayerWinsCount
        {
            get
            {
                return _educatedAIPlayer != null ? _educatedAIPlayer.WinsCount : 0.0;
            }
        }

        public bool? IsFirstPlayerTurn
        {
            get
            {
                return _game != null ? (bool?)_game.IsFirstPlayerTurn : null;
            }
        }

        public int SticksCount
        {
            get
            {
                return _game != null ? _game.SticksCount : 0;
            }
        }

        private bool _isGameAlive;
        public bool IsGameAlive
        {          
            get
            {
                return _isGameAlive;
            }
            set
            {
                if (_isGameAlive != value)
                {
                    _isGameAlive = value;
                    RaisePropertyChanged("IsGameAlive");
                }
            }
        }

        #endregion

        #region Commands

        public ICommand NewGameCommand { get; private set; }
        public ICommand RemoveSticksCommand { get; private set; }

        private void InitializeCommands()
        {
            NewGameCommand = new RelayCommand(() =>
            {
                Messenger.Default.Send<Tokens, EducationViewModel>(Tokens.GameStarted);
                _game = new SticksGame(11, CollectionExtensions.GetRandomElement(0, 1), _humanPlayer, _educatedAIPlayer);
                _game.GameStateChanged += OnGameStateChanged;
                _game.GameEnded += OnGameEnded;
                _game.MoveRequested += OnMoveRequested;
                IsGameAlive = true;

                RaisePropertyChanged("HumanPlayerName");
                RaisePropertyChanged("HumanPlayerWinsCount");
                RaisePropertyChanged("EducatedAIPlayerName");
                RaisePropertyChanged("EducatedAIPlayerWinsCount");
                RaisePropertyChanged("IsFirstPlayerTurn");
                RaisePropertyChanged("SticksCount");

                (RemoveSticksCommand as RelayCommand<string>).RaiseCanExecuteChanged();

                _game.Start();
            }, () => {
                return _humanPlayer != null && _educatedAIPlayer != null;
            });

            RemoveSticksCommand = new RelayCommand<string>((param) =>
            {
                var sticksToRemoveCount = int.Parse(param);
                _game.CurrentPlayer.OnMoveMade(sticksToRemoveCount);
            }, (param) =>
            {
                return _game != null ? _game.CurrentPlayer == _humanPlayer : false; 
            });

        }

        #endregion

        #region Private Fields

        private SticksGame _game;
        private Player _humanPlayer;
        private SticksAIPlayer _educatedAIPlayer;

        #endregion

    }
}