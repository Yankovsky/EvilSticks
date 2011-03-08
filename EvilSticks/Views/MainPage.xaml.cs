using System.Windows;
using System.Windows.Controls;
using EvilSticks.ViewModels;
using GalaSoft.MvvmLight.Messaging;
using EvilSticks.Views;
using EvilSticks.Model;

namespace EvilSticks
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(MainPage_Loaded);
            Messenger.Default.Register<SticksHumanPlayer>(this, (player) =>
            {
                var dialog = new ChangePlayerNameDialogView();
                Messenger.Default.Send<SticksHumanPlayer, ChangePlayerNameDialogViewModel>(player);
                dialog.Show();
            });
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Send<Tokens, MainViewModel>(Tokens.MainPageLoaded);
        }
    }
}
