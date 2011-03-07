using System.Windows.Input;
using EvilSticks.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Game;
using System;
using System.Windows;

namespace EvilSticks.ViewModels
{
    public class GameViewModel : ViewModelBase
    {
        public GameViewModel()
        {
            if (IsInDesignMode)
            {
                _game = new SticksGame(FirstPlayer, SecondPlayer, SecondPlayer, 11);
            }
            else
            {
                var a = new SticksAIPlayer("BOt", 1000);
                var b = new SticksHumanPlayer("Human");
                _game = new SticksGame(a, b, a, 11);
                _game.GameStateChanged += OnGameStateChanged;
                _game.GameEnded += OnGameEnded;
                _game.Start();
                /*
                Messenger.Default.Register<SticksPlayer>(this, Tokens.FirstPlayer, (firstPlayer) =>
                {
                    FirstPlayer = firstPlayer;
                });
                Messenger.Default.Register<SticksPlayer>(this, Tokens.SecondPlayer, (secondPlayer) =>
                {
                    SecondPlayer = secondPlayer;
                });
                 * */
            }
        }

        void OnGameEnded(object sender, GameEndedEventArgs e)
        {
            MessageBox.Show(_game.CurrentPlayer.ToString());
        }

        void OnGameStateChanged(object sender, GameStateChangedEventArgs e)
        {
            RaisePropertyChanged("SticksCount");
            RaisePropertyChanged("IsFirstPlayerTurn");
        }

        #region Public Properties

        public Player FirstPlayer
        {
            get
            {
                return _game != null ? _game.FirstPlayer : null;
            }
        }

        public Player SecondPlayer
        {
            get
            {
                return _game != null ? _game.SecondPlayer : null;
            }
        }

        public bool IsFirstPlayerTurn
        {
            get
            {
                return _game != null && _game.CurrentPlayer == _game.FirstPlayer ? true : false;
            }
        }

        public int SticksCount
        {
            get
            {
                return _game != null ? _game.SticksCount : 0;
            }
        }

        #endregion

        #region Commands

        public ICommand RemoveSticksCommand
        {
            get
            {
                return new RelayCommand<string>((param) =>
                {
                    var sticksToRemoveCount = int.Parse(param);
                    _game.CurrentPlayer.OnMoveMade(sticksToRemoveCount);
                });
            }
        }

        #endregion

        #region Private Fields

        private SticksGame _game;

        #endregion

    }
}