using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using EvilSticks.Model;

namespace EvilSticks.Views
{
    public partial class ChangePlayerNameDialogView : ChildWindow
    {
        public ChangePlayerNameDialogView()
        {
            InitializeComponent();
            Messenger.Default.Register<SticksHumanPlayer>(this, Tokens.PlayerNameChanged, (player) =>
            {
                Close();
            });
        }
    }
}
