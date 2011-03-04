namespace EvilSticks.ViewModels
{
    public class ViewModelLocator
    {

        #region Bindable ViewModels Instances

        public MainViewModel Main
        {
            get
            {
                return _mainStatic;
            }
        }

        public EducationViewModel Education
        {
            get
            {
                return _educationStatic;
            }
        }

        public GameViewModel Game
        {
            get
            {
                return _gameStatic;
            }
        }

        #endregion

        #region Static Properties Holding ViewModels Instances

        private static MainViewModel _mainStatic
        {
            get
            {
                if (_main == null)
                    _main = new MainViewModel();
                return _main;
            }
        }

        private static EducationViewModel _educationStatic
        {
            get
            {
                if (_education == null)
                    _education = new EducationViewModel();
                return _education;
            }
        }

        private static GameViewModel _gameStatic
        {
            get
            {
                if (_game == null)
                    _game = new GameViewModel();
                return _game;
            }
        }

        #endregion

        #region Clean Up

        public static void Cleanup()
        {
            ClearMain();
            ClearEducation();
            ClearGame();
        }

        private static void ClearMain()
        {
            _main.Cleanup();
            _main = null;
        }

        private static void ClearEducation()
        {
            _education.Cleanup();
            _education = null;
        }

        private static void ClearGame()
        {
            _game.Cleanup();
            _game = null;
        }

        #endregion

        #region Private Fields

        private static MainViewModel _main;
        private static EducationViewModel _education;
        private static GameViewModel _game;

        #endregion

    }
}