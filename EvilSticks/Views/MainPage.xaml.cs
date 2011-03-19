using System.Windows;
using System.Windows.Controls;
using EvilSticks.ViewModels;
using GalaSoft.MvvmLight.Messaging;
using EvilSticks.Views;
using Game;
using EvilSticks.Model;
using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using EvilSticks.Tools;

namespace EvilSticks
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            Loaded += MainPage_Loaded;

            Messenger.Default.Register<Player>(this, (player) =>
            {
                var dialog = new ChangePlayerNameDialogView();
                Messenger.Default.Send<Player, ChangePlayerNameDialogViewModel>(player);
                dialog.Show();
            });
            Messenger.Default.Register<SticksAIPlayer>(this, Tokens.EducationEnded, (player) =>
            {
                var educationResult = new ChildWindow();
                educationResult.Content = new TextBlock() { Text = player.Name + " is ready for battle!" };
                educationResult.Show();
            });
            Messenger.Default.Register<Tuple<Player, Player>>(this, Tokens.GameEnded, (playersPair) =>
            {
                var gameResult = new ChildWindow();
                gameResult.Content = new TextBlock() { Text = GameOverMessage.GetRandom(playersPair.Item1.Name, playersPair.Item2.Name) };
                gameResult.Show();
            });
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Send<Tokens, MainViewModel>(Tokens.MainPageLoaded);
        }        
    }
}