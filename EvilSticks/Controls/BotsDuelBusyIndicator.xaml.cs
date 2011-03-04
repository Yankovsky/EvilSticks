using System.Windows;
using System.Windows.Controls;
using EvilSticks.Model;

namespace EvilSticks.Controls
{
    public partial class BotsDuelBusyIndicator : BusyIndicator
    {
        public BotsDuelBusyIndicator()
        {
            InitializeComponent();
            DataContext = this;
        }

        #region Dependency Properties

        public AIPlayer FirstAIPlayer
        {
            get { return (AIPlayer)GetValue(FirstAIPlayerProperty); }
            set { SetValue(FirstAIPlayerProperty, value); }
        }

        public static readonly DependencyProperty FirstAIPlayerProperty =
            DependencyProperty.Register("FirstAIPlayer", typeof(AIPlayer), typeof(BotsDuelBusyIndicator), new PropertyMetadata(null));

        public AIPlayer SecondAIPlayer
        {
            get { return (AIPlayer)GetValue(SecondAIPlayerProperty); }
            set { SetValue(SecondAIPlayerProperty, value); }
        }

        public static readonly DependencyProperty SecondAIPlayerProperty =
            DependencyProperty.Register("SecondAIPlayer", typeof(AIPlayer), typeof(BotsDuelBusyIndicator), new PropertyMetadata(null));
        
        #endregion

    }
}
