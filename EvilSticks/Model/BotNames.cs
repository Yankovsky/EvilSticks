using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace EvilSticks.Model
{
    public static class BotNames
    {
        static BotNames()
        {
            _namesCount = _names.GetUpperBound(0) - _names.GetLowerBound(0);
            _modificatorsCount = _modificators.GetUpperBound(0) - _modificators.GetLowerBound(0);
        }

        public static string GetRandom()
        {
            return _names[_random.Next(0, _namesCount)] + _modificators[_random.Next(0, _modificatorsCount)];
        }

        private static Random _random = new Random();
        private static string[] _names = new string[] { "IVAN", "VASILISA", "PARASHKA", "ROZA", "FEKLA", "AGLAYA", "ANFISA", "AGATA", "LADA", "AVDOTYA", "STEPA" };
        private static string[] _modificators = new string[] { "-2000", "~Me", "~XP", "~4G", ".NET", ".ru", "7", "1.1", "№1", "(C)", "+", "666", "777", ".AI", ".Bot" };
        private static int _namesCount;
        private static int _modificatorsCount;
    }
}
