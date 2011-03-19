using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using Game;

namespace EvilSticks.Views
{
    public partial class ChangePlayerNameDialogView : ChildWindow
    {
        public ChangePlayerNameDialogView()
        {
            InitializeComponent();
            Messenger.Default.Register<Player>(this, Tokens.PlayerNameChanged, (player) =>
            {
                Close();
            });
        }
    }
}
