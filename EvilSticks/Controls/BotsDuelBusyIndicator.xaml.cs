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

        public SticksAIPlayer FirstAIPlayer
        {
            get { return (SticksAIPlayer)GetValue(FirstAIPlayerProperty); }
            set { SetValue(FirstAIPlayerProperty, value); }
        }

        public static readonly DependencyProperty FirstAIPlayerProperty =
            DependencyProperty.Register("FirstAIPlayer", typeof(SticksAIPlayer), typeof(BotsDuelBusyIndicator), new PropertyMetadata(null));

        public SticksAIPlayer SecondAIPlayer
        {
            get { return (SticksAIPlayer)GetValue(SecondAIPlayerProperty); }
            set { SetValue(SecondAIPlayerProperty, value); }
        }

        public static readonly DependencyProperty SecondAIPlayerProperty =
            DependencyProperty.Register("SecondAIPlayer", typeof(SticksAIPlayer), typeof(BotsDuelBusyIndicator), new PropertyMetadata(null));
        
        #endregion

    }
}
