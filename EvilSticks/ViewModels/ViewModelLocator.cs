using GalaSoft.MvvmLight;

namespace EvilSticks.ViewModels
{    
    public class ViewModelLocator
    {
        private static MainViewModel _main;
        private static EducationViewModel _education;
        private static GameViewModel _game;
        private static ChangePlayerNameDialogViewModel _changePlayerNameDialog;

        public ViewModelLocator()
        {
        }

        public static MainViewModel MainStatic
        {
            get
            {
                if (_main == null)
                {
                    CreateMain();
                }

                return _main;
            }
        }

        public static EducationViewModel EducationStatic
        {
            get
            {
                if (_education == null)
                {
                    CreateEducation();
                }

                return _education;
            }
        }

        public static GameViewModel GameStatic
        {
            get
            {
                if (_game == null)
                {
                    CreateGame();
                }

                return _game;
            }
        }

        public static ChangePlayerNameDialogViewModel ChangePlayerNameDialogStatic
        {
            get
            {
                if (_changePlayerNameDialog == null)
                {
                    CreateChangePlayerNameDialog();
                }

                return _changePlayerNameDialog;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main
        {
            get
            {
                return MainStatic;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public EducationViewModel Education
        {
            get
            {
                return EducationStatic;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public GameViewModel Game
        {
            get
            {
                return GameStatic;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public ChangePlayerNameDialogViewModel ChangePlayerNameDialog
        {
            get
            {
                return ChangePlayerNameDialogStatic;
            }
        }

        public static void ClearMain()
        {
            _main.Cleanup();
            _main = null;
        }

        public static void ClearEducation()
        {
            _education.Cleanup();
            _education = null;
        }

        public static void ClearGame()
        {
            _game.Cleanup();
            _game = null;
        }

        public static void ClearChangePlayerNameDialog()
        {
            _changePlayerNameDialog.Cleanup();
            _changePlayerNameDialog = null;
        }

        public static void CreateMain()
        {
            if (_main == null)
            {
                _main = new MainViewModel();
            }
        }

        public static void CreateEducation()
        {
            if (_education == null)
            {
                _education = new EducationViewModel();
            }
        }

        public static void CreateGame()
        {
            if (_game == null)
            {
                _game = new GameViewModel();
            }
        }

        public static void CreateChangePlayerNameDialog()
        {
            if (_changePlayerNameDialog == null)
            {
                _changePlayerNameDialog = new ChangePlayerNameDialogViewModel();
            }
        }

        public static void Cleanup()
        {
            ClearMain();
            ClearEducation();
            ClearGame();
            ClearChangePlayerNameDialog();
        }
    }
}